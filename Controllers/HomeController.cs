using Microsoft.AspNetCore.Mvc;
using Part2.Models;
using System.Diagnostics;

namespace Part2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Index Method called. Return the Index view from the Home folder.
        //This will return the Home Page
        public IActionResult Index()
        {
            return View();
        }

        //AboutUs Method called. Return the AboutUs view from the Home folder.
        //This will return the About Us Page
        public IActionResult AboutUs()
        {
            return View();
        }

        //ContactUs Method called. Return the ContactUs view from the Home folder.
        //This will return the ContactUs Page
        public IActionResult ContactUs()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

// CODE ATTRIBUTION
//Anderson, R (2024) Get started with ASP.NET Core MVC [SourceCode]. https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-8.0&tabs=visual-studio


