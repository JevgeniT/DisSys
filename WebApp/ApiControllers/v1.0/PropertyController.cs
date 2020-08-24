using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Public.DTO;
using Public.DTO.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    ///  Properties
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    
    public class PropertyController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PropertyMapper _mapper = new PropertyMapper();
        
        /// <summary>
        ///  Constructor
        /// </summary>
        public PropertyController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// get all properties
        /// </summary>
        /// <returns>Array of properties</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyDTO>>> GetProperties()
        {
            var properties = (await _bll.Properties.AllAsync(User.UserGuidId())) //user !!
                .Select(bllEntity => _mapper.Map(bllEntity)) ;
             return Ok(properties);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("find")]
        public async Task<ActionResult<IEnumerable<PropertyViewDTO>>> FindProperties([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] string input)
        {
             var properties = (await _bll.Properties.FindAsync(from, to, input)); // TODO 
             return Ok(properties.Select(p=> _mapper.MapPropertyView(p)));
        }
        
        
        [HttpGet("{id}")][AllowAnonymous]
        public async Task<ActionResult<PropertyDTO>> GetProperty(Guid id)
        {
            var property = await _bll.Properties.FirstOrDefaultAsync(id);
            
            if (property == null)
            {
                return NotFound(new MessageDTO($"Property with id {id} not found"));
            }
 
            return Ok(_mapper.Map(property));
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProperty(Guid id, PropertyDTO property)
        {
            if (id != property.Id)
            {
                return BadRequest(new MessageDTO("Ids does not match!"));
            }

            if (!await _bll.Properties.ExistsAsync(id))
            {
                return BadRequest();
            } 
            
            _bll.Properties.Update(_mapper.Map(property));
            await _bll.SaveChangesAsync();
            return NoContent();
        }

      
        [HttpPost]
        public async Task<ActionResult<PropertyDTO>> PostProperty(PropertyDTO property)
        {

            var entity = _mapper.Map(property);
            
             _bll.Properties.Add(entity);
             
            await _bll.SaveChangesAsync();

            property.Id = entity.Id;
            return CreatedAtAction("GetProperty", new { id = property.Id }, property);
        }

        // DELETE: api/Properties/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PropertyDTO>> DeleteProperty(Guid id)
        {
            var property = await _bll.Properties.FirstOrDefaultAsync(id);
            
            if (property == null)
            {
                return NotFound();
            }

            await _bll.Properties.DeleteAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(property);
        }

       
    }
}
