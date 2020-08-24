using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using DAL.App.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.DTO;


namespace WebApp.ApiControllers
{
 
    [Produces("application/json")]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class AvailabilityController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DALMapper<Availability,AvailabilityDTO> _mapper = new DALMapper<Availability, AvailabilityDTO>();
        
        public AvailabilityController(IAppBLL bll)
        {
            _bll = bll;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvailabilityDTO>>> GetDates([FromQuery] Guid rId)
        {
            var availability = (await _bll.Availabilities.AllAsync(rId)); //TODO
            
            return Ok(availability.Select(a=> _mapper.Map(a)));
        }

        [HttpGet]
        [Route("checkdates")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AvailabilityDTO>>> CheckDates([FromQuery]DateTime from, [FromQuery]DateTime to, [FromQuery] Guid pId)
        {
            var availability = (await _bll.Availabilities.FindAvailableDates(from, to, pId))
               .Select(bllEntity => _mapper.Map(bllEntity));
            return Ok(availability);
        }
       
        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<AvailabilityDTO>> PostAvailability(AvailabilityDTO availability)
        {
            var entity = _mapper.Map(availability);
            
            _bll.Availabilities.Add(entity);
            await _bll.SaveChangesAsync();
            availability.Id = entity.Id;
            return CreatedAtAction("GetDates", new { id = availability.Id }, availability);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProperty(Guid id, AvailabilityDTO availability)
        {
            if (id != availability.Id)
            {
                return BadRequest(new MessageDTO("Ids does not match!"));
            }

            if (!await _bll.Availabilities.ExistsAsync(id))
            {
                return BadRequest();
            } 
            
            _bll.Availabilities.Update(_mapper.Map(availability));
            await _bll.SaveChangesAsync();
            return NoContent();
        }
 
    }
}