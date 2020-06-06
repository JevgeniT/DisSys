using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain;
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
        
        private readonly AppDbContext _context;

        public DateController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Availability>>> GetDates()
            // public async Task<string> GetDates()
        {

            return await _context.Availabilities.ToListAsync();
            
        }

        [HttpGet("/{from}&{to}")]
        public async Task<ActionResult<Availability>> GetDate(DateTime from, DateTime to)
        
        {
            Console.WriteLine(true);
            return await _context.Availabilities.Where(a => a.From == from && a.To == to).FirstAsync();
        }
    }
}