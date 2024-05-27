using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStats_Core.Contracts;
using SportsStats_Core.Models.GroupModels;
using SportsStats_Core.Models.MatchModels;
using SportsStats_Infastructure.DataModels;

namespace SportsStats.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GroupController : BaseController
    {
        private readonly IGroupService groupService;
        private readonly ITeamService teamService;
        private readonly ITournamentService tournamentService;
        private readonly IRefereeService refereeService;
        private readonly IMatchService matchService;

        public GroupController(
            IGroupService _groupService,
            ITeamService _teamService,
            ITournamentService _tournamentService,
            IRefereeService _refereeService,
            IMatchService _matchService)
        {
            groupService = _groupService;
            teamService = _teamService;
            tournamentService = _tournamentService;
            refereeService = _refereeService;
            matchService = _matchService;
        }

        public async Task<IActionResult> GetAllGroupsForTournament(int id)
        {

            var tournamentGroups = await groupService.GetAllGroupsForTournament(id);
            ViewBag.Torunament = tournamentGroups[0].TournamentName;
            return View(nameof(Result), tournamentGroups);
        }

        [HttpGet]
        public async Task<IActionResult> AddTeamsToGroup(int id)
        {
            var tournamentId = await groupService.FindTournament(id);
            ActionForTeamsInGroup model = new ActionForTeamsInGroup()
            {
                Teams = await teamService.GetAllTeamsForTournament(tournamentId)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeamsToGroup(ActionForTeamsInGroup model)
        {
            var tournamentId = await groupService.FindTournament(model.Id);
            if (!ModelState.IsValid)
            {

                model.Teams = await teamService.GetAllTeamsForTournament(tournamentId);
                return View(model);
            }

            if (model.SelectedTeams != null)
            {
                int[] teams = new int[model.SelectedTeams.Length];
                for (int i = 0; i < teams.Length; i++)
                {
                    var team = await teamService.FindTeam(model.SelectedTeams[i]);
                    var isTeamIn = await groupService.IsTeamInThisGroup(model.Id, team.Id);
                    if (isTeamIn == true)
                    {
                        continue;
                    }
                    if (team == null)
                    {
                        return View("Error404", new { area = "" });
                    }
                    teams[i] = model.SelectedTeams[i];
                }
                await groupService.AddTeamsToGroup(model.Id, teams);
            }

            var tournamentGroups = await groupService.GetAllGroupsForTournament(tournamentId);
            ViewBag.Torunament = tournamentGroups[0].TournamentName;
            return View(nameof(Result), tournamentGroups);
        }

        [HttpGet]
        public async Task<IActionResult> ReamoveTeamsFromGroup(int id)
        {
            ActionForTeamsInGroup model = new ActionForTeamsInGroup()
            {
                Teams = await teamService.GetTeamsByGroupId(id)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ReamoveTeamsFromGroup(ActionForTeamsInGroup model)
        {
            var tournamentId = await groupService.FindTournament(model.Id);
            if (!ModelState.IsValid)
            {

                model.Teams = await teamService.GetTeamsByGroupId(model.Id);
                return View(model);
            }
            if (model.SelectedTeams != null)
            {
                int[] teams = new int[model.SelectedTeams.Length];
                for (int i = 0; i < teams.Length; i++)
                {
                    var team = await teamService.FindTeam(model.SelectedTeams[i]);
                    if (await matchService.IsTeamAssignedToMatchInGroup(team.Id, model.Id) == true)
                    {
                        continue;
                    }
                    await teamService.RemoveFromGroup(team.Id, model.Id);

                }
            }
            var tournamentGroups = await groupService.GetAllGroupsForTournament(tournamentId);
            ViewBag.Torunament = tournamentGroups[0].TournamentName;
            return View(nameof(Result), tournamentGroups);


        }

        public IActionResult Result(List<ShowGroupViewModel> model)
        {
            return View(model);
        }


        public IActionResult AllMatches(List<ShowMatchModel> result)
        {
            return View(result);
        }

        public async Task<IActionResult> MatchesInGroup(int groupId)
        {
            if (TempData["groupId"] is not null)
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

    }
}
