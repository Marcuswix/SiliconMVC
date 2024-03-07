using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ShowDiv = true;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Our Courses";
            return View();
        }
    }
}
