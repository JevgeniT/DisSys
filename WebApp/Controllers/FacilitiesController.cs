using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using BLL.App.DTO;
namespace WebApp.Controllers
{
    public class FacilitiesController : Controller
    {
        private readonly IAppBLL _bll;

        public FacilitiesController(IAppBLL context)
        {
            _bll = context;
        }

        // GET: Facilities
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Facilities.AllAsync());
        }

        // GET: Facilities/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _bll.Facilities
                .FirstOrDefaultAsync(id.Value);
            if (facility == null)
            {
                return NotFound();
            }

            return View(facility);
        }

        // GET: Facilities/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_bll.Rooms.All(), "Id", "Id");

            return View();
        }

        // POST: Facilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id, RoomId")] Facility facility)
        {
            if (ModelState.IsValid)
            {
                facility.Id = Guid.NewGuid();
                _bll.Facilities.Add(facility);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facility);
        }

        // GET: Facilities/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _bll.Facilities.FindAsync(id);
            if (facility == null)
            {
                return NotFound();
            }
            return View();
        }

        // POST: Facilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] Facility facility)
        {
            if (id != facility.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Facilities/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _bll.Facilities
                .FirstOrDefaultAsync(id.Value);
            if (facility == null)
            {
                return NotFound();
            }

            return View(facility);
        }

        // POST: Facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var facility = await _bll.Facilities.FindAsync(id);
            _bll.Facilities.Remove(facility);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
