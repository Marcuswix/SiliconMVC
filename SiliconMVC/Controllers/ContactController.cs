using Microsoft.AspNetCore.Mvc;
using SiliconMVC.ViewModels;
using System.Reflection;

namespace SiliconMVC.Controllers
{
    public class ContactController : Controller
    {
        [Route("/contact")]
        public IActionResult Index()
        {
            ViewBag.ShowDiv = true;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Contact";
            return View();
        }


        [HttpPost]
        [Route("/contact")]
        public IActionResult Index(MessageViewModel viewModel)
        {
            ViewBag.ShowDiv = true;
            ViewData["Title"] = "Contact";

            if (ModelState.IsValid)
            {

            }
                return View();
        }
    }
}
