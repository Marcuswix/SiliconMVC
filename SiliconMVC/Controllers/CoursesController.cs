using Microsoft.AspNetCore.Mvc;

namespace SiliconMVC.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ShowDiv = true;
            ViewData["Title"] = "Our Courses";
            return View();
        }
    }
}
