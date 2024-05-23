using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.MatchModels;
using My_Transfermarkt_Core.Models.TournamentModels;
using Tournament = My_Transfermarkt_Infastructure.DataModels.Tournament;

namespace My_Transfermarkt.Controllers
{
    [Authorize]
    public class TournamentController : Controller
    {
        private readonly ITournamentService tournamentService;


        public TournamentController(
            ITournamentService _service,
            ITeamService _team)
        {
            tournamentService = _service;
        }

        [Authorize(Roles = "Agent, User")]
        public async Task<IActionResult> AllTournaments()
        {
            var result = await tournamentService.GetAllTournaments();
            return View(result);
        }

        [Authorize(Roles = "Agent, User")]
        public async Task<IActionResult> CurrentTeams(AddTeamsToTournament model)
        {

            var tournament = await tournamentService.FindTournament(model.Id);

            if (tournament == null)
            {
                return View("Error404", new { area = "" });
            }

            var result = await tournamentService.GetDetails(model.Id);
            return View(result);

        }

        public async Task<IActionResult> Matches(int Id)
        {
            if (TempData["Id"] != null)
            {
                Id = (int)TempData["Id"];
                TempData.Remove("Id");
            }
            var tournament = await tournamentService.FindTournament(Id);

            if (tournament == null)
            {
                return View("Error404", new { area = "" });
            }
            var result = await tournamentService.FindMatchesInTournament(Id);

            if (tournament == null)
            {
                return View("Error404", new { area = "" });
            }
            var competition = (Tournament)tournament;

            ViewBag.Tournament = competition.Name;
            ViewBag.Id = competition.Id;
            return View("ResultMatches", result);
        }
        public IActionResult ResultMatches(List<ShowMatchModel> result)
        {
            return View(result);
        }

        public async Task<IActionResult> Standings(int Id)
        {
            var tournament = await tournamentService.FindTournament(Id);
            if (tournament != null)
            {
                var competition = (Tournament)tournament;
                ViewBag.Tournament = competition.Name;
                ViewBag.TournamentId = competition.Id;
            }

            var result = await tournamentService.StandongsInTournament(Id);

            return View(result);
        }

        public async Task<IActionResult> Team(int teamId, int tourId)
        {
            var result = await tournamentService.FindMatchesPerTeamInTournament(teamId, tourId);
            return View("ResultMatches", result);
        }

    }
}
