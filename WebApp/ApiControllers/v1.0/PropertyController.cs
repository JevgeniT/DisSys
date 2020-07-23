using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Public.DTO;
using Public.DTO.Mappers;

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
        private readonly PropertyMapper _mapper = new PropertyMapper();
        
        public PropertyController(IAppBLL bll)
        {
            _bll = bll;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyDTO>>> GetProperties()
        {
            var properties = (await _bll.Properties.AllAsync()) //user !!
                .Select(bllEntity => _mapper.Map(bllEntity)) ;
             return Ok(properties);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("find")]
        public async Task<ActionResult<IEnumerable<PropertyViewDTO>>> FindProperties(SearchDTO search)
        {
             var found = _bll.Properties.FindAsync(search); // TODO 
             var properties = (await found);
             return Ok(properties.Select(p=> _mapper.MapPropertyView(p)));
        }
        
        
        // GET: api/Properties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDTO>> GetProperty(Guid id)
        {
            var property = await _bll.Properties.FirstOrDefaultAsync(id);
            
            if (property == null)
            {
                return NotFound();
            }
 
            return Ok(_mapper.Map(property));
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
            prop.Country = property.Country;
            prop.Name = property.Name;

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
           
            var prop = new Property()
            {
                AppUserId = User.UserGuidId(),
                Country =  property.Country,
                Name = property.Name,
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
