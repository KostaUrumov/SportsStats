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
            int[] teams = new int[model.SelectedTeams.Count()];
           for (int i = 0;i < teams.Length; i++)
            {
                teams[i] = model.SelectedTeams[i];
            }

           return View(teams);
        }

    }
}
