﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SportsStats.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : BaseController
    {
        public IActionResult NotAuthorize()
        {
            return View("NotAuthorize");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
