using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt.Models;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models;
using My_Transfermarkt_Core.Models.GeneralModels;
using System.Diagnostics;

namespace My_Transfermarkt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITeamService teamerService;
        private readonly IFootballerService footballerService;
        private readonly IStadiumService stadiumService;
        private readonly ITeamService teamService;

        public HomeController(
            ILogger<HomeController> logger,
            ITeamService team,
            IFootballerService _footballerService,
            IStadiumService _stadiumService,
            ITeamService _teamService)
        {
            _logger = logger;
            teamerService = team;
            footballerService = _footballerService;
            stadiumService = _stadiumService;
            teamService = _teamService;
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchViewModel model)
        {
            List<ResultsViewModel> results = new List<ResultsViewModel>();
            var footballers = await footballerService.FindFootballers(model.Search);
            if (footballers.Count > 0)
            {
                results.AddRange(footballers);
            }
            var teams = await teamerService.FindTeams(model.Search);
            if (teams.Count > 0)
            {
                results.AddRange(teams);
            }

            return View("DisplaySearched", results);
        }

        public IActionResult DisplaySearched(List<ResultsViewModel> results)
        {
            return View(results);
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
