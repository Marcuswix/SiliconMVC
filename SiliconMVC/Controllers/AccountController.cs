using Microsoft.AspNetCore.Mvc;
using SiliconMVC.Models;
using SiliconMVC.ViewModels;

namespace SiliconMVC.Controllers
{
    public class AccountController : Controller
    {
        private void SetDefaultViewValues(string title)
        {
            ViewBag.ShowDiv = false;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = title;
        }

        [Route("/account")]
        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new AccountDetailsViewModel();
            ViewBag.ShowDiv = true;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Account Details";
            //viewModel.BasicInfo = _accountService.GetBasicInfo();
            //viewModel.AddressInfo = _accountService.GetAddressInfo(); 
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult BasicInfo(AccountDetailsViewModel viewModel)
        {
            //_accountService.SaveBasicInfo(viewModel.BasicInfo)
            return RedirectToAction("Index", viewModel);
        }

        [HttpPost]
        public IActionResult AddressInfo(AccountDetailsViewModel viewModel)
        {
            //_accountService.SaveAddressInfo(viewModel.AddressInfo)
            return RedirectToAction("Index", viewModel);
        }

        [Route("/signin")]
        [HttpGet]
        public IActionResult SignIn()
        {
            var viewModel = new SignInViewModel();
            SetDefaultViewValues("Sign In");

            if(ModelState.IsValid)
            {
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", viewModel);
            }

        }

        [Route("/signin")]
        [HttpPost]
        public IActionResult SignIn(SignInViewModel viewModel)
        {
            SetDefaultViewValues("Sign In");
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
            SetDefaultViewValues("Sign Up");
            return View(viewModel);
        }

        [Route("/signup")]
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel viewModel)
        {
            SetDefaultViewValues("Sign Up");

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            //Detta tar oss till en annan sida om formuläret inte är godkänt.
            return RedirectToAction("SignIn", "Account");
        }
    }
}
