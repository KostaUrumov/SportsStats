using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStats_Core.Contracts;
using SportsStats_Core.Models.MatchModels;
using SportsStats_Infastructure.DataModels;

namespace SportsStats.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MatchController : BaseController
    {
        private readonly ITournamentService tournamentService;
        private readonly ITeamService teamService;
        private readonly IRefereeService refService;
        private readonly IMatchService matchService;
        private readonly IGroupService groupService;

        public MatchController(
            ITournamentService _tournamentService,
            ITeamService _team,
            IRefereeService _refServ,
            IMatchService _matchService,
            IGroupService _groupService)
        {
            tournamentService = _tournamentService;
            teamService = _team;
            refService = _refServ;
            matchService = _matchService;
            groupService = _groupService;
        }

        [HttpGet]
        public async Task<IActionResult> AddNewMatch(int Id, int GroupId)
        {
            AddNewMatchModel model = new AddNewMatchModel();

            if (GroupId != 0)
            {
                model.GroupId = GroupId;
                Id = await groupService.FindTournament(GroupId);
                model.Rounds = await groupService.AddRounds(GroupId);
                model.Teams = await teamService.GetTeamsByGroupId(GroupId);
                TempData["groupId"] = GroupId;
            }
            var tournament = await tournamentService.FindTournament(Id);
            if (tournament == null)
            {
                return View("Error404", new { area = "" });
            }
            var tour = (Tournament)tournament;

            if (tournament == null)
            {
                return View("Error404", new { area = "" });
            }

            model.TournamentId = tour.Id;
            if (model.Teams.Count() == 0 && GroupId == 0)
            {
                model.Teams = await teamService.GetAllTeamsForTournament(Id);
            }

            model.Referees = await refService.AllReferees();
            if (model.Rounds.Count == 0)
            {
                model.Rounds = await tournamentService.AddRounds(tour.Id);
            }


            ViewBag.Tournament = tour.Name;
            ViewBag.Id = tour.Id;
            ViewBag.Group = model.GroupId;
            model.Date = DateTime.Today;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewMatch(int Id, AddNewMatchModel model)
        {
            model.TournamentId = Id;
            if (TempData["groupId"] != null)
            {
                model.GroupId = (int)TempData["groupId"];
                TempData.Remove("groupId");
                model.TournamentId = (int)await tournamentService.FindTournamentIdByGroup((int)model.GroupId);
            }


            if (matchService.AreTeamsDifferent(model) == false)
            {
                ViewBag.comment = "Home and away teams are the same. Select different teams";
                model.Teams = await teamService.GetAllTeamsForTournament(Id);
                model.Referees = await refService.AllReferees();
                if (model.GroupId != null)
                {
                    model.Rounds = await groupService.AddRounds((int)model.GroupId);
                    return View(model);
                }
                model.Rounds = await tournamentService.AddRounds(Id);

                return View(model);
            }

            if (await matchService.CheckIfMatchExists(model, Id) == true)
            {
                ViewBag.comment = "Same match is already in the tournament";
                model.Teams = await teamService.GetAllTeamsForTournament(Id);
                model.Referees = await refService.AllReferees();
                model.Rounds = await tournamentService.AddRounds(Id);
                if (model.GroupId != null)
                {
                    model.Rounds = await groupService.AddRounds((int)model.GroupId);
                    return View(model);
                }
                return View(model);
            }
            await matchService.AddNewMatch(model);
            if (model.GroupId != null)
            {
                ViewBag.Id = model.GroupId;
                var tour = await tournamentService.FindTournament(model.TournamentId);
                if (tour == null)
                {
                    return View("Error404", new { area = "" });
                }
                var competition = (Tournament)tour;
                ViewBag.Tournament = competition.Name;
                TempData["groupId"] = model.GroupId;
                return RedirectToAction("MatchesInGroup", "Group");

            }

            TempData["Id"] = Id;
            return RedirectToAction("Matches", "Tournament");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            AddNewMatchModel model = new AddNewMatchModel();
            var match = await matchService.FindMatch(Id);
            if (match.GroupId != null)
            {
                model.Rounds = await groupService.AddRounds((int)match.GroupId);
                model.Teams = await teamService.GetTeamsByGroupId((int)match.GroupId);
            }
            else
            {
                model.Rounds = await tournamentService.AddRounds(match.TournamentId);
            }
            model.TournamentId = match.TournamentId;
            if (model.Teams.Count() == 0 && model.GroupId == null)
            {
                model.Teams = await teamService.GetAllTeamsForTournament(match.TournamentId);
            }

            model.Referees = await refService.AllReferees();
            model.RefereeId = match.RefereeId;
            model.HomeTeamId = match.HomeTeamId;
            model.AwayTeamId = match.AwayTeamId;
            model.HomeScore = match.HomeScore;
            model.AwayScore = match.AwayScore;
            model.Round = match.Round;
            model.Date = match.MatchDate;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddNewMatchModel model)
        {
            var findMatch = await matchService.FindMatch(model.Id);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await matchService.CheckIfMatchExists(model, findMatch.TournamentId) == true)
            {
                ViewBag.comment = "Same match is already in the tournament";
                if (findMatch.GroupId != null)
                {
                    model.Rounds = await groupService.AddRounds((int)findMatch.GroupId);
                }
                else
                {
                    model.Rounds = await tournamentService.AddRounds(findMatch.TournamentId);
                }
                model.TournamentId = findMatch.TournamentId;
                model.Teams = await teamService.GetAllTeamsForTournament(findMatch.TournamentId);
                model.Referees = await refService.AllReferees();
                model.RefereeId = findMatch.RefereeId;
                model.HomeTeamId = findMatch.HomeTeamId;
                model.AwayTeamId = findMatch.AwayTeamId;
                model.HomeScore = findMatch.HomeScore;
                model.AwayScore = findMatch.AwayScore;
                model.Round = findMatch.Round;
                model.Date = findMatch.MatchDate;
                return View(model);
            }


            if (matchService.AreTeamsDifferent(model) == false)
            {
                ViewBag.comment = "Home and away teams are the same. Select different teams";
                if (findMatch.GroupId != null)
                {
                    model.Rounds = await groupService.AddRounds((int)findMatch.GroupId);
                }
                else
                {
                    model.Rounds = await tournamentService.AddRounds(findMatch.TournamentId);
                }
                model.TournamentId = findMatch.TournamentId;
                model.Teams = await teamService.GetAllTeamsForTournament(findMatch.TournamentId);
                model.Referees = await refService.AllReferees();
                model.RefereeId = findMatch.RefereeId;
                model.HomeTeamId = findMatch.HomeTeamId;
                model.AwayTeamId = findMatch.AwayTeamId;
                model.HomeScore = findMatch.HomeScore;
                model.AwayScore = findMatch.AwayScore;
                model.Round = findMatch.Round;
                model.Date = findMatch.MatchDate;
                return View(model);
            }
            await matchService.SaveChanges(model);

            if (findMatch.GroupId != null)
            {
                TempData["groupId"] = findMatch.GroupId;
                return RedirectToAction("MatchesInGroup", "Group");
            }

            TempData["Id"] = findMatch.TournamentId;
            return RedirectToAction("Matches", "Tournament");
        }

    }
}
