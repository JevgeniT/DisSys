  using System;
  using System.Threading.Tasks;
using DAL.App.EF;
  using Domain.Identity;
  using Extensions;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Mvc;
 
namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _db;
        private UserManager<AppUser> _manager;
        
        public HomeController(AppDbContext db, UserManager<AppUser> manager)
        {
            _db = db;
            _manager = manager;
        }


        public  IActionResult Index()
        {
            return View();
        }

        public async Task <IActionResult> Find ()
        {
          
            return RedirectToAction("Index");
          
        }
    }
}