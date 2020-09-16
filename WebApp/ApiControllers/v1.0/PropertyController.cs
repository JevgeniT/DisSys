using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cache;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Public.DTO;
using Public.DTO.Mappers;
using WebApp.Helpers;

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
        private readonly IResponseCacheService _cache;
        /// <summary>
        ///  Constructor
        /// </summary>
        public PropertyController(IAppBLL bll, IResponseCacheService cache)
        {
            _cache = cache;
            _bll = bll;
        }

        /// <summary>
        /// Get all Properties
        /// </summary>
        /// <returns>Array of properties</returns>
        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<PropertyDTO>>> GetProperties()
        {
            var key = User.UserGuidId().ToString();
             
             
             
            if (await _cache.GetCachedResponseAsync(key) == null)
            {
                var  properties = (await _bll.Properties.AllAsync(User.UserGuidId())).Select(bllEntity => _mapper.Map(bllEntity)) ;
                     await _cache.CacheResponseAsync(key ,JsonConvert.SerializeObject(properties) );
                     return Ok(properties);
            }

            var cache = await _cache.GetCachedResponseAsync(key);


            return Ok(JsonConvert.DeserializeObject<ICollection<PropertyDTO>>(cache));
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
        public async Task<ActionResult<IEnumerable<PropertyViewDTO>>> FindProperties([FromQuery] DateTime? from,
            [FromQuery] DateTime? to, [FromQuery] string input)
        {
             var properties = (await _bll.Properties.FindAsync(from, to, input)); // TODO 
             return Ok(properties.Select(p=> _mapper.MapPropertyView(p)));
        }
        
        
        /// <summary>
        /// Get single Property
        /// </summary>
        /// <param name="id">Property Id</param>
        /// <returns>Property object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
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
        public async Task<ActionResult<PropertyDTO>> DeleteProperty(Guid id)
        {
            var property = await _bll.Properties.FirstOrDefaultAsync(id);
            
            if (property == null)
            {
                return NotFound();
            }

            await _bll.Properties.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(property);
        }

       
    }
}
