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
    public class ExtraController : Controller
    {
        private readonly AppDbContext _context;

        public ExtraController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Extra
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Extra.Include(e => e.Facility);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Extra/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extra = await _context.Extra
                .Include(e => e.Facility)
                .FirstOrDefaultAsync(m => m.ExtraId == id);
            if (extra == null)
            {
                return NotFound();
            }

            return View(extra);
        }

        // GET: Extra/Create
        public IActionResult Create()
        {
            ViewData["FacilityId"] = new SelectList(_context.Set<Facility>(), "FacilityId", "FacilityId");
            return View();
        }

        // POST: Extra/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExtraId,ExtraName,FacilityId")] Extra extra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(extra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacilityId"] = new SelectList(_context.Set<Facility>(), "FacilityId", "FacilityId", extra.FacilityId);
            return View(extra);
        }

        // GET: Extra/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extra = await _context.Extra.FindAsync(id);
            if (extra == null)
            {
                return NotFound();
            }
            ViewData["FacilityId"] = new SelectList(_context.Set<Facility>(), "FacilityId", "FacilityId", extra.FacilityId);
            return View(extra);
        }

        // POST: Extra/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExtraId,ExtraName,FacilityId")] Extra extra)
        {
            if (id != extra.ExtraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(extra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExtraExists(extra.ExtraId))
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
            ViewData["FacilityId"] = new SelectList(_context.Set<Facility>(), "FacilityId", "FacilityId", extra.FacilityId);
            return View(extra);
        }

        // GET: Extra/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extra = await _context.Extra
                .Include(e => e.Facility)
                .FirstOrDefaultAsync(m => m.ExtraId == id);
            if (extra == null)
            {
                return NotFound();
            }

            return View(extra);
        }

        // POST: Extra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var extra = await _context.Extra.FindAsync(id);
            _context.Extra.Remove(extra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExtraExists(int id)
        {
            return _context.Extra.Any(e => e.ExtraId == id);
        }
    }
}
