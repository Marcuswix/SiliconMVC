using Infrastructure.Entities;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace SiliconMVC.Controllers
{
    public class SignInController : Controller
    {

        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;

        public SignInController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void SetDefaultValues()
        {
            ViewBag.ShowDiv = false;
            ViewBag.ShowChoices = false;
        }

        #region [HttpGet] SignIn
        [HttpGet]
        [Route("/signin")]
        public IActionResult Index(string returnUrl)
        {
            SetDefaultValues();

            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Account");
            }

            ViewData["ReturnUrl"] = returnUrl ?? Url.Content("~/");
            return View();
        }
        #endregion

        #region [HttpPost] SignIn
        [Route("/signin")]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model, string returnUrl)
        {
            SetDefaultValues();

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Form.Email, model.Form.Password, model.Form.RememberMe, false);
                Debug.WriteLine("PasswordSignInAsync result: " + result.Succeeded);

                if (result.Succeeded)
                {

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("Index", "Account");

                }
            }
            ModelState.AddModelError("IncorrextValues", "Incorrect email or password");
            ViewData["ErrorMessage"] = "Incorrect email or password";

            return View("Index", model);
        }
        #endregion



        #region External Account | Facebook
        [HttpGet]
        public IActionResult Facebook()
        {
            var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("FacebookCallback"));

            return new ChallengeResult("Facebook", authProps);
        }
        #endregion

        #region [HttpGet] FacebookCallback
        [HttpGet]
        public async Task<IActionResult> FacebookCallback()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info != null)
            {
                var userEntity = new UserEntity
                {
                    FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                    LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                    IsExternalAccount = true,
                };

                var user = await _userManager.FindByEmailAsync(userEntity.Email);

                if (user == null)
                {
                    var result = await _userManager.CreateAsync(userEntity);

                    if (result.Succeeded)
                    {
                        user = await _userManager.FindByEmailAsync(userEntity.Email);
                    }
                }

                if (user != null)
                {
                    if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
                    {
                        user.FirstName = userEntity.FirstName;
                        user.LastName = userEntity.LastName;
                        user.Email = userEntity.Email;
                        user.IsExternalAccount = true;

                        await _userManager.UpdateAsync(user);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (HttpContext.User != null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("InvalidFacebookAuthenication", "danger|Failed facebook authentication");
            ViewData["StatusMessage"] = "danger|Failed facebook authentication";
            return RedirectToAction("Index", "SignIn");
        }
        #endregion
    }
}