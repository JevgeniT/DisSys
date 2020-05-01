﻿using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            
            AppUser user  = new AppUser();

            Console.WriteLine(user.Email);
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower().Trim();
                var result = _db.Locations
                    .Include(location => location.LocationProperties)
                    .Where(location => location.City.ToLower().Contains(search)).ToListAsync();

                return View(await result);
            }
            
            return RedirectToAction("Index");
          
        }
    }
}