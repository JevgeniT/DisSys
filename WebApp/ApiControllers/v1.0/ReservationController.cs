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
using Microsoft.AspNetCore.Http;
using Public.DTO;
using Public.DTO.Mappers;
using Public.DTO.Reservation;

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
        private readonly ReservationMapper _mapper = new   ReservationMapper();
        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ReservationController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get all reservations
        /// </summary>
        /// <param name="pId">Property Id if present</param>
        /// <returns>Arrays of reservations</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReservationDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<IEnumerable<ReservationPreviewDTO>>> GetReservations([FromQuery]Guid? pId)
        {
            var reservations = (await _bll.Reservations.AllAsync(User.UserGuidId(), pId))
                .Select(res => _mapper.MapPreviewDto(res));
            
            if (reservations is null)
            {
                return NotFound(new MessageDTO("Nothing was found"));
            }
 
            return Ok(reservations);
        }
        
        
        /// <summary>
        /// Get single Reservation
        /// </summary>
        /// <param name="id">Reservation Id</param>
        /// <returns>Reservation object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReservationDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))] 
        public async Task<ActionResult<ReservationDTO>> GetReservation(Guid id)
        {
            var reservation = await _bll.Reservations.FirstOrDefaultAsync(id, User.UserGuidId());
 
            if (reservation is null)
            {
                return NotFound(new MessageDTO($"Reservation with id {id} was not found"));
            }
            return Ok(_mapper.Map(reservation));
        }

        /// <summary>
        /// Update Reservation
        /// </summary>
        /// <param name="id">Reservation Id</param>
        /// <param name="reservation"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutReservation(Guid id, ReservationDTO reservation)
        {
            if (id != reservation.Id)
            { 
                return BadRequest(new MessageDTO("Ids does not match!"));
            }

            var bll = _mapper.Map(reservation);
            bll.AppUserId = User.UserGuidId();
            await _bll.Reservations.UpdateAsync(bll, User.UserGuidId());
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        
        /// <summary>
        /// Create Reservation
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReservationDTO))]
        public async Task<ActionResult<ReservationDTO>> PostReservation(ReservationCreateDTO reservation)
        {
            if (!await _bll.Availabilities.ExistsAsync(reservation.CheckInDate, reservation.CheckOutDate,
                reservation.RoomDtos!.Select(r=>r.RoomId).ToList()))
            {
                return BadRequest(new MessageDTO("No dates available"));
            }

            Reservation bllReservation = _mapper.MapCreateDto(reservation);
            bllReservation.AppUserId = User.UserGuidId();
            _bll.Reservations.Add(bllReservation);
            await _bll.Availabilities.UpdateReservationDatesAsync(bllReservation);
            await _bll.SaveChangesAsync();
            return CreatedAtAction("GetReservation", new { id = bllReservation.Id }, bllReservation);
        }
        
    }
}
