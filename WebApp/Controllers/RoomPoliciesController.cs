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
    public class RoomPoliciesController : Controller
    {
        private readonly AppDbContext _context;

        public RoomPoliciesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RoomPolicies
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.RoomPolicies.Include(r => r.Policy).Include(r => r.Room);
            return View(await appDbContext.ToListAsync());
        }

        // GET: RoomPolicies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomPolicies = await _context.RoomPolicies
                .Include(r => r.Policy)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.RoomPoliciesId == id);
            if (roomPolicies == null)
            {
                return NotFound();
            }

            return View(roomPolicies);
        }

        // GET: RoomPolicies/Create
        public IActionResult Create()
        {
            ViewData["PolicyId"] = new SelectList(_context.Policy, "PolicyId", "PolicyId");
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "RoomId", "RoomId");
            return View();
        }

        // POST: RoomPolicies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomPoliciesId,RoomId,PolicyId")] RoomPolicies roomPolicies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomPolicies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PolicyId"] = new SelectList(_context.Policy, "PolicyId", "PolicyId", roomPolicies.PolicyId);
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "RoomId", "RoomId", roomPolicies.RoomId);
            return View(roomPolicies);
        }

        // GET: RoomPolicies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomPolicies = await _context.RoomPolicies.FindAsync(id);
            if (roomPolicies == null)
            {
                return NotFound();
            }
            ViewData["PolicyId"] = new SelectList(_context.Policy, "PolicyId", "PolicyId", roomPolicies.PolicyId);
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "RoomId", "RoomId", roomPolicies.RoomId);
            return View(roomPolicies);
        }

        // POST: RoomPolicies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomPoliciesId,RoomId,PolicyId")] RoomPolicies roomPolicies)
        {
            if (id != roomPolicies.RoomPoliciesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomPolicies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomPoliciesExists(roomPolicies.RoomPoliciesId))
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
            ViewData["PolicyId"] = new SelectList(_context.Policy, "PolicyId", "PolicyId", roomPolicies.PolicyId);
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "RoomId", "RoomId", roomPolicies.RoomId);
            return View(roomPolicies);
        }

        // GET: RoomPolicies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomPolicies = await _context.RoomPolicies
                .Include(r => r.Policy)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.RoomPoliciesId == id);
            if (roomPolicies == null)
            {
                return NotFound();
            }

            return View(roomPolicies);
        }

        // POST: RoomPolicies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomPolicies = await _context.RoomPolicies.FindAsync(id);
            _context.RoomPolicies.Remove(roomPolicies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomPoliciesExists(int id)
        {
            return _context.RoomPolicies.Any(e => e.RoomPoliciesId == id);
        }
    }
}
