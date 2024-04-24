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

        public MatchController(
            ITournamentService _tournamentService,
            ITeamService _team,
            IRefereeService _refServ)
        {
            tournamentService = _tournamentService;
            teamService = _team;
            refService = _refServ;
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
                Teams = await teamService.GetAllTeams(),
                Referees = await refService.AllReferees()
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
            return View();
        }


    }
}
