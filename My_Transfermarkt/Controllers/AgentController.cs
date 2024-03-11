﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.FootballModels;
using My_Transfermarkt_Infastructure.Enums;
using System.Globalization;
using System.Security.Claims;

namespace My_Transfermarkt.Controllers
{
    [Authorize(Roles = "Agent")]
    public class AgentController : Controller
    {
        private readonly ICountryService countryService;
        private readonly ITeamService teamService;
        private readonly IFootballerService footballerService;
        public AgentController(
            ICountryService _country,
            ITeamService _teamService,
            IFootballerService _football)
        {
            countryService = _country;
            teamService = _teamService;
            footballerService = _football;
        }
        [HttpGet]
        public async Task<IActionResult> AddFootballer()
        {
            AddNewFootallerModel footballer = new AddNewFootallerModel();
            footballer.StartDateContract = DateTime.Now;
            footballer.BirthDay = DateTime.Parse("2000-01-01 12:00", CultureInfo.InvariantCulture);
            footballer.Countries = await countryService.GetAllCuntries();
            footballer.Positions.Add(Position.Goalkeeper);
            footballer.Positions.Add(Position.Defender);
            footballer.Positions.Add(Position.Midfielder);
            footballer.Positions.Add(Position.Forward);
            footballer.Feet.Add(Foot.Left);
            footballer.Feet.Add(Foot.Right);

            footballer.Teams = await teamService.GetAllTeams();
            return View(footballer);
        }

        [HttpPost]
        public async Task<IActionResult> AddFootballer(AddNewFootallerModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.AgentId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await footballerService.CreateFootballerAsync(model);
            return View();
        }
    }
}