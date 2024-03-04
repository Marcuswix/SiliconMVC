using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using SiliconMVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SiliconMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserServices _userServices;
        private readonly AccountServices _accountServices;

        public AccountController(UserServices userServices, AccountServices accountServices)
        {
            _userServices = userServices;
            _accountServices = accountServices;
        }

        private void SetDefaultViewValues()
        {
            ViewBag.ShowDiv = false;
            ViewBag.ShowChoices = false;
        }

        [Authorize]
        [Route("/account")]
        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new AccountDetailsViewModel();
            ViewBag.ShowDiv = true;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Account Details";
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
            SetDefaultViewValues();
            return View();
        }

        [Route("/signin")]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel viewModel)
        {
            SetDefaultViewValues();

            if (ModelState.IsValid)
            {
                var result = await _userServices.SignInUserAsync(viewModel.Form);

                if (result != null)
                {
                    var claims = new List<Claim>()
                    {
                        new(ClaimTypes.NameIdentifier, result.Id),
                        new(ClaimTypes.Name, result.FirstName + " " + result.LastName),
                        new(ClaimTypes.Email, result.Email),
                        new(ClaimTypes.MobilePhone, result.Phone!),
                        new(ClaimTypes.GivenName, result.FirstName),
                        new(ClaimTypes.Surname, result.LastName),
                        new(ClaimTypes.UserData, result.Biography!),
                    };

                    await HttpContext.SignInAsync("AuthCookie", new ClaimsPrincipal(new ClaimsIdentity(claims, "AuthCookie")));
                    //Detta tar oss till en annan sida om formuläret är godkänt.
                    return RedirectToAction("Index", "Account");
                }
            }

            ViewData["ErrorMessage"] = "Incorrect email or password";
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Update(AccountDetailsViewModel model)
        //{
        //    SetDefaultViewValues();

        //    if (ModelState.IsValid)
        //    {


        //        var result = await _userServices.UpdateUserInfo(model);

        //        if (result.StatusCode == Infrastructure.Model.StatusCodes.OK)
        //        {
        //            return RedirectToAction("Index", "Account");
        //        }
        //    }
        //    return RedirectToAction("Index", "Account");
        //}


        [Route("/signup")]
        [HttpGet]
        public IActionResult SignUp()
        {
            var viewModel = new SignUpViewModel();
            SetDefaultViewValues();
            return View(viewModel);
        }

        [Route("/signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {
            SetDefaultViewValues();

            if (ModelState.IsValid)
            {
                var result = await _userServices.CreateUser(viewModel.Form);

                if (result.StatusCode == Infrastructure.Model.StatusCodes.OK)
                {
                    //Detta tar oss till en annan sida om formuläret är godkänt.
                    return RedirectToAction("SignIn", "Account");
                }
                if(result.StatusCode == Infrastructure.Model.StatusCodes.EXISTS)
                {
                    viewModel.ErrorMessage = "A user with the same email already exist";
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public new async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("SignIn", "Account");
        }
    }
}
