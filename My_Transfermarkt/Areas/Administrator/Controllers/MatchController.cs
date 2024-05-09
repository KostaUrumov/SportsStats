﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.MatchModels;
using My_Transfermarkt_Core.Services;

namespace My_Transfermarkt.Areas.Administrator.Controllers
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
        public async Task<IActionResult> AddNewMatch(int Id)
        {
            var tournament = await tournamentService.FindTournament(Id);
            if (await tournamentService.IsItGroupStageTournament(Id) == true)
            {
                TempData["id"] = Id;
                RedirectToAction("GetAllGroupsForTournament", "Group");
            }
            if (tournament == null)
            {
                return View("Error404", new { area = "" });
            }
            AddNewMatchModel model = new AddNewMatchModel()
            {
                TournamentId = tournament.Id,
                Teams = await teamService.GetAllTeamsForTournament(Id),
                Referees = await refService.AllReferees(),
                Rounds = await tournamentService.AddRounds(tournament.Id)
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
            if (matchService.AreTeamsDifferent(model) == false)
            {
                ViewBag.comment = "Home and away teams are the same. Select different teams";
                model.Teams = await teamService.GetAllTeamsForTournament(Id);
                model.Referees = await refService.AllReferees();
                model.Rounds = await tournamentService.AddRounds(Id);

                return View(model);
            }

            if (await matchService.CheckIfMatchExists(model, Id) == true)
            {
                ViewBag.comment = "Same match is already in the tournament";
                model.Teams = await teamService.GetAllTeamsForTournament(Id);
                model.Referees = await refService.AllReferees();
                model.Rounds = await tournamentService.AddRounds(Id);
                return View(model);
            }

            await matchService.AddNewMatch(model);
            var result = await tournamentService.FindMatchesInTournament(Id);
            return View("MatchesInTournament", result);
        }

        public IActionResult MatchesInTournament (List<ShowMatchModel> result)
        {
            ViewBag.Id = result[0].TournamentId;
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            AddNewMatchModel model = new AddNewMatchModel();
            var match = await matchService.FindMatch(Id);
            if (match.GroupId != null)
            {
                model.Rounds = await groupService.AddRounds((int)match.GroupId);
            }
            else
            {
                model.Rounds = await tournamentService.AddRounds(match.TournamentId);
            }
            model.TournamentId = match.TournamentId;
            model.Teams = await teamService.GetAllTeamsForTournament(match.TournamentId);
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

            if(await matchService.CheckIfMatchExists(model, findMatch.TournamentId) == true)
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
            return RedirectToAction("Matches","Tournament");
        }

    }
}
