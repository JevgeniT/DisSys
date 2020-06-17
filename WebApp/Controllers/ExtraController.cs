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
            var appDbContext = _context.Extras.Include(e => e.Facility);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Extra/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extra = await _context.Extras
                .Include(e => e.Facility)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (extra == null)
            {
                return NotFound();
            }

            return View(extra);
        }

        // GET: Extra/Create
        public IActionResult Create()
        {
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Id");
            return View();
        }

        // POST: Extra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,FacilityId,Id")] Extra extra)
        {
            if (ModelState.IsValid)
            {
                extra.Id = Guid.NewGuid();
                _context.Add(extra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Id", extra.FacilityId);
            return View(extra);
        }

        // GET: Extra/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extra = await _context.Extras.FindAsync(id);
            if (extra == null)
            {
                return NotFound();
            }
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Id", extra.FacilityId);
            return View(extra);
        }

        // POST: Extra/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,FacilityId,Id")] Extra extra)
        {
            if (id != extra.Id)
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
                    if (!ExtraExists(extra.Id))
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
            ViewData["FacilityId"] = new SelectList(_context.Facilities, "Id", "Id", extra.FacilityId);
            return View(extra);
        }

        // GET: Extra/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extra = await _context.Extras
                .Include(e => e.Facility)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (extra == null)
            {
                return NotFound();
            }

            return View(extra);
        }

        // POST: Extra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var extra = await _context.Extras.FindAsync(id);
            _context.Extras.Remove(extra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExtraExists(Guid id)
        {
            return _context.Extras.Any(e => e.Id == id);
        }
    }
}
