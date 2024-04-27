using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.GroupModels;
using My_Transfermarkt_Core.Services;
using System.Data;

namespace My_Transfermarkt.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GroupController : BaseController
    {
        private readonly IGroupService groupService;
        private readonly ITournamentService tournamentService;
        private readonly ITeamService teamService;

        public GroupController(
            IGroupService _groupService,
            ITournamentService _tServ,
            ITeamService teamServ)
        {
            groupService = _groupService;
            tournamentService = _tServ;
            teamService = teamServ;
        }

        public async Task<IActionResult> GetAllGroupsForTournament(int id)
        {
            var tournamentGroups = await groupService.GetAllGroupsForTournament(id);
            var find = await tournamentService.FindTournament(id);
            ViewBag.tournament = find.Name;
            return View(tournamentGroups);
        }

        [HttpGet]
        public async Task<IActionResult> AddTeams(int id)
        {
            var tournamentId = await groupService.FindTournament(id);
            var find = await tournamentService.FindTournament(tournamentId);
            var groupName = await groupService.FindGroup(id);
            AddTeamsToGroup model = new AddTeamsToGroup()
            {
                GroupId = id,
                Teams = await teamService.CurrentTeamsInTournament(tournamentId)

            };
            ViewBag.Tournament = find.Name;
            ViewBag.Group = groupName;
            return View(model);
        }
    }
}
