using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]

    public class LocationController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public LocationController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Location
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Locations.AllAsync());
        }

        // GET: Location/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _uow.Locations
                .FirstOrDefaultAsync(id.Value);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Location/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Country,City,Address,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Location location)
        {
            if (ModelState.IsValid)
            {
                _uow.Locations.Add(location);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        // GET: Location/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _uow.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // POST: Location/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Country,City,Address,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Location location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Locations.Update(location);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Locations.ExistsAsync(location.Id))
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
            return View(location);
        }

        // GET: Location/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _uow.Locations
                .FirstOrDefaultAsync(id.Value);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _uow.Locations.FindAsync(id);
            _uow.Locations.Remove(location);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

      
    }
}
