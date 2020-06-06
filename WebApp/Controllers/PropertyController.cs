using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
 

namespace WebApp.Controllers
{
    [Authorize]
    [Authorize(Roles = "host")]
    public class PropertyController : Controller
    {
        private readonly AppDbContext _uow;
        private readonly UserManager<AppUser> _userManager;
        public PropertyController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _uow = context;
            _userManager = userManager;
        }

        // GET: Property
        public async Task<IActionResult> Index()
        {
            var property = _uow.Properties.Include(property1 => property1.PropertyRooms).ToListAsync();
            
            
                // _uow.Properties.Include(property=> property.PropertyLocation).Include(property => property.PropertyRooms);
            return View( await property  );
        }

        // GET: Property/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property =   _uow.Properties.Find(id.Value);
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
          
            
            ViewData["PropertyType"] = new SelectList(Enum.GetNames(typeof(PropertyType)));
            
            

            return View();
        }

        // POST: Property/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropertyName,Address, PropertyLocation,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Property @property)
        {
             
            
            var userId = _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                            // string[] location = property.PropertyLocationId.ToString().Split(",");
            
            if (ModelState.IsValid)
            {
                
                property.AppUserId = userId.Result.Id;
                _uow.Properties.Add(@property);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["PropertyLocationId"] = new SelectList(_uow.Locations, "Id", "City", @property.PropertyLocationId);
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
            // ViewData["PropertyLocationId"] = new SelectList(_uow.Locations, "Id", "City", @property.PropertyLocationId);
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
                    // if (!await _uow.Properties.Find(property.Id))
                    // {
                    //     return NotFound();
                    // }
                    // else
                    // {
                    //     throw;
                    // }
                }
                return RedirectToAction(nameof(Index));
            }
            // ViewData["PropertyLocationId"] = new SelectList(_uow.Locations, "Id", "City", @property.PropertyLocationId);
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
 
    }
}
// dotnet aspnet-codegenerator controller -name ReservationController  -m Reservation  -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
