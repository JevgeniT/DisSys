using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Public.DTO;
using Public.DTO.Mappers;


namespace WebApp.ApiControllers
{
 
    /// <summary>
    /// PropertyRules controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PropertyRulesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DTOMapper<PropertyRules, PropertyRulesDTO> _mapper = new();
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public PropertyRulesController(IAppBLL bll)
        {
            _bll = bll;
        }


        /// <summary>
        /// Get property's rules
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyRulesDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<PropertyRules>> GetPropertyRules(Guid id)
        {
            var rule = await _bll.PropertyRules.FirstOrDefaultAsync(id);
            
            return Ok(_mapper.Map(rule));
        }
        
        /// <summary>
        /// Create rules for property
        /// </summary>
        /// <param name="rules"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyRulesDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<ActionResult<PropertyRulesDTO>> PostPropertyRules(PropertyRulesDTO rules)
        {
            var entity = _mapper.Map(rules);
            if (await _bll.PropertyRules.ExistsAsync(rules.Id))
            {
                await _bll.PropertyRules.UpdateAsync(entity);
                return CreatedAtAction("GetPropertyRules", new { id = rules.Id }, rules);
            }

            _bll.PropertyRules.Add(entity);
            await _bll.SaveChangesAsync();
            rules.Id = entity.Id;
            
            return CreatedAtAction("GetPropertyRules", new { id = rules.Id }, rules);
        }
        
        /// <summary>
        /// Update rules
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rules"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutPropertyRules(Guid id, PropertyRulesDTO rules)
        {
            if (id != rules.Id) return BadRequest(new MessageDTO("Ids does not match!"));
            
            await _bll.PropertyRules.UpdateAsync(_mapper.Map(rules));
            await _bll.SaveChangesAsync();
            return NoContent();
        }
 
    }
}