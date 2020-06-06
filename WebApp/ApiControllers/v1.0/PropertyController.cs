using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Public.DTO;
using Room = BLL.App.DTO.Room;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PropertyController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public PropertyController(IAppBLL bll)
        {
            _bll = bll;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Property>>> GetProperties()
        {
            var properties = (await _bll.Properties.AllAsync(User.UserGuidId()))
                .Select(bllEntity => new Property()
                {
                    Id = bllEntity.Id,
                    Address = bllEntity.Address,
                    PropertyLocation = bllEntity.PropertyLocation,
                    PropertyName = bllEntity.PropertyName,
                    AppUserId = bllEntity.AppUserId
                }) ;
            
            return Ok(properties);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("find")]
        
        public async Task<ActionResult<IEnumerable<Property>>> FindProperties(SearchDTO search)
        {
            Console.WriteLine(search);
             var found = _bll.Properties.FindAsync(search);
             
             // var rooms = await _bll.PropertyRoomsService.FindAsync(id);
             // Console.WriteLine(found.Result.Count());
             
             var properties = (await found)
                .Select(bllEntity => new Property()
                {
                    Id = bllEntity.Id,
                    Address = bllEntity.Address,
                    PropertyLocation = bllEntity.PropertyLocation,
                    PropertyName = bllEntity.PropertyName,
                    // PropertyRooms =  bllEntity.PropertyRooms
                    // AppUserId = bllEntity.AppUserId
                }) ;
            
            return Ok(properties);
        }
        // GET: api/Properties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Property>> GetProperty(Guid id)
        {
            var property = await _bll.Properties.FirstOrDefaultAsync(id);
           
            if (property == null)
            {
                return NotFound();
            }
            return Ok(property);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProperty(Guid id, Property property)
        {
            if (id != property.Id)
            {
                return BadRequest();
            }
            var prop = await _bll.Properties.FirstOrDefaultAsync(property.Id, User.UserGuidId());

            if (prop == null)
            {
                return BadRequest();
            }

            prop.Address = property.Address;
            prop.PropertyLocation = property.PropertyLocation;
            prop.PropertyName = property.PropertyName;

            _bll.Properties.Update(prop);
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Properties.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

      
        [HttpPost]
        public async Task<ActionResult<Property>> PostProperty(Property property)
        {
           
            var prop = new BLL.App.DTO.Property()
            {
                AppUserId = User.UserGuidId(),
                PropertyLocation =  property.PropertyLocation,
                PropertyName = property.PropertyName,
                Address =property.Address
            };
             _bll.Properties.Add(prop);
            await _bll.SaveChangesAsync();
           
            return CreatedAtAction("GetProperty", new { id = property.Id }, prop);
        }

        // DELETE: api/Properties/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Property>> DeleteProperty(Guid id)
        {
            var property = await _bll.Properties.FirstOrDefaultAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            _bll.Properties.Remove(property);
            await _bll.SaveChangesAsync();

            return Ok(property);
        }

       
    }
}
