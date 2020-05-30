﻿  using System.Threading.Tasks;
using DAL.App.EF;
 using Microsoft.AspNetCore.Mvc;
 
namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _db;
        public string? Search { get; set; } = ""; 
        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public  IActionResult Index()
        {
            return View();
        }


        public async Task <IActionResult> Find (string? search)
        {
            Search = search;
 
            // if (!string.IsNullOrWhiteSpace(search))
            // {
            //     // search = search.ToLower().Trim();
            //     // var result = _db.Properties
            //     //      
            //     //     .Where(p.location => location.City.ToLower().Contains(search)).ToListAsync();
            //
            //     return View(await result);
            // }
                
            return RedirectToAction("Index");
          
        }
    }
}