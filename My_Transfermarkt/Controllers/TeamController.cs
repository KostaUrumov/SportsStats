using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Pagening;

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
    }
}
