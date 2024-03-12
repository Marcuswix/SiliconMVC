using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers
{
    public class PageNotFoundController : Controller
    {
        [Route("/error")]
        public IActionResult Index()
        {
            ViewBag.ShowDiv = false;
            ViewBag.ShowChoices = false;
            return View();
        }


        public IActionResult ReturnToHome() 
        {
            return RedirectToAction("Index", "Home");
        }
    }

    //public class DefaultController : Controller
    //{
    //    [Route("/")]
    //    public IActionResult Index() => View();


    //    [Route("/error")]
    //    public IActionResult Error404(int statusCode) => View();
    //}
}
