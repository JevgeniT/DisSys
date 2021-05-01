using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Public.DTO;
using Public.DTO.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Rooms
    /// </summary>

    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DTOMapper<Room, RoomDTO> _mapper = new();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public RoomController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get all property Rooms
        /// </summary>
        /// <param name="pId">Property Id</param>
        /// <returns>Array of rooms</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoomDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))] 
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms([FromQuery] Guid pId)
        {
            var result = await _bll.Rooms.AllAsync(pId);
            if (result is null)
            {
                return NotFound(new MessageDTO($"Property with id {pId} does not have any rooms yet"));
            }
            return Ok(result);
        }

        /// <summary>
        /// Get single Room
        /// </summary>
        /// <param name="id">Room Id</param>
        /// <returns>Room object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous] 
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<RoomDTO>> GetRoom(Guid id)
        {
            var room = await _bll.Rooms.FirstOrDefaultAsync(id);
            if (room is null)
            {
                return NotFound(new MessageDTO($"Room with the id {id} was not found"));
            }

            return Ok(room);
        }
 
        
        /// <summary>
        /// Update the Room
        /// </summary>
        /// <param name="id">Room Id</param>
        /// <param name="room">room object</param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutRoom(Guid id, RoomDTO room)
        {
            if (id != room.Id)
            {
                return BadRequest(new MessageDTO("Ids does not match"));
            }

            if (!await _bll.Rooms.ExistsAsync(id))
            {
                return NotFound(new MessageDTO($"Room with the id {id} was not found"));
            }

            await _bll.Rooms.UpdateAsync(_mapper.Map(room));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        
        /// <summary>
        /// Post the new Room
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RoomDTO))]
        public async Task<ActionResult<RoomDTO>> PostRoom(RoomDTO room)
        {
            var entity = _mapper.Map(room); 
            _bll.Rooms.Add(entity);
            await  _bll.SaveChangesAsync();

            if (room.FacilityDtos is not null)
            {
                await _bll.RoomFacilities.AddRangeAsync(room.FacilityDtos?.Select(f => new RoomFacilities{FacilityId = f.Id,RoomId = entity.Id}).ToList()!);
                //todo
            }
            room.Id = entity.Id;
            await  _bll.SaveChangesAsync();

            return CreatedAtAction("GetRoom", new { id = room.Id }, room);
        }
        
        
        /// <summary>
        /// Delete the Room
        /// </summary>
        /// <param name="id">Room id to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<RoomDTO>> DeleteRoom(Guid id)
        {
            var room = await _bll.Rooms.FirstOrDefaultAsync(id);
            if (room is null)
            {
                return NotFound(new MessageDTO($"Room with id {id} was not found"));
            }
        
            await _bll.Rooms.RemoveAsync(id);
            await _bll.SaveChangesAsync();
        
            return Ok(room);
        }
        
    }
}
