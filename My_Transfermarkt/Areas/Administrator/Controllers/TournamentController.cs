using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.TournamentModels;

namespace My_Transfermarkt.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TournamentController : BaseController
    {
        private readonly ITournamentService tournamentService;
        private readonly ITeamService teamService;

        public TournamentController(
            ITournamentService _service,
            ITeamService _team)
        {
            tournamentService = _service;
            teamService = _team;
        }

        public async Task<IActionResult> AllTournaments()
        {
            var result = await tournamentService.GetAllTournaments();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> AddTeams(int Id)
        {
            AddTeamsToTournament model = new AddTeamsToTournament()
            {
                Id = Id,
                Teams = await teamService.GetAllTeamsAvailable()
            };
            ViewBag.Tournament = await tournamentService.GetName(Id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeams(int Id, AddTeamsToTournament model)
        {
            var tournament = await tournamentService.FindTournament(Id);
            model.Id = tournament.Id;
            
            if (tournament == null)
            {
                return View("Error404", new { area = "" });
            }
            int[] teams = new int[model.SelectedTeams.Count()];
           for (int i = 0;i < teams.Length; i++)
            {
                var team = await teamService.FindTeam(model.SelectedTeams[i]);
                if (team == null)
                {
                    return View("Error404", new { area = "" });
                }
                teams[i] = model.SelectedTeams[i];
                await tournamentService.AddTeamToTournament(Id, model.SelectedTeams[i]);
            }
            
            return RedirectToAction("TournamentDeails", model);
        }

        public async Task<IActionResult> TournamentDeails(AddTeamsToTournament model)
        {
            var tournament = await tournamentService.FindTournament(model.Id);

            if (tournament == null)
            {
                return View("Error404", new { area = "" });
            }

            var result = await tournamentService.GetDetails(model.Id);
            return View(result);

        }

    }
}
