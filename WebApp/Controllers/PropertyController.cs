using System;
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
    // [Authorize]
    // [Authorize(Roles = "host")]
    public class PropertyController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PropertyController(IAppUnitOfWork context)
        {
            _uow = context;
        }

        // GET: Property
        public async Task<IActionResult> Index()
        {
            var appDbContext = _uow.Properties.AllAsync();
            
            
                // _uow.Properties.Include(property=> property.PropertyLocation).Include(property => property.PropertyRooms);
            return View(await appDbContext);
        }

        // GET: Property/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _uow.Properties.FirstOrDefaultAsync(id.Value);
                // .Include(p=> p.PropertyLocation)
                // .FirstOrDefaultAsync(m => m.Id == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // GET: Property/Create
        public IActionResult Create()
        {
            ViewData["PropertyLocationId"] = new SelectList(_uow.Locations.All(), "Id", "City");
            ViewData["PropertyType"] = new SelectList(Enum.GetNames(typeof(PropertyType)));
            
            

            return View();
        }

        // POST: Property/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropertyName,Address, PropertyLocationId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Property @property)
        {
            string messages = string.Join("; ", ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));
            Console.WriteLine(messages);
            if (ModelState.IsValid)
            {
                _uow.Properties.Add(@property);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PropertyLocationId"] = new SelectList(_uow.Locations.All(), "Id", "City", @property.PropertyLocationId);
            return View(@property);
        }

        // GET: Property/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _uow.Properties.FindAsync(id);
            if (@property == null)
            {
                return NotFound();
            }
            ViewData["PropertyLocationId"] = new SelectList(_uow.Locations.All(), "Id", "City", @property.PropertyLocationId);
            return View(@property);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PropertyName,PropertyLocationId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Property @property)
        {
            if (id != @property.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Properties.Update(@property);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Properties.ExistsAsync(property.Id))
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
            ViewData["PropertyLocationId"] = new SelectList(_uow.Locations.All(), "Id", "City", @property.PropertyLocationId);
            return View(@property);
        }

        // GET: Property/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _uow.Properties.FindAsync(id.Value);
                // .Include(property=> property.PropertyLocation)
                // .FirstOrDefaultAsync(m => m.Id == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // POST: Property/Delete/5    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var @property = await _uow.Properties.FindAsync(id);
            _uow.Properties.Remove(@property);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // private bool PropertyExists(int id)
        // {
        //     return _uow.Properties.ExistsAsync(id);
        // }
    }
}
