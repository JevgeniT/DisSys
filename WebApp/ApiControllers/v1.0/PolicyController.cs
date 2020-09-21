using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Policy Controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PolicyController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DTOMapper<Policy, PolicyDTO> _mapper = new DTOMapper<Policy, PolicyDTO>();
        
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public PolicyController(IAppBLL bll)    
        {
            _bll = bll;
        }
        
        /// <summary>
        /// Get all property Policies
        /// </summary>
        /// <param name="pId">Property Id</param>
        /// <returns>Array of policies</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PolicyDTO>))]
        public async Task<ActionResult<IEnumerable<PolicyDTO>>> GetPolicies([FromQuery] Guid pId)
        {
            var policies = await _bll.Policies.AllAsync(pId);
            return Ok(policies.Select(p=> _mapper.Map(p)));
        }

        
        /// <summary>
        /// Get single Policy
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Policy object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PolicyDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<PolicyDTO>> GetPolicy(Guid id)
        {
            var policy = await _bll.Policies.FirstOrDefaultAsync(id);

            if (policy == null)
            {
                return NotFound(new MessageDTO($"Policy with id {id} not found"));
            }

            return Ok(policy);
        }
 
        /// <summary>
        /// Update Policy
        /// </summary>
        /// <param name="id"></param>
        /// <param name="policy"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutPolicy(Guid id, PolicyDTO policy)
        {
            if (id != policy.Id)
            {
                return BadRequest(new MessageDTO("Ids does not match!"));
            }

            if (! await _bll.Policies.ExistsAsync(id))
            {
                return NotFound(new MessageDTO($"Review does not exist"));
            }
            
            await _bll.Policies.UpdateAsync(_mapper.Map(policy));
            await _bll.SaveChangesAsync();
           

            return NoContent();
        }

        
        /// <summary>
        /// Create Policy
        /// </summary>
        /// <param name="policy"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PolicyDTO))]
        public async Task<ActionResult<PolicyDTO>> PostPolicy(PolicyDTO policy)
        {
            var entity = _mapper.Map(policy);
            _bll.Policies.Add(entity);
            await _bll.SaveChangesAsync();
            
            policy.Id = entity.Id;
            
            return CreatedAtAction("GetPolicy", new { id = policy.Id }, policy);
        }

        /// <summary>
        /// Delete Policy
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PolicyDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<PolicyDTO>> DeletePolicy(Guid id)
        {
            var policy = await _bll.Policies.FirstOrDefaultAsync(id);
            
            if (policy == null)
            {
                return NotFound(new MessageDTO($"Policy with id {id} was not found"));
            }

            await _bll.Policies.RemoveAsync(policy);
            await _bll.SaveChangesAsync();

            return Ok(policy);
        }
        
    }
}
