using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.DTO;
using Public.DTO.Mappers;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PolicyController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DTOMapper<Policy, PolicyDTO> _mapper = new DTOMapper<Policy, PolicyDTO>();
        
        public PolicyController(IAppBLL bll)    
        {
            _bll = bll;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PolicyDTO>>> GetPolicies([FromQuery(Name = "pId")] Guid pId)
        {
            var policies = await _bll.Policies.AllAsync(pId);
            return Ok(policies.Select(p=> _mapper.Map(p)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PolicyDTO>> GetPolicy(Guid id)
        {
            var policy = await _bll.Policies.FindAsync(id);

            if (policy == null)
            {
                return NotFound();
            }

            return Ok(policy);
        }
 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPolicy(Guid id, PolicyDTO policy)
        {
            if (id != policy.Id)
            {
                return BadRequest();
            }

            if (! await _bll.Policies.ExistsAsync(id))
            {
                return NotFound();
            }

            _bll.Policies.Update(_mapper.Map(policy));
            await _bll.SaveChangesAsync();
           

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<PolicyDTO>> PostPolicy(PolicyDTO policy)
        {
            var entity = _mapper.Map(policy);
            
            _bll.Policies.Add(entity);
            await _bll.SaveChangesAsync();
            policy.Id = entity.Id;
            
            return CreatedAtAction("GetPolicy", new { id = policy.Id }, policy);
        }

        // DELETE: api/Policy/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PolicyDTO>> DeletePolicy(Guid id)
        {
            var policy = await _bll.Policies.FindAsync(id);
            
            if (policy == null)
            {
                return NotFound();
            }

            _bll.Policies.Remove(policy);
            await _bll.SaveChangesAsync();

            return Ok(policy);
        }
        
    }
}
