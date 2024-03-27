using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.TeamModels;
using My_Transfermarkt_Core.Pagening;

namespace My_Transfermarkt.Controllers
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

        [Authorize(Roles = "User")]
        public async Task<IActionResult> AllTeams(int pg = 1)
        {
            

            var listedTeams = await teamService.GetAllTeamsAvailable();
            const int pageSize = 9;
            if (pg < 1) pg = 1;
            int resCounts = listedTeams.Count();
            var pager = new Pager(resCounts, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = listedTeams.Skip(recSkip).Take(pager.PageSize).ToList();
            pager.TotalPages = resCounts / pageSize;
            this.ViewBag.Pager = pager;
            return View(data);
        }

        [HttpGet]
        public  IActionResult SearchTeamsForCountry()
        {
            return View();
        }

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
            model.Country = (char.ToUpper(model.Country[0]) + model.Country.Substring(1));
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

        public IActionResult Result(List<ShowTeamModelView> listedTeams)
        {
            return View(listedTeams);
        }


    }
}
