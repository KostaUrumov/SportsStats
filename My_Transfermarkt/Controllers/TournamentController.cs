﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.TournamentModels;

namespace My_Transfermarkt.Controllers
{
    [Authorize]
    public class TournamentController : Controller
    {
        private readonly ITournamentService tournamentService;
        private readonly ITeamService teamService;

        public TournamentController(
            ITournamentService _service,
            ITeamService _team)
        {
            tournamentService = _service;
            teamService = _team;
        }
        
        public async Task<IActionResult> AllTournaments()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("NotAuthorize", "Home", new { area = "Administrator" });
            }
            var result = await tournamentService.GetAllTournaments();
            return View(result);
        }
        public async Task<IActionResult> CurrentTeams(AddTeamsToTournament model)
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("NotAuthorize", "Home", new { area = "Administrator" });
            }
            var tournament = await tournamentService.FindTournament(model.Id);

            if (tournament == null)
            {
                return View("Error404", new { area = "" });
            }

            var result = await tournamentService.GetDetails(model.Id);
            return View(result);

        }
    }
}