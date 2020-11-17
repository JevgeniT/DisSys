using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        /// Get all Properties
        /// </summary>
        /// <returns>Array of properties</returns>
        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<PropertyDTO>>> GetProperties()
        {
            var  properties = (await _bll.Properties.AllAsync(User.UserGuidId())).Select(bllEntity => _mapper.Map(bllEntity));
            return Ok(properties);
        }

        /// <summary>
        /// Find Properties
        /// </summary>
        /// <param name="from">Check in date</param>
        /// <param name="to">Check out date</param>
        /// <param name="input">Name or location of the property</param>
        /// <returns>Array of the properties that match criteria</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("find")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PropertyViewDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<IEnumerable<PropertyViewDTO>>> FindProperties([FromQuery] DateTime? from,
            [FromQuery] DateTime? to, [FromQuery] string input)
        {
             var properties = (await _bll.Properties.FindAsync(from, to, input)); // TODO 
            
             if (!properties.Any())
             {
                 return NotFound(new MessageDTO("Nothing was found"));
             }
             return Ok(properties.Select(p=> _mapper.MapPropertyView(p)));
        }
        
        
        /// <summary>
        /// Get single Property
        /// </summary>
        /// <param name="id">Property Id</param>
        /// <returns>Property object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<PropertyDTO>> GetProperty(Guid id)
        { 
            var property =  _mapper.Map(await _bll.Properties.FirstOrDefaultAsync(id));
  
            if (property == null)
            {
                 return NotFound(new MessageDTO($"Property with id {id} not found"));
            }
            return Ok(property);
        }

        /// <summary>
        /// Update Property
        /// </summary>
        /// <param name="id">Property Id</param>
        /// <param name="property"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutProperty(Guid id, PropertyDTO property)
        {
            if (id != property.Id)
            {
                return BadRequest(new MessageDTO("Ids does not match!"));
            }

            if (!await _bll.Properties.ExistsAsync(id))
            {
                return BadRequest(new MessageDTO($"Property was not found"));
            } 
            
            await _bll.Properties.UpdateAsync(_mapper.Map(property));
            await _bll.SaveChangesAsync();
            return NoContent();
        }

      
        /// <summary>
        /// Create Property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PropertyDTO))]
        public async Task<ActionResult<PropertyDTO>> PostProperty(PropertyDTO property)
        {
            property.AppUserId = User.UserGuidId();
            var entity = _mapper.Map(property);
            _bll.Properties.Add(entity);
              await _bll.SaveChangesAsync(); 
            property.Id = entity.Id; 
            return CreatedAtAction("GetProperty", new { id = property.Id }, property);
        }

        /// <summary>
        /// Delete Property
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<PropertyDTO>> DeleteProperty(Guid id)
        {
            var property = await _bll.Properties.FirstOrDefaultAsync(id);
            
            if (property == null)
            {
                return NotFound(new MessageDTO($"Property with id {id} was not found"));
            }

            await _bll.Properties.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(property);
        }

       
    }
}
