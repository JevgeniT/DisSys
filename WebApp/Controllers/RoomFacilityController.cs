using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class RoomFacilityController : Controller
    {
        private readonly AppDbContext _context;

        public RoomFacilityController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RoomFacility
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.RoomFacilities.Include(r => r.Facility).Include(r => r.Room);
            return View(await appDbContext.ToListAsync());
        }

        // GET: RoomFacility/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomFacilities = await _context.RoomFacilities
                .Include(r => r.Facility)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomFacilities == null)
            {
                return NotFound();
            }

            return View(roomFacilities);
        }

        // GET: RoomFacility/Create
        public IActionResult Create()
        {
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Id");
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id");
            return View();
        }

        // POST: RoomFacility/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacilityId,RoomId,Id")] RoomFacilities roomFacilities)
        {
            if (ModelState.IsValid)
            {
                roomFacilities.Id = Guid.NewGuid();
                _context.Add(roomFacilities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Id", roomFacilities.FacilityId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", roomFacilities.RoomId);
            return View(roomFacilities);
        }

        // GET: RoomFacility/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomFacilities = await _context.RoomFacilities.FindAsync(id);
            if (roomFacilities == null)
            {
                return NotFound();
            }
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Id", roomFacilities.FacilityId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", roomFacilities.RoomId);
            return View(roomFacilities);
        }

        // POST: RoomFacility/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FacilityId,RoomId,Id")] RoomFacilities roomFacilities)
        {
            if (id != roomFacilities.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomFacilities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomFacilitiesExists(roomFacilities.Id))
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
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Id", roomFacilities.FacilityId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", roomFacilities.RoomId);
            return View(roomFacilities);
        }

        // GET: RoomFacility/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomFacilities = await _context.RoomFacilities
                .Include(r => r.Facility)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomFacilities == null)
            {
                return NotFound();
            }

            return View(roomFacilities);
        }

        // POST: RoomFacility/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var roomFacilities = await _context.RoomFacilities.FindAsync(id);
            _context.RoomFacilities.Remove(roomFacilities);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomFacilitiesExists(Guid id)
        {
            return _context.RoomFacilities.Any(e => e.Id == id);
        }
    }
}
