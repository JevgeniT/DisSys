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
    public class RoomFacilitiesController : Controller
    {
        private readonly AppDbContext _context;

        public RoomFacilitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RoomFacilities
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.RoomFacilities.Include(r => r.Facility).Include(r => r.PropertyRooms);
            return View(await appDbContext.ToListAsync());
        }

        // GET: RoomFacilities/Details/5    
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomFacilities = await _context.RoomFacilities
                .Include(r => r.Facility)
                .Include(r => r.PropertyRoomsId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomFacilities == null)
            {
                return NotFound();
            }

            return View(roomFacilities);
        }

        // GET: RoomFacilities/Create
        public IActionResult Create()
        {
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Name");
            ViewData["RoomId"] = new SelectList(_context.PropertyRooms, "Id", "PropertyRoomsId");
            return View();
        }

        // POST: RoomFacilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacilityId,PropertyRoomsId,Id")] RoomFacilities roomFacilities)
        {
            if (ModelState.IsValid)
            {
                roomFacilities.Id = Guid.NewGuid();
                _context.Add(roomFacilities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Name", roomFacilities.FacilityId);
            ViewData["RoomId"] = new SelectList(_context.PropertyRooms, "Id", "PropertyRoomsId", roomFacilities.PropertyRoomsId);
            return View(roomFacilities);
        }

        // GET: RoomFacilities/Edit/5
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
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Name", roomFacilities.FacilityId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "RoomName", roomFacilities.PropertyRoomsId);
            return View(roomFacilities);
        }

        // POST: RoomFacilities/Edit/5
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
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Name", roomFacilities.FacilityId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "RoomName", roomFacilities.PropertyRoomsId);
            return View(roomFacilities);
        }

        // GET: RoomFacilities/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomFacilities = await _context.RoomFacilities
                .Include(r => r.Facility)
                .Include(r => r.PropertyRoomsId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomFacilities == null)
            {
                return NotFound();
            }

            return View(roomFacilities);
        }

        // POST: RoomFacilities/Delete/5
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
