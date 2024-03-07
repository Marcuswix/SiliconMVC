using Microsoft.AspNetCore.Mvc;
using Infrastructure.ViewModels;
using System.Reflection;

namespace Infrastructure.Controllers
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
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Contact";

            if (ModelState.IsValid)
            {
                return View();
            }

            return View();
        }
    }
}
