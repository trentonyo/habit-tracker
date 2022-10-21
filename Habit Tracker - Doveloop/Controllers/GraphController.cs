using Microsoft.AspNetCore.Mvc;

namespace Habit_Tracker___Doveloop.Controllers
{
    public class GraphController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
