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
    public class ReviewController : Controller
    {
        private readonly AppDbContext _context;

        public ReviewController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Review
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Reviews.Include(r => r.AppUser).Include(r => r.Property).Include(r => r.Reservation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Review/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.AppUser)
                .Include(r => r.Property)
                .Include(r => r.Reservation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Review/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Id");
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id");
            return View();
        }

        // POST: Review/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Score,ReservationId,Comment,CreatedAt,PropertyId,AppUserId,Id,CreatedBy,ChangedBy,ChangedAt")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.Id = Guid.NewGuid();
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", review.AppUserId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Id", review.PropertyId);
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", review.ReservationId);
            return View(review);
        }

        // GET: Review/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", review.AppUserId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Id", review.PropertyId);
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", review.ReservationId);
            return View(review);
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Score,ReservationId,Comment,CreatedAt,PropertyId,AppUserId,Id,CreatedBy,ChangedBy,ChangedAt")] Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", review.AppUserId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Id", review.PropertyId);
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "Id", review.ReservationId);
            return View(review);
        }

        // GET: Review/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.AppUser)
                .Include(r => r.Property)
                .Include(r => r.Reservation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(Guid id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
