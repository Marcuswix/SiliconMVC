using Microsoft.AspNetCore.Mvc;

namespace SiliconMVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Home";
        return View();

    }
}
