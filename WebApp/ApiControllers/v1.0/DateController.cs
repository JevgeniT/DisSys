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
 

namespace WebApp.ApiControllers
{
 
    [Produces("application/json")]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class DateController : ControllerBase
    {
        
        private readonly IAppBLL _bll;

        public DateController(IAppBLL bll)
        {
            _bll = bll;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Availability>>> GetDates()
            // public async Task<string> GetDates()
        {
            var availability = (await _bll.Availabilities.AllAsync()).Select(bllEntity => new Availability()
            {
                Id = bllEntity.Id,
                From = bllEntity.From,
                To = bllEntity.To,
                IsUsed = bllEntity.IsUsed,
             }) ;
            return   Ok(availability);
            
        }
 
    }
}