using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStats_Core.Contracts;
using SportsStats_Core.Models.FootballerModels;
using SportsStats_Infastructure.Enums;
using System.Security.Claims;

namespace SportsStats.Controllers
{
    public class FootballerController : BaseController
    {
        private readonly IFootballerService footballerService;
        private readonly ICountryService countryService;
        private readonly ITeamService teamService;

        public FootballerController(
            IFootballerService _footballervice,
            ICountryService _countryService,
            ITeamService _teamService)
        {
            footballerService = _footballervice;
            countryService = _countryService;
            teamService = _teamService;
        }

        [Authorize(Roles = "Agent")]

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var findFootballer = await footballerService.FindFootballer(id);

            if (findFootballer == null)
            {
                ViewBag.comment = "Player do not exist";
                return View("Error404");
            }

            if (findFootballer.isRetired == true)
            {
                ViewBag.comment = "Player is already retired";
                return View("Error404");
            }

            if (findFootballer.AgentId != userId)
            {
                ViewBag.comment = "Not Authorized";
                return View("NotAuthorize");
            }

            findFootballer.Countries = await countryService.GetAllCuntries();
            findFootballer.Teams = await teamService.GetAllTeams();
            findFootballer.Positions.Add(Position.Goalkeeper);
            findFootballer.Positions.Add(Position.Defender);
            findFootballer.Positions.Add(Position.Midfielder);
            findFootballer.Positions.Add(Position.Forward);
            findFootballer.Feet.Add(Foot.Left);
            findFootballer.Feet.Add(Foot.Right);
            ViewBag.FootballerName = findFootballer.FirstName + " " + findFootballer.LastName;
            return View(findFootballer);
        }





        [Authorize(Roles = "Agent")]
        [HttpGet]
        public async Task<IActionResult> UploadPicture(int Id)
        {
            var findFootballer = await footballerService.FindFootballer(Id);
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (findFootballer.AgentId != userId)
            {
                ViewBag.comment = "Not Authorized";
                return View("NotAuthorize");
            }
            if (findFootballer == null)
            {
                return View("Error404");
            }
            return View(Id);
        }

        [Authorize(Roles = "Agent, User")]

        public async Task<IActionResult> GetAllPlayersForClub(int Id)
        {
            var listedPlayers = await footballerService.GetAllPLayersForClub(Id);
            var team = await teamService.FindTeam(Id);
            if (team == null)
            {
                return View("Error404");
            }
            ViewBag.TeamName = team.TeamName;
            return View(listedPlayers);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int Id)
        {

            var findFootballer = await footballerService.FindFootballer(Id);
            if (findFootballer == null)
            {

                return View("Error404");
            }

            var result = await footballerService.Details(Id);
            return View(result);
        }



        [Authorize(Roles = "User")]
        public async Task<IActionResult> RetiredPlayers()
        {

            var getRetiredPlayers = await footballerService.GetRetiredPlayers();
            return View(getRetiredPlayers);
        }

        [Authorize]
        [Authorize(Roles = "Agent, User")]
        public async Task<IActionResult> GetAllFootballers()
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var result = await footballerService.AllFootballers(userId);

            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = "Agent, User")]
        public IActionResult SearchFootballersForCountry()
        {

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Agent, User")]
        public async Task<IActionResult> SearchFootballersForCountry(SearchByCountryModel model)
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


            var listetPlayers = await footballerService.GetAllPLayersForCountry(model.Country);
            model.Country = char.ToUpper(model.Country[0]) + model.Country.Substring(1);
            if (listetPlayers.Count == 0)
            {
                listetPlayers.Add(new ShowFootballerDetailsViewModel()
                {
                    Country = isCountryIn
                });

                ViewBag.comment = "No players for";
                return View("Result", listetPlayers);
            }
            return View("Result", listetPlayers);
        }

        [Authorize(Roles = "Agent, User")]
        public IActionResult Result(List<ShowFootballerDetailsViewModel> listetPlayers)
        {
            return View(listetPlayers);
        }
    }
}
