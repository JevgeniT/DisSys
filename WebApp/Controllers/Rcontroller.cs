using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    [Authorize]
    public class RController : Controller
    {
        private readonly AppDbContext _context;
        public UserManager<AppUser> _userManager { get; set; }
        public RController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reservation
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result.Id;

            var appDbContext = _context.Reservations.Where(reservation => reservation.AppUserId == userId);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
             
            var reservation = await _context.Reservations
                .Include(r => r.AppUserId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservation/Create
        public IActionResult Create()
        {
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "PropertyName");
            ViewData["RoomId"] = new SelectList(_context.Rooms,"Id","RoomName");
            // Property property = new Property().id;
             return View();
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CheckIn,CheckInDate,CheckOutDate," +
                                                      "PropertyId,DeletedAt,Id,RoomId")]
                                                        Reservation reservation)
        {
         
            if (ModelState.IsValid)
            {
                var userId = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result.Id;
                reservation.AppUserId = userId;
                
                 _context.Add(reservation);
                 await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms,"Id","RoomName");

         
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "PropertyName", reservation.PropertyId);

            return View(reservation);
        }

        // GET: Reservation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ReservationNumber,MadeAt,CheckIn,CheckInDate,CheckOutDate,GuestReservationsId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
             return View(reservation);
        }

        // GET: Reservation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.AppUserId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(Guid id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
