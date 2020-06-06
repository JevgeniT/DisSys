using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO.Identity;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
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
            // return await _bll.Reservations.AllAsync(User.UserId());
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

        // PUT: api/Reservation/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(Guid id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }

            // _bll.Entry(reservation).State = EntityState.Modified;
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

        // POST: api/Reservation
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            var available = _bll.Availabilities.AllAsync();
                // Where(availability => availability.PropertyRoomId == reservation.RoomId     
                //                       && availability.From.Month == reservation.CheckInDate.Month 
                //                       && availability.From.Day>=reservation.CheckInDate.Day 
                //                       && !availability.IsUsed);
            Console.WriteLine("--");

            // foreach (var availability in available)
            // {
            //     Console.WriteLine(availability);
            // }

            Console.WriteLine("--");
            // var list = ParseDate(new List<Availability>(available), reservation.CheckInDate,reservation.CheckOutDate);
            // if (!available.Any())
            // {
            //     return BadRequest(new MessageDTO("no dates available"));
            // }
            
            
            // foreach (var availability in list)
            // {
            //     if (await _bll.Availabilities.FindAsync(availability.Id)!=null)
            //     {
            //          _bll.Availabilities.UpdateRange(availability);
            //     }
            //     else
            //     {
            //         if (availability.PropertyRoomId != reservation.PropertyRoomId)
            //         {
            //             availability.PropertyRoomId = reservation.PropertyRoomId;
            //         }
            //         
            //         await  _bll.Availabilities.AddAsync(availability);
            //     }
            //     _bll.SaveChanges();
            //
            // }
            var res = new BLL.App.DTO.Reservation()
            {
                AppUserId = User.UserGuidId(),
                CheckInDate =  reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                PropertyId = reservation.PropertyId
            };
              _bll.Reservations.Add(res);
            
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, res);
        }

        // DELETE: api/Reservation/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Reservation>> DeleteReservation(Guid id)
        // {
        //     var reservation = await _bll.Reservations.FindAsync(id);
        //     if (reservation == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _bll.Reservations.Remove(reservation);
        //     await _bll.SaveChangesAsync();
        //
        //     return reservation;
        // }
        //
       
        
        
        //TODO fix 
        public string ValidDate(DateTime In, DateTime Out)
        {
            
            DateTime from = DateTime.Parse("06-01-2020");
            DateTime to = DateTime.Parse("06-10-2020");
            if (In.CompareTo(from)>-1 && to.CompareTo(Out)>=0)
            {
                if (In.CompareTo(from)==0 || Out.CompareTo(to)==0)
                {
                    DateTime newFrom = In.CompareTo(from)==0?Out:from;
                    DateTime newTo = Out.CompareTo(to)==0?In:to;
                    return (newFrom + " " + newTo);
                } 
                if (In.CompareTo(from) == 1 && Out.CompareTo(from)==1)  
                {
                    DateTime newFrom = from;
                    DateTime newTo = In;
                     
                    DateTime newFrom2 = Out;
                    DateTime newTo2 = to;
                    return ($"{newFrom} {newTo} {newFrom2} {newTo2}");

                }
                 
            }

            return "not legit";

        }


        public List<Availability> ParseDate(List<Availability> list,DateTime From, DateTime To)
        { 
            List<Availability> result = new List<Availability>();
   
            Availability holder = new Availability();
           
            foreach (var date in list)
            {
                if (date.From>=From && (date.To <= To || date.From <= To))
                {
                    date.IsUsed = true;
                    if (date.From != From && date.To != To)
                    {
                        result.Add(date);
                                            
                    }
                }
                
            }

            Availability one, two;
            List<Availability> hold  = new List<Availability>();
            
            foreach (var available in list)
            {
                // if (d.To.Date!=To.Date && d.From.Date != From.Date )
                // {
                //     holder.From = To;
                //     holder.To = d.To;
                //     holder.IsUsed = false;
                //     holder.PropertyRoomId = d.PropertyRoomId;
                //     result.Add(holder);
                // }
                
                if (From.CompareTo(available.From)==0 || To.CompareTo(available.To)==1)
                {
                    one = new Availability();
                        
                    one.From = From.CompareTo(available.From)==0?To:available.From;
                    one.To = To.CompareTo(available.To)==0?From:available.To;
                    hold.Add(one);
                } 
                    
                if (From.CompareTo(available.From) == 1 && To.CompareTo(available.From)==1)  
                {
                    one = new Availability();
                    two = new Availability();
                    one.From = available.From;
                    one.To = From;
                    //
                    two.From = To;
                    two.To = available.To;
                    two.IsUsed = false;
                    one.IsUsed = false;

                    hold.Add(one);
                    hold.Add(two);
                    // Console.WriteLine("trr");

                }
            }
            result.AddRange(hold);
            return result;
        }
    }
}
