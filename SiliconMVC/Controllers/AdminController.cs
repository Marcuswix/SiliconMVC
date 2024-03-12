using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SiliconMVC.Controllers
{
    [Authorize(Policy = "Admins")]
    //[Authorize(Roles = "Admin")] //Detta gör så bara de som har roll som admin kommer åt denna sida...
    public class AdminController : Controller
    {
        [HttpGet]
        [Route("/admin")]
        public IActionResult Index()
        {
            ViewBag.ShowDiv = true;
            ViewBag.ShowChoices = false;
            return View();
        }

        [Authorize(Policy = "CIO")]
        public IActionResult Settings() 
        {
            ViewBag.ShowDiv = true;
            ViewBag.ShowChoices = false;
            return View();
        }
    }
}
