using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Newtonsoft.Json;
 
namespace WebApp.Controllers
{
    public class AvailabilityController : Controller
    {
        private readonly AppDbContext _context;

        public AvailabilityController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Availability
        public async Task<IActionResult> Index()
        {
           
            return View(await _context.Availabilities.ToListAsync());
        }

        [HttpGet]
         public async Task<ActionResult<IEnumerable<Availability>>> GetProperties()
        {
              return await _context.Availabilities.ToListAsync();
        }
        
        // GET: Availability/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availability = await _context.Availabilities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (availability == null)
            {
                return NotFound();
            }

            return View(availability);
        }

        // GET: Availability/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Rooms,"Id","Id");

            return View();
        }

        // POST: Availability/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, From , To, RoomId, PricePerNight")] Availability availability)
        {
            if (ModelState.IsValid)
            {
                availability.Id = Guid.NewGuid();
                _context.Add(availability);
                await _context.SaveChangesAsync();

                // _context.RoomAvailabilities.Add(new RoomAvailability()
                //     {RoomId = availability.PropertyRoomId, AvailabilityId = availability.Id});
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms,"Id","Id");

            return View(availability);
        }

        // GET: Availability/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availability = await _context.Availabilities.FindAsync(id);
            if (availability == null)
            {
                return NotFound();
            }
            return View(availability);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,From,To")] Availability availability)
        {
            if (id != availability.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(availability);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvailabilityExists(availability.Id))
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
            return View(availability);
        }

        // GET: Availability/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availability = await _context.Availabilities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (availability == null)
            {
                return NotFound();
            }

            return View(availability);
        }

        // POST: Availability/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var availability = await _context.Availabilities.FindAsync(id);
            _context.Availabilities.Remove(availability);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvailabilityExists(Guid id)
        {
            return _context.Availabilities.Any(e => e.Id == id);
        }
    }
}
