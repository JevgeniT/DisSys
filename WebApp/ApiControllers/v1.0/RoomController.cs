using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Public.DTO;
using Public.DTO.Mappers;

namespace WebApp.ApiControllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DTOMapper<BLL.App.DTO.Room, RoomDTO> _mapper = new DTOMapper<Room, RoomDTO>();

        public RoomController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms(Guid propertyId)
        {
            return Ok(await _bll.Rooms.AllAsync(propertyId));
        }
        
        
        [HttpGet][Route("property/{propertyId}")]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetPropertyRooms(Guid propertyId)
        {
            return Ok(await _bll.Rooms.AllAsync(propertyId));
        }
        
        // GET: api/Rooms/5
        [HttpGet("{id}")][AllowAnonymous] 
        public async Task<ActionResult<RoomDTO>> GetRoom(Guid id)
        {
            var room = await _bll.Rooms.FirstOrDefaultAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }
 
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(Guid id, RoomDTO room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            if (!await _bll.Rooms.ExistsAsync(id))
            {
                return NotFound();
            }

            _bll.Rooms.Update(_mapper.Map(room));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

 
        [HttpPost]
        public async Task<ActionResult<RoomDTO>> PostRoom(RoomDTO room)
        {
            var entity = _mapper.Map(room);
            _bll.Rooms.Add(entity);
            await _bll.SaveChangesAsync();
            room.Id = entity.Id;
            return CreatedAtAction("GetRoom", new { id = room.Id }, room);
        }
        
        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RoomDTO>> DeleteRoom(Guid id)
        {
            var room = await _bll.Rooms.FindAsync(id);
            
            if (room == null)
            {
                return NotFound();
            }
        
            await _bll.Rooms.DeleteAsync(id);
            await _bll.SaveChangesAsync();
        
            return Ok(room);
        }
        
    }
}
