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
    public class PolicyController : Controller
    {
        private readonly AppDbContext _context;

        public PolicyController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Policy
        public async Task<IActionResult> Index()
        {
            return View(await _context.Policies.ToListAsync());
        }

        // GET: Policy/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policy = await _context.Policies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policy == null)
            {
                return NotFound();
            }

            return View(policy);
        }

        // GET: Policy/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Policy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PolicyName,PrepaymentBefore,CancellationBefore")] Policy policy)
        {
            if (ModelState.IsValid)
            {
                policy.Id = Guid.NewGuid();
                _context.Add(policy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(policy);
        }

        // GET: Policy/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policy = await _context.Policies.FindAsync(id);
            if (policy == null)
            {
                return NotFound();
            }
            return View(policy);
        }

        // POST: Policy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PolicyName,PrepaymentBefore,CancellationBefore")] Policy policy)
        {
            if (id != policy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyExists(policy.Id))
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
            return View(policy);
        }

        // GET: Policy/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policy = await _context.Policies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policy == null)
            {
                return NotFound();
            }

            return View(policy);
        }

        // POST: Policy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var policy = await _context.Policies.FindAsync(id);
            _context.Policies.Remove(policy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicyExists(Guid id)
        {
            return _context.Policies.Any(e => e.Id == id);
        }
    }
}
