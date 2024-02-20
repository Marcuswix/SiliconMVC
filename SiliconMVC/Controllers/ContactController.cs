﻿using Microsoft.AspNetCore.Mvc;

namespace SiliconMVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ShowDiv = true;
            ViewData["Title"] = "Contact";
            return View();
        }
    }
}
