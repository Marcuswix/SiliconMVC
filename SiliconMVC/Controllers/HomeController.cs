using Microsoft.AspNetCore.Mvc;

namespace SiliconMVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.ShowDiv = true;
        ViewBag.ShowChoices = true;
        ViewData["Title"] = "Home";
        return View();

    }
}
