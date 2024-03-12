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
        public async Task <IActionResult> Message(MessageViewModel viewModel)
        {
            ViewBag.ShowDiv = true;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Contact";

            if (ModelState.IsValid)
            {
                TempData["ThanksForYourMessage"] = "Thanks for your Message!";
                return RedirectToAction("Index");
            }

            return View("Index", viewModel);
        }

        [HttpPost]
        public async Task <IActionResult> Application(MessageViewModel model)
        {
            ViewBag.ShowDiv = true;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Contact";

            if (ModelState.IsValid) 
            {
                TempData["ThanksForYourApplication"] = "Thanks for your Application!";
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }

    }
}
