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
        [HttpGet]
        public IActionResult SignIn()
        {
            var viewModel = new SignInViewModel();
            ViewBag.ShowDiv = false;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Sign In";
            return View(viewModel);
        }

        [Route("/signin")]
        [HttpPost]
        public IActionResult SignIn(SignInViewModel viewModel)
        {
            ViewBag.ShowDiv = false;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Sign In";
            viewModel.ErrorMessage = "Incorrect email or password";

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Account", "Index");
            }
            

        }

        [Route("/signup")]
        [HttpGet]
        public IActionResult SignUp() 
        {
            var viewModel = new SignUpViewModel();
            ViewBag.ShowDiv = false;
            ViewBag.ShowChoices = false; 
            ViewData["Title"] = "Sign Up";
            return View(viewModel);
        }

        [Route("/signup")]
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel viewModel)
        {
            ViewBag.ShowDiv = false;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Sign Up";

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            //Detta tar oss till en annan sida om formuläret inte är godkänt.

            return RedirectToAction("SignIn", "Account");

        }
    }
}
