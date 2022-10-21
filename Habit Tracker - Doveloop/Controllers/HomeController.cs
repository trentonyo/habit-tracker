using Habit_Tracker___Doveloop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Habit_Tracker___Doveloop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //HttpContext.User.Identity.IsAuthenticated;
            ViewBag.UserName = HttpContext.User.Identity.Name;
            return View();
        }

        public IActionResult Privacy()
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