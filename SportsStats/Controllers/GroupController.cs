using Microsoft.AspNetCore.Mvc;
using SportsStats_Core.Contracts;
using SportsStats_Core.Models.MatchModels;
using SportsStats_Infastructure.DataModels;

namespace SportsStats.Controllers
{
    public class GroupController : BaseController
    {
        private readonly IGroupService groupService;
        private readonly ITournamentService tournamentService;

        public GroupController(
            IGroupService _groupService,
            ITournamentService _turnamentService)
        {
            groupService = _groupService;
            tournamentService = _turnamentService;
        }

        public async Task<IActionResult> GetAllGroupsForTournament(int id)
        {
            var tournamentGroups = await groupService.GetAllGroupsForTournament(id);
            ViewBag.Torunament = tournamentGroups[0].TournamentName;
            return View(tournamentGroups);
        }

        public async Task<IActionResult> Details(int groupId)
        {
            var result = await groupService.GetDetails(groupId);
            return View(result);
        }

        public async Task<IActionResult> MatchesInGroup(int groupId)
        {
            if (TempData["groupId"] != null)
            {
                groupId = (int)TempData["groupId"];
                TempData.Remove("groupId");
            }
            var result = await tournamentService.FindMatchesInGroup(groupId);

            if (result.Count == 0)
            {
                return View("Error404");
            }
            ViewBag.Id = groupId;
            var tour = await tournamentService.FindTournament(result[0].TournamentId);
            if (tour == null)
            {
                return View("Error404", new { area = "" });
            }
            Tournament res = (Tournament)tour;
            ViewBag.Tournament = res.Name;
            return View("AllMatches", result);
        }

        public IActionResult AllMatches(List<ShowMatchModel> result)
        {
            return View(result);
        }
    }
}
