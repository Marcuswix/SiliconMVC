using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Entities;
using Infrastructure.Model;
using System.Security.Claims;

namespace Infrastructure.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly AddressServices _addressServices;

        public AccountController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, AddressServices addressServices)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _addressServices = addressServices;
        }

        private void SetDefaultViewValues()
        {
            ViewBag.ShowDiv = false;
            ViewBag.ShowChoices = false;
        }


        #region [HttpGet] Index
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

            var userInfoResult = await UserInfo();

            if (userInfoResult is ViewResult userInfoView)
            {
                ViewData["UserInfo"] = userInfoView.Model;
            }

            var basicInfoResult = await BasicInfo();

            if (basicInfoResult is ViewResult basicInfoView)
            {
                ViewData["BasicInfo"] = basicInfoView.Model;
            }

            // Hämta och rendera AddressInfo
            var addressInfoResult = await AddressInfo();

            if (addressInfoResult is ViewResult addressInfoView)
            {
                // Lägg till AddressInfo till ViewData för att användas i Index-vyn
                ViewData["AddressInfo"] = addressInfoView.Model;
            }

            return View();
        }
        #endregion

        #region [HttpGet] UserInfo
        //INDEX
        [Authorize]
        public async Task<IActionResult> UserInfo()
        {
            ViewBag.ShowDiv = true;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Account Details";

            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("SigIn", "Account");
            }

            var userEntity = await _userManager.GetUserAsync(User);

            var viewModel = new AccountDetailsViewModel
            {
                User = userEntity
            };

            return View(viewModel);
        }
        #endregion

        #region [HttpGet] BasicInfo
        //INDEX
        [Authorize]
        public async Task<IActionResult> BasicInfo()
        {
            ViewBag.ShowDiv = true;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Account Details";

            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("SigIn", "SigIn");
            }

            var userEntity = await _userManager.GetUserAsync(User);

            if(userEntity != null)
            {
                var viewModel = new AccountBasicInfoViewModel
                {

                    BasicInfo = new AccountDetailsModel
                    {
                        FirstName = userEntity.FirstName,
                        LastName = userEntity.LastName,
                        Email = userEntity.Email,
                        Phone = userEntity.PhoneNumber,
                        Biography = userEntity.Biography,
                    }

                };
                return View(viewModel);
            }

            return View();
        }
        #endregion

        #region [HttpGet] UserInfo
        //INDEX
        [Authorize]
        public async Task<IActionResult> AddressInfo()
        {
            ViewBag.ShowDiv = true;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Account Details";

            if (!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("SigIn", "SignIn");
            }

            var userEntity = await _userManager.GetUserAsync(User);

            if (userEntity != null)
            {
                var addressId = userEntity.AddressId;

                if(addressId != null)
                {
                    var entity = await _addressServices.GetOneAddresses(userEntity);

                    var viewModel = new AccountAddressDetailsViewModel
                    {
                        AddressInfo = new AccountDetailsAddressModel
                        {
                            Address = entity.StreetName,
                            Address2 = entity.StreetName2,
                            PostalCode = entity.PostalCode,
                            City = entity.City,
                        }
                    };
                    return View(viewModel);
                }

                if(addressId == null)
                {
                    var viewModel = new AccountAddressDetailsViewModel
                    {
                        AddressInfo = new AccountDetailsAddressModel
                        {
                            Address = string.Empty,
                            Address2 = string.Empty,
                            PostalCode = string.Empty,
                            City = string.Empty,
                        }
                    };

                    return View(viewModel);
                }
            }
            return View();
        }
        #endregion


        #region [HttpPost] SaveBasicInfo
        [Authorize]
        [HttpPost] 
        public async Task<IActionResult> SaveBasicInfo(AccountBasicInfoViewModel model)
        {
            ViewBag.ShowDiv = true;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Account Details";

            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {

                if (user != null)
                {
                    user.FirstName = model.BasicInfo.FirstName;
                    user.LastName = model.BasicInfo.LastName;
                    user.Email = model.BasicInfo.Email;
                    user.PhoneNumber = model.BasicInfo.Phone;
                    user.Biography = model.BasicInfo.Biography;
                    user.IsExternalAccount = model.IsExternalAccount;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        TempData["SuccessMessageBasicInfo"] = "The data was updated successfully";
                        return RedirectToAction("Index", "Account", model);
                    }
                    if (result.Succeeded != true)
                    {
                        TempData["ErrorMessageBasicInfo"] = "The data was not updated...";
                        return RedirectToAction("Index", "Account", model);
                    }
                }
            }

            if (!ModelState.IsValid)
            {
            }

            return View("Index", model);
        }
        #endregion

        #region [HttpPost] SaveAddressInfo
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveAddressInfo(AccountAddressDetailsViewModel model)
        {
            ViewBag.ShowDiv = true;
            ViewBag.ShowChoices = false;
            ViewData["Title"] = "Account Details";

                var user = await _userManager.GetUserAsync(User);

                if (ModelState.IsValid && user.AddressId == null)
                {
                var newAddress = new AccountDetailsAddressModel
                {
                    Address = model.AddressInfo.Address,
                    Address2 = model.AddressInfo.Address2,
                    PostalCode = model.AddressInfo.PostalCode,
                    City = model.AddressInfo.City,
                };
                    await _addressServices.CreateAddress(newAddress);
                    return RedirectToAction("Index", "Account", model);
                }
                if (ModelState.IsValid && user.AddressId != null)
                {
                    var successResult = await _addressServices.UpdateAddresses(model, user);

                    if (successResult == true)
                    {
                        TempData["SuccessMessageAddressInfo"] = "Data was successfully updated!";
                    }
                    else
                    {
                        TempData["ErrorMessageAddressInfo"] = "The data wasn't updated";
                        
                    }
                    return RedirectToAction("Index", "Account", model);
            }

            TempData["ErrorMessageAddressInfo"] = "Unable to save the changes";

            return RedirectToAction("Index", "Account");
        }
        #endregion       

        //SIGN_OUT
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "SignIn");
        }
    }
}