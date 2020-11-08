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

namespace WebApp.ApiControllers.v1._0
{
    /// <summary>
    /// Facility controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExtraController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DTOMapper<Extra, ExtraDTO> _mapper = new DTOMapper<Extra, ExtraDTO>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ExtraController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        /// <summary>
        /// Get all extras
        /// </summary>
        /// <returns>Array of extras</returns>
        [HttpGet]
        [Produces("application/json")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExtraDTO>))]
        public async Task<ActionResult<IEnumerable<ExtraDTO>>> GetExtras([FromQuery] Guid pId)
        {
            var extra = (await _bll.Extras.AllAsync(pId)).Select(a => _mapper.Map(a)); 
            return Ok(extra);
        } 
        
        
        /// <summary>
        /// Create Extra
        /// </summary>
        /// <param name="extra"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ExtraDTO))]
        public async Task<ActionResult<ExtraDTO>> PostExtra(ExtraDTO extra)
        {
            var entity = _mapper.Map(extra);
            _bll.Extras.Add(entity);
            await _bll.SaveChangesAsync();
            extra.Id = entity.Id;
            return CreatedAtAction("GetExtra", new { id = extra.Id }, extra);
        }
        
        
           
        /// <summary>
        /// Get single extra
        /// </summary>
        /// <param name="id"></param>
        /// <returns>extra object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExtraDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<ExtraDTO>> GetExtra(Guid id)
        {
            var extra = await _bll.Extras.FirstOrDefaultAsync(id);

            if (extra == null)
            {
                return NotFound(new MessageDTO($"Extra with id {id} not found"));
            }

            return Ok(_mapper.Map(extra));
        }
        
        /// <summary>
        /// Update the Extra
        /// </summary>
        /// <param name="id">Extra Id</param>
        /// <param name="extra">Extra to update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutExtra(Guid id, ExtraDTO extra)
        {
            if (id != extra.Id)
            {
                return BadRequest(new MessageDTO("Ids does not match!"));
            }

            if (!await _bll.Reviews.ExistsAsync(id))
            {
                return BadRequest(new MessageDTO("Extra does not exists"));
            }

            await _bll.Extras.UpdateAsync(_mapper.Map(extra));
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}