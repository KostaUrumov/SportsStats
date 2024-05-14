using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Services;

namespace My_Transfermarkt.Controllers
{
    public class GroupController : BaseController
    {
        private readonly IGroupService groupService;

        public GroupController(IGroupService _groupService)
        {
            groupService = _groupService;
        }

        public async Task<IActionResult> GetAllGroupsForTournament(int id)
        {
            var tournamentGroups = await groupService.GetAllGroupsForTournament(id);
            ViewBag.Torunament = tournamentGroups[0].TournamentName;
            return View(tournamentGroups);
        }
    }
}
