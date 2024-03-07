using Microsoft.AspNetCore.Mvc;
using Infrastructure.ViewModels;

namespace Infrastructure.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.ShowDiv = true;
        ViewBag.ShowChoices = true;
        ViewData["Title"] = "Home";
        return View();
    }

    [HttpPost]
    public IActionResult Index(SubscribeViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ShowDiv = true;
            ViewBag.ShowChoices = true;
            ViewData["Title"] = "Home";
            return View(viewModel);
        }
        //_homeService.Subscribe(viewModel.Subscribe)
        return View();
    }

}
