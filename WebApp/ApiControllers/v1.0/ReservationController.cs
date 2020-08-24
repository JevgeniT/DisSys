using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Internal;
using Public.DTO;
using Public.DTO.Mappers;

namespace WebApp.ApiControllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private DTOMapper<Reservation,ReservationDTO> _mapper = new DTOMapper<Reservation,ReservationDTO>();
        
        public ReservationController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Reservation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
        {
            var reservations = (await _bll.Reservations.AllAsync(User.UserGuidId()))
                .Select(res => _mapper.Map(res));
            return Ok(reservations);

        }

        [HttpGet][Route("property/{propertyId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "host")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetPropertyReservations(Guid? propertyId)
        {
            var reservations = (await _bll.Reservations.AllForPropertyAsync(User.UserGuidId(), propertyId))
                .Select(res => _mapper.Map(res));
            return Ok(reservations);

        }
        // GET: api/Reservation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDTO>> GetReservation(Guid id)
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
        public async Task<ActionResult<ReservationDTO>> PostReservation(Reservation reservation)
        {
            var available = await _bll.Availabilities.FindAvailableDates(reservation.CheckInDate,reservation.CheckOutDate,reservation.PropertyId);
            _bll.Availabilities .ParseDate(new List<Availability>(available.ToList()), reservation.CheckInDate,reservation.CheckOutDate);
            
            if (!available.Any())
            {
                return BadRequest(new MessageDTO("No dates available"));
            }

            var res = new Reservation()
            {
                AppUserId = User.UserGuidId(),
                CheckInDate =  reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                PropertyId = reservation.PropertyId,
                RoomId = reservation.RoomId,
                TotalPrice = reservation.CheckOutDate.Subtract(reservation.CheckInDate).Days 
                             * available.FirstOrDefault().PricePerNightForAdult
            };
             _bll.Reservations.Add(res);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, res);
        }
        
     
    }
}
