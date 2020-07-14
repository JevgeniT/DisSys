using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using DAL.App.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public AvailabilityController(IAppBLL bll)
        {
            _bll = bll;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Availability>>> GetDates(SearchDTO searchDTO)
        {
            var availability = (await _bll.Availabilities.AllAsync()).Select(bllEntity => new Availability()
            {
                Id = bllEntity.Id,
                From = bllEntity.From,
                To = bllEntity.To,
                IsUsed = bllEntity.IsUsed,
                RoomId = bllEntity.RoomId, 
                PricePerNightForAdult = bllEntity.PricePerNightForAdult
            });
            
            return   Ok(availability);
        }

        [HttpPost][Route("checkdates")][Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<Availability>>> CheckDates(SearchDTO searchDTO)
        {
            var availability = (await _bll.Availabilities.FindAvailableDates(searchDTO.From, searchDTO.To,searchDTO.PropertyId))
               .Select(bllEntity => new Availability()
                {
                    Id = bllEntity.Id,
                    From = bllEntity.From,
                    To = bllEntity.To,
                    // IsUsed = bllEntity.IsUsed,
                    RoomId = bllEntity.RoomId,
                    Policy = bllEntity.Policy,
                    PricePerNightForAdult = bllEntity.PricePerNightForAdult,
                    RoomsAvailable = bllEntity.RoomsAvailable
                });
            ;

            return Ok(availability);
        }
       
 
    }
}