using Microsoft.AspNetCore.Mvc;
using SiliconMVC.ViewModels;

namespace SiliconMVC.Controllers
{
    public class AccountController : Controller
    {
        //IActionResult gör det möjligt att få tillgång till olika vyer och olika statuskoder. 
        public IActionResult Index()
        {
            ViewBag.ShowDiv = false;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Profile";
            return View();
        }

        [Route("/signin")]
        public IActionResult SignIn()
        {

            ViewBag.ShowDiv = false;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Sign In";
            return View();
        }

        [Route("/signup")]
        [HttpGet]
        public IActionResult SignUp() 
        {
            var viewModel = new SignUpViewModel();
            ViewBag.ShowDiv = false;
            ViewBag.ShowChoices = false; 
            ViewData["Title"] = "Sign Up";
            return View();
        }

        [Route("/signup")]
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel viewModel)
        {
            if(ModelState.IsValid == false)
            {
                return View(viewModel);
            }
            //Detta tar oss till en annan sida om formuläret inte är godkänt.

            return RedirectToAction("SignIn", "Account");

        }
    }
}
