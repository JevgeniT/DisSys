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
using Microsoft.EntityFrameworkCore.Internal;
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
        private readonly DTOMapper<Availability, AvailabilityDTO> _mapper = new DTOMapper<Availability, AvailabilityDTO>();
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public AvailabilityController(IAppBLL bll)
        {
            _bll = bll;
        }

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
            if (availability == null)
            {
                return NotFound(new MessageDTO("No availabilities were found"));
            }
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
        [Route("checkdates")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AvailabilityDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<IEnumerable<AvailabilityDTO>>> CheckDates([FromQuery]DateTime from, [FromQuery]DateTime to, [FromQuery]Guid pId)
        {
            var availability = (await _bll.Availabilities.FindAvailableDates(from, to, pId)).Select(bllEntity => _mapper.Map(bllEntity));
            if (availability == null)
            {
                return NotFound(new MessageDTO("No availabilities were found"));
            }
            var result = availability.GroupBy(availability => availability.RoomId).Select(e => e.First());
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
            if (await _bll.Availabilities.ExistsAsync(availability.From.Date, availability.To.Date))
            {
                return BadRequest(new MessageDTO("Dates already exist"));
            }
       
            var entity = _mapper.Map(availability); 
            _bll.Availabilities.Add(entity);

            await _bll.SaveChangesAsync();
            availability.Id = entity.Id;
            return CreatedAtAction("GetDates", new { id = availability.Id }, availability);
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
            if (id != availability.Id)
            {
                return BadRequest(new MessageDTO("Ids does not match!"));
            }

            if (!await _bll.Availabilities.ExistsAsync(id))
            {
                return NotFound(new MessageDTO($"Availability with id {id} was not found"));
            } 
            
            await _bll.Availabilities.UpdateAsync(_mapper.Map(availability));
            await _bll.SaveChangesAsync();
            return NoContent();
        }
 
    }
}