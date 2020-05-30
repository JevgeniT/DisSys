using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers
{
    
    [Produces("application/json")]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RoomAvailabilityController :ControllerBase
    {
        private readonly AppDbContext _context;


        public RoomAvailabilityController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomAvailability>>> get()
        {


            return await  _context.RoomAvailabilities.ToListAsync();
        }
    }
}