using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;

namespace My_Transfermarkt.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService teamService;

        public TeamController(ITeamService _team)
        {
            teamService = _team;
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> AllTeams()
        {
            var listedTeams = await teamService.GetAllTeamsAvailable();
            return View(listedTeams);
        }
    }
}
