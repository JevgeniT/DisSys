using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLL.App.DTO;
using Contracts.BLL.App;
using Public.DTO;

namespace WebApp.ApiControllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public RoomController(IAppBLL context)
        {
            _bll = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms(SearchDTO? searchDTO)
        {
            return Ok(await _bll.Rooms.AllAsync(searchDTO));
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(Guid id)
        {
            var room = await _bll.Rooms.FirstOrDefaultAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(Guid id, Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            // _bll.Entry(room).State = EntityState.Modified;

            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Rooms.ExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

 
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            _bll.Rooms.Add(room);
            await _bll.SaveChangesAsync();
        
            return CreatedAtAction("GetRoom", new { id = room.Id }, room);
        }
        
        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> DeleteRoom(int id)
        {
            var room = await _bll.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
        
            _bll.Rooms.Remove(room);
            await _bll.SaveChangesAsync();
        
            return room;
        }
        //
    }
}
