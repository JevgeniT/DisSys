#pragma warning disable 1591
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }

    }
}

//sudo docker run -it -v mongodata:/data/db -p 27017:27017 --name dissys -d mongo