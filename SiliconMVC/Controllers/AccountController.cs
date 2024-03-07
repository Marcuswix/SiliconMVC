using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Infrastructure.Model;

namespace Infrastructure.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserServices _userServices;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly AddressServices _addressServices;

        public AccountController(UserServices userServices, SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, AddressServices addressServices)
        {
            _userServices = userServices;
            _signInManager = signInManager;
            _userManager = userManager;
            _addressServices = addressServices;
        }

        private void SetDefaultViewValues()
        {
            ViewBag.ShowDiv = false;
            ViewBag.ShowChoices = false;
        }

        //INDEX
        [Authorize]
        [HttpGet]
        [Route("/account")]
        public async Task<IActionResult> Index()
        {
            ViewBag.ShowDiv = true;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Account Details";

            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("SigIn", "Account");
            }

            var userEntity = await _userManager.GetUserAsync(User);
            var addressEntity = await _addressServices.GetOneAddresses(userEntity);

            var addressModel = new AccountDertailsAddressModel();

                if (addressEntity != null!)
                {
                    addressModel.Address = addressEntity.StreetName;
                    addressModel.Address2 = addressEntity.StreetName2;
                    addressModel.PostalCode = addressEntity.PostalCode;
                    addressModel.City = addressEntity.City;
                }

            var viewModel = new AccountDetailsViewModel
            {
                User = userEntity!,
                AddressInfo = addressModel
            };

            return View(viewModel);
        }

        #region [HttpPost] SaveBasicInfo
        [Authorize]
        [HttpPost] 
        public async Task<IActionResult> SaveBasicInfo(AccountDetailsViewModel model)
        {
            ViewBag.ShowDiv = true;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Account Details";

            var user = await _userManager.GetUserAsync(User);
                
                if (user != null)
                {
                    user.FirstName = model.BasicInfo.FirstName;
                    user.LastName = model.BasicInfo.LastName;
                    user.Email = model.BasicInfo.Email;
                    user.PhoneNumber = model.BasicInfo.Phone;
                    user.Biography = model.BasicInfo.Biography;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                    model.SuccessErrorMessage = "Data was updated successfully!";
                    return RedirectToAction("Index", "Account", model);
                     
                    }
                }

            return View(model);
        }
        #endregion

        #region [HttpPost] SaveAddressInfo
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveAddressInfo(AccountDetailsViewModel model)
        {
            ViewBag.ShowDiv = true;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Account Details";

            if (TryValidateModel(model.AddressInfo))
            {
                return RedirectToAction("Index", "Account", model);
            }

            ModelState.AddModelError("Failed to update data", "Unable to save the changed data");
            ViewData["ErrorMessage"] = "Unable tp save the changes";

            return View();
        }
        #endregion

        //SIGN-IN
        [HttpGet]
        [Route("/signin")]
        public IActionResult SignIn(string returnUrl)
        {
            SetDefaultViewValues();

            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Account");
            }

            ViewData["ReturnUrl"] = returnUrl ?? Url.Content("~/");
            return View();
        }

        [Route("/signin")]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model, string returnUrl)
        {
            SetDefaultViewValues();

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Form.Email, model.Form.Password, model.Form.RememberMe, false);
                Debug.WriteLine("PasswordSignInAsync result: " + result.Succeeded);

                if (result.Succeeded)
                { 

                    if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("Index", "Account");

                }
            }
            ModelState.AddModelError("IncorrextValues", "Incorrect email or password");
            ViewData["ErrorMessage"] = "Incorrect email or password";
            return View(model);
        }

        //SIGN-UP

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

            if(ModelState.IsValid)
            {
                var exist = await _userManager.Users.AnyAsync(x => x.Email == viewModel.Form.Email);

                if(exist == true)
                {
                    viewModel.ErrorMessage = "A user with the same email already exist";
                    return View(viewModel); 
                }

                var userEntity = new UserEntity
                {
                    FirstName = viewModel.Form.FirstName,
                    LastName = viewModel.Form.LastName,
                    Email = viewModel.Form.Email,
                    UserName = viewModel.Form.Email,
                };

                var result = await _userManager.CreateAsync(userEntity, viewModel.Form.Password);

                if(result.Succeeded)
                {
                    return RedirectToAction("SignIn", "Account");
                }
            }
            return View(viewModel); 
        }

        //SIGN_OUT
        [HttpGet]
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Account");
        }
    }
}





















//    SetDefaultViewValues();

//            if (ModelState.IsValid)
//            {
//                var result = await _userServices.CreateUser(viewModel.Form);

//                if (result.StatusCode == Infrastructure.Model.StatusCodes.OK)
//                {
//                    //Detta tar oss till en annan sida om formuläret är godkänt.
//                    return RedirectToAction("SignIn", "Account");
//}
//if (result.StatusCode == Infrastructure.Model.StatusCodes.EXISTS)
//{
//    viewModel.ErrorMessage = "A user with the same email already exist";
//}
//            }
//            return View(viewModel);
//        }










//if (ModelState.IsValid)
//{
//    var result = await _userServices.SignInUserAsync(viewModel.Form);

//    if (result != null)
//    {
//        var claims = new List<Claim>()
//                    {
//                        new(ClaimTypes.NameIdentifier, result.Id),
//                        new(ClaimTypes.Name, result.FirstName + " " + result.LastName),
//                        new(ClaimTypes.Email, result.Email),
//                        new(ClaimTypes.MobilePhone, result.Phone!),
//                        new(ClaimTypes.GivenName, result.FirstName),
//                        new(ClaimTypes.Surname, result.LastName),
//                        new(ClaimTypes.UserData, result.Biography!),
//                    };

//        await HttpContext.SignInAsync("AuthCookie", new ClaimsPrincipal(new ClaimsIdentity(claims, "AuthCookie")));
//        //Detta tar oss till en annan sida om formuläret är godkänt.
//        return RedirectToAction("Index", "Account");
//    }
//}
