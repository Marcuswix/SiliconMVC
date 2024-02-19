using Microsoft.AspNetCore.Mvc;

namespace SiliconMVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Contact";
            return View();
        }
    }
}
