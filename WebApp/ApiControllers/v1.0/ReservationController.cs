using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Public.DTO;
using Public.DTO.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    ///  Reservations
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReservationController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private DTOMapper<Reservation,ReservationDTO> _mapper = new DTOMapper<Reservation,ReservationDTO>();
        
        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ReservationController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get all property reservations
        /// </summary>
        /// <param name="pId">Property Id</param>
        /// <returns>Arrays of reservations</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations([FromQuery]Guid? pId)
        {
            var reservations = (await _bll.Reservations.AllForPropertyAsync(User.UserGuidId(), pId))
                .Select(res => _mapper.Map(res));
            return Ok(reservations);
        }
        
        /// <summary>
        /// Get single Reservation
        /// </summary>
        /// <param name="id">Reservation Id</param>
        /// <returns>Reservation object</returns>
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

        /// <summary>
        /// Update Reservation
        /// </summary>
        /// <param name="id">Reservation Id</param>
        /// <param name="reservation"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(Guid id, ReservationDTO reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }
            
            if (!await _bll.Reservations.ExistsAsync(id))
            {
                return BadRequest();
            }
            
            _bll.Reservations.Update(_mapper.Map(reservation));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        
        /// <summary>
        /// Create Reservation
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservation)
        {
          
            var bllReservation = _mapper.Map(reservation);
            
            var available = await _bll.Availabilities.FindAvailableDates(bllReservation.CheckInDate,bllReservation.CheckOutDate,bllReservation.PropertyId);
            
            _bll.Availabilities.ParseDate(new List<Availability>(available.ToList()), bllReservation.CheckInDate, bllReservation.CheckOutDate);
            
            if (!available.Any())
            {
                return BadRequest(new MessageDTO("No dates available"));
            }

            _bll.Reservations.Add(bllReservation);
            await _bll.SaveChangesAsync();

            reservation.Id = bllReservation.Id;
            
            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }
        
    }
}
