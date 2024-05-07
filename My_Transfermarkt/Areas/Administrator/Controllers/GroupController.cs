using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.GroupModels;
using My_Transfermarkt_Core.Models.MatchModels;
using My_Transfermarkt_Core.Services;

namespace My_Transfermarkt.Areas.Administrator.Controllers
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
            AddTeamsToGroupModel model = new AddTeamsToGroupModel()
            {
                Teams = await teamService.GetAllTeamsForTournament(tournamentId)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeamsToGroup(AddTeamsToGroupModel model)
        {
            var tournamentId = await groupService.FindTournament(model.Id);
            if (!ModelState.IsValid)
            {
               
                model.Teams = await teamService.GetAllTeamsForTournament(tournamentId);
                return View(model);
            }
            
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
            await groupService.AddTeamsToGroup(model.Id,teams);
            var tournamentGroups = await groupService.GetAllGroupsForTournament(tournamentId);
            ViewBag.Torunament = tournamentGroups[0].TournamentName;
            return View(nameof(Result), tournamentGroups);
        }

        public  IActionResult Result(List<ShowGroupViewModel> model)
        {
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddNewMatch(int Id)
        {
            AddNewMatchModel model = new AddNewMatchModel()
            {
                Teams = await teamService.GetTeamsByGroupId(Id),
                Referees = await refereeService.AllReferees(),
                Rounds = await groupService.AddRounds(Id)
            };

            return View(model); 
        }

        [HttpPost]
        public async Task<IActionResult> AddNewMatch(int Id, AddNewMatchModel model)
        {
            model.GroupId = Id;
            model.TournamentId = await tournamentService.FindTournamentIdByGroup(Id);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (matchService.AreTeamsDifferent(model) == false)
            {
                ViewBag.comment = "Home and away teams are the same. Select different teams";
                return View(model);
            }
            await matchService.AddNewMatch(model);
            var result = await tournamentService.FindMatchesInGroup(Id);
            ViewBag.Id = result[0].GroupId;
            var tour = await tournamentService.FindTournament(result[0].TournamentId);
            ViewBag.Tournament = tour.Name;
            return View("AllMatches", result);
            
        }

        public async Task<IActionResult> AllMatches(List<ShowMatchModel> result)
        {
            return View(result);
        }

    }
}
