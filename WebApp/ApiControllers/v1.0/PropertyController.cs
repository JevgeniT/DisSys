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
    [Authorize(Roles = "host")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    
    public class PropertyController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PropertyMapper _mapper = new();
        
        /// <summary>
        ///  Constructor
        /// </summary>
        public PropertyController(IAppBLL bll) { _bll = bll; }

        /// <summary>
        /// Get all Properties
        /// </summary>
        /// <returns>Array of properties</returns>
        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<PropertyDTO>>> GetProperties()
            => Ok((await _bll.Properties.AllAsync(User.UserGuidId())).Select(bllEntity => _mapper.Map(bllEntity)));
        
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
        public async Task<ActionResult<IEnumerable<PropertyViewDTO>>> FindProperties(
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] string input)
        {
             var properties = await _bll.Properties.FindAsync(from, to, input); // TODO 

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
  
            return Ok(property);
        }

        /// <summary>
        /// Update Property
        /// </summary>
        /// <param name="id">Property Id</param>
        /// <param name="property"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<ActionResult<PropertyCreateDTO>> PutProperty(Guid id, PropertyCreateDTO property)
        {
            if (id != property.Id) return BadRequest(new MessageDTO("Ids does not match!"));
            
            var updated = _mapper.MapPropertyCreateView(property);
            updated.AppUserId = User.UserGuidId();
            await _bll.Properties.UpdateAsync(updated);
            await _bll.SaveChangesAsync();
            
            return Ok(property);
        }

      
        /// <summary>
        /// Create Property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(Roles = "host")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PropertyDTO))]
        public ActionResult<PropertyDTO> PostProperty(PropertyCreateDTO property)
        {
            var entity = _mapper.MapPropertyCreateView(property);
            entity.AppUserId = User.UserGuidId();
            entity = _bll.Properties.Add(entity);
            return CreatedAtAction("GetProperty", new { id = entity.Id }, entity);
        }

        /// <summary>
        /// Delete Property
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<ActionResult<PropertyDTO>> DeleteProperty(Guid id)
        {
            var property = await _bll.Properties.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return Ok(property);
        }   
    }
}
