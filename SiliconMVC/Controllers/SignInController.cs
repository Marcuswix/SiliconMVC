using Infrastructure.Entities;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace SiliconMVC.Controllers
{
    public class SignInController : Controller
    {

        private readonly SignInManager<UserEntity> _signInManager;

        public SignInController(SignInManager<UserEntity> signInManager)
        {
            _signInManager = signInManager;
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



        #region External Account | Facebbok
        [HttpGet]
        public IActionResult Facebook()
        {
            var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Facebbok", Url.Action("FacebookCallback"));

            return new ChallengeResult("Facebook", authProps);
        }
        #endregion
    }
}