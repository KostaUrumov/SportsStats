using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt.Models;
using My_Transfermarkt_Core.Contracts;
using System.Diagnostics;

namespace My_Transfermarkt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITeamService teamerService;

        public HomeController(
            ILogger<HomeController> logger,
            ITeamService team)
        {
            _logger = logger;
            teamerService = team;
        }

        public async Task< IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home", new { area = "Administrator" });
            }
            if (User.IsInRole("Agent"))
            {
                return RedirectToAction("MyFootballers", "Footballer");
            }
            else if (User.IsInRole("User"))
            {
                return RedirectToAction("AllTeams", "Team");
            }
            var result = await teamerService.GetRandomListForHomePage();
            return View(result);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 404)
            {
                return View();
            }

            return View();
        }
    }
}
