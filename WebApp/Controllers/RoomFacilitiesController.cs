using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            var appDbContext = _context.RoomFacilities.Include(r => r.Room);
            return View(await appDbContext.ToListAsync());
        }

        // GET: RoomFacilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomFacilities = await _context.RoomFacilities
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.RoomFacilitiesId == id);
            if (roomFacilities == null)
            {
                return NotFound();
            }

            return View(roomFacilities);
        }

        // GET: RoomFacilities/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "RoomId", "RoomId");
            return View();
        }

        // POST: RoomFacilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomFacilitiesId,RoomId")] RoomFacilities roomFacilities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomFacilities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "RoomId", "RoomId", roomFacilities.RoomId);
            return View(roomFacilities);
        }

        // GET: RoomFacilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "RoomId", "RoomId", roomFacilities.RoomId);
            return View(roomFacilities);
        }

        // POST: RoomFacilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomFacilitiesId,RoomId")] RoomFacilities roomFacilities)
        {
            if (id != roomFacilities.RoomFacilitiesId)
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
                    if (!RoomFacilitiesExists(roomFacilities.RoomFacilitiesId))
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
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "RoomId", "RoomId", roomFacilities.RoomId);
            return View(roomFacilities);
        }

        // GET: RoomFacilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomFacilities = await _context.RoomFacilities
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.RoomFacilitiesId == id);
            if (roomFacilities == null)
            {
                return NotFound();
            }

            return View(roomFacilities);
        }

        // POST: RoomFacilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomFacilities = await _context.RoomFacilities.FindAsync(id);
            _context.RoomFacilities.Remove(roomFacilities);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomFacilitiesExists(int id)
        {
            return _context.RoomFacilities.Any(e => e.RoomFacilitiesId == id);
        }
    }
}
