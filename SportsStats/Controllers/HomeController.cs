﻿using Microsoft.AspNetCore.Mvc;
using SportsStats_Core.Contracts;
using SportsStats_Core.Models.GeneralModels;

namespace SportsStats.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITeamService teamerService;
        private readonly IFootballerService footballerService;
        private readonly IStadiumService stadiumService;
        private readonly ITeamService teamService;
        private readonly ICountryService countryService;

        public HomeController(
            ILogger<HomeController> logger,
            ITeamService team,
            IFootballerService _footballerService,
            IStadiumService _stadiumService,
            ITeamService _teamService,
            ICountryService _countryService)
        {
            _logger = logger;
            teamerService = team;
            footballerService = _footballerService;
            stadiumService = _stadiumService;
            teamService = _teamService;
            countryService = _countryService;
        }

        [HttpGet]
        public IActionResult Search()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("NotAuthorize", "Home", new { area = "Administrator" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchViewModel model)
        {
            List<ResultsViewModel> results = new List<ResultsViewModel>();
            var footballers = await footballerService.FindFootballers(model.Search);
            var teams = await teamerService.FindTeams(model.Search);
            var stadiums = await stadiumService.FindStadiums(model.Search);
            var countries = await countryService.FindCountries(model.Search);
            if (footballers.Count > 0)
            {
                results.AddRange(footballers);
            }

            if (teams.Count > 0)
            {
                results.AddRange(teams);
            }
            if (stadiums.Count > 0)
            {
                results.AddRange(stadiums);
            }
            if (countries.Count > 0)
            {
                results.AddRange(countries);
            }


            return View("DisplaySearched", results);
        }

        public IActionResult DisplaySearched(List<ResultsViewModel> results)
        {
            return View(results);
        }


        public IActionResult Index()
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

            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("Error404");
            }

            return View();
        }
    }
}
