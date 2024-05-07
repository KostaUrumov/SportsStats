using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.MatchModels;

namespace My_Transfermarkt.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MatchController : BaseController
    {
        private readonly ITournamentService tournamentService;
        private readonly ITeamService teamService;
        private readonly IRefereeService refService;
        private readonly IMatchService matchService;

        public MatchController(
            ITournamentService _tournamentService,
            ITeamService _team,
            IRefereeService _refServ,
            IMatchService _matchService)
        {
            tournamentService = _tournamentService;
            teamService = _team;
            refService = _refServ;
            matchService = _matchService;
        }

        [HttpGet]
        public async Task<IActionResult> AddNewMatch(int Id)
        {
            var tournament = await tournamentService.FindTournament(Id);
            if (tournament == null)
            {
                return View("Error404", new { area = "" });
            }
            AddNewMatchModel model = new AddNewMatchModel()
            {
                TournamentId = tournament.Id,
                Teams = await teamService.GetAllTeamsForTournament(Id),
                Referees = await refService.AllReferees(),
                Rounds = await tournamentService.AddRounds(tournament.Id)
            };
            ViewBag.Tournament = tournament.Name;
            ViewBag.Id = tournament.Id;
            model.Date = DateTime.Today;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewMatch(int Id,AddNewMatchModel model)
        {
            model.TournamentId = Id;
            var areTeamsDifferent = matchService.AreTeamsDifferent(model);
            if (areTeamsDifferent == false)
            {
                ViewBag.comment = "Home and away teams are the same. Select different teams";
                return View(model);
            }
            await matchService.AddNewMatch(model);
            var result = await tournamentService.FindMatchesInTournament(Id);
            return View("MatchesInTournament", result);
        }

        public IActionResult MatchesInTournament (List<ShowMatchModel> result)
        {
            ViewBag.Id = result[0].TournamentId;
            return View(result);
        }

    }
}
