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
    /// Availability controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class AvailabilityController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DTOMapper<Availability, AvailabilityDTO> _mapper = new ();
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public AvailabilityController(IAppBLL bll) { _bll = bll; }

        /// <summary>
        /// Get all room availabilities
        /// </summary>
        /// <param name="rId">Room Id</param>
        /// <returns>Array of availabilities</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AvailabilityDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<IEnumerable<AvailabilityDTO>>> GetDates([FromQuery] Guid rId)
        {
            var availability = (await _bll.Availabilities.AllAsync(rId)).Select(a=> _mapper.Map(a));
             
            return Ok(availability);
        }

        /// <summary>
        /// Get all property availabilities
        /// </summary>
        /// <param name="pId">Property Id</param>
        /// <param name="from">Check in date</param>
        /// <param name="to">Check out date</param>
        /// <returns>Array of availabilities</returns>
        [HttpGet]
        [Route("check")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AvailabilityDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<IEnumerable<AvailabilityDTO>>> CheckDates([FromQuery]DateTime from, [FromQuery]DateTime to, [FromQuery]Guid pId)
        {
            var availability = (await _bll.Availabilities.FindAvailableDates(from, to, pId)).Select(bllEntity => _mapper.Map(bllEntity));
          
            var result = availability.GroupBy(a => a.RoomId).Select(e => e.First());
            return Ok(result);
        }
       
        /// <summary>
        /// Create availability
        /// </summary>
        /// <param name="availability"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AvailabilityDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<ActionResult<AvailabilityDTO>> PostAvailability(AvailabilityDTO availability)
        {
            var entity = _mapper.Map(availability);
            if (await _bll.Availabilities.ExistsAsync(entity)) return BadRequest(new MessageDTO("Dates already exist"));
         
            _bll.Availabilities.Add(entity);
            availability.Id = entity.Id;

            return Ok(entity);
            // return CreatedAtAction("GetDates", new { id = availability.Id }, availability);
        }
        
        /// <summary>
        /// Update availability
        /// </summary>
        /// <param name="id"></param>
        /// <param name="availability"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutAvailability(Guid id, AvailabilityDTO availability)
        {
            if (id != availability.Id) return BadRequest(new MessageDTO("Ids does not match!"));
           
            await _bll.Availabilities.UpdateAsync(_mapper.Map(availability));
            await _bll.SaveChangesAsync();
            return NoContent();
        }
    }
}