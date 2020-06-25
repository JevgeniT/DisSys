using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Domain.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Public.DTO;

namespace WebApp.ApiControllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IAppBLL _bll;
 
        public ReservationController(IAppBLL context)
        {
            _bll = context;
        }

        // GET: api/Reservation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            
            var reservations = (await _bll.Reservations.AllAsync(User.UserGuidId()))
                .Select(bllEntity => new Reservation()
                {
                    Id = bllEntity.Id,
                    CheckInDate = bllEntity.CheckInDate,
                    CheckOutDate = bllEntity.CheckOutDate,
                    PropertyId = bllEntity.PropertyId,
                    AppUserId = bllEntity.AppUserId
                }) ;

            return Ok(reservations);

        }

        // GET: api/Reservation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(Guid id)
        {
            var reservation = await _bll.Reservations.FirstOrDefaultAsync(id, User.UserGuidId());

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

   
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(Guid id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }

            var res = await _bll.Reservations.FirstOrDefaultAsync(reservation.Id, User.UserGuidId());
            if (res == null)
            {
                return BadRequest();
            }
            
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Reservations.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            var available = await _bll.Availabilities.FindAvailableDates(reservation.CheckInDate,reservation.CheckOutDate);
            
            _bll.Availabilities.ParseDate(new List<Availability>(available.ToList()), reservation.CheckInDate,reservation.CheckOutDate);
            
            if (!available.Any())
            {
                return BadRequest(new MessageDTO("no dates available"));
            }
            
            var res = new Reservation()
            {
                AppUserId = User.UserGuidId(),
                CheckInDate =  reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                PropertyId = reservation.PropertyId,
                RoomId = reservation.RoomId,
                TotalPrice = reservation.CheckOutDate.Subtract(reservation.CheckInDate).Days 
                             * available.FirstOrDefault().PricePerNight
            };
             _bll.Reservations.Add(res);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, res);
        }
        
     
    }
}
