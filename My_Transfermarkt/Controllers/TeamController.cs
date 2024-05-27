using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStats_Core.Contracts;
using SportsStats_Core.Models.TeamModels;
using SportsStats_Core.Pagening;

namespace SportsStats.Controllers
{
    public class TeamController : BaseController
    {
        private readonly ITeamService teamService;
        private readonly ICountryService countryService;

        public TeamController(
            ITeamService _team,
            ICountryService _countryService)
        {
            teamService = _team;
            countryService = _countryService;
        }

        [Authorize(Roles = "Agent, User")]
        public async Task<IActionResult> AllTeams(int pg = 1)
        {

            int totalTeams = teamService.TotalTeamNumber();
            const int pageSize = 9;
            var result = await teamService.GetTeams(pageSize, pg);
            int resCounts = result.Count();
            var pager = new Pager();
            pager.TotalPages = (int)Math.Ceiling(totalTeams / (decimal)pageSize);
            pager.Startpage = 1;
            ViewBag.Pager = pager;
            return View(result.ToList());
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult SearchTeamsForCountry()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> SearchTeamsForCountry(SearchCountry model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var isCountryIn = await countryService.FindCountryByname(model.Country);
            if (isCountryIn == null)
            {
                ViewBag.comment = "No Such Country";
                return View("Error404");
            }

            var listedTeams = await teamService.FindTeamByCountry(model.Country);
            model.Country = char.ToUpper(model.Country[0]) + model.Country.Substring(1);
            if (listedTeams.Count == 0)
            {
                ShowTeamModelView newModel = new ShowTeamModelView()
                {
                    Country = isCountryIn
                };
                ViewBag.comment = "No teams listed from ";
                listedTeams.Add(newModel);
                return View(nameof(Result), listedTeams);
            }

            return View(nameof(Result), listedTeams);
        }

        [Authorize(Roles = "User")]
        public IActionResult Result(List<ShowTeamModelView> listedTeams)
        {
            return View(listedTeams);
        }

    }
}
