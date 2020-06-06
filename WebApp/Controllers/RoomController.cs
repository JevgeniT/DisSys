using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BLL.App.DTO;

using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]

    public class RoomController : Controller
    {
        private readonly IAppBLL _uow;

        public RoomController(IAppBLL uow)
        {
            _uow = uow;
        }

        // GET: Room
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Rooms.AllAsync());
        }

        // GET: Room/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _uow.Rooms
                .FirstOrDefaultAsync(id.Value);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Room/Create
        public IActionResult Create()
        {
             ViewData["RoomPropertyId"] = new SelectList(_uow.Properties.All(), "Id", "PropertyName");
 
            return View();
        }

        // POST: Room/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomName,RoomCapacity,RoomSize,RoomPropertyId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Room room)
        {
     
            if (ModelState.IsValid)
            {
                _uow.Rooms.Add(room);
                await _uow.SaveChangesAsync();
                // _uow.PropertyRoomsService.Add(new PropertyRooms(){RoomId = room.Id,PropertyId = room.RoomPropertyId});
                // await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomPropertyId"] = new SelectList(_uow.Properties.All(), "Id", "PropertyName", room.RoomPropertyId);

            return View(room);
        }

        // GET: Room/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _uow.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: Room/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RoomName,RoomCapacity,RoomSize,RoomPropertyId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Rooms.Update(room);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await  _uow.Rooms.ExistsAsync(room.Id))
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
            return View(room);
        }

        // GET: Room/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _uow.Rooms
                .FirstOrDefaultAsync(id.Value);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var room = await _uow.Rooms.FindAsync(id);
            _uow.Rooms.Remove(room);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
// dotnet aspnet-codegenerator controller -name FacilitiesController  -actions -m Domain.Facility  -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
// dotnet aspnet-codegenerator controller -name RoomFacilitiesController  -actions -m Domain.RoomFacilities  -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
