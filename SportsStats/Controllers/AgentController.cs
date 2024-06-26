﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStats_Core.Contracts;
using SportsStats_Core.Models.FootballerModels;
using SportsStats_Infastructure.Enums;
using System.Globalization;
using System.Security.Claims;

namespace SportsStats.Controllers
{
    [Authorize(Roles = "Agent")]
    public class AgentController : BaseController
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
                model.BirthDay = DateTime.Parse("2000-01-01 12:00", CultureInfo.InvariantCulture);
                model.Countries = await countryService.GetAllCuntries();
                model.Positions.Add(Position.Goalkeeper);
                model.Positions.Add(Position.Defender);
                model.Positions.Add(Position.Midfielder);
                model.Positions.Add(Position.Forward);
                model.Feet.Add(Foot.Left);
                model.Feet.Add(Foot.Right);
                model.Teams = await teamService.GetAllTeams();

                return View(model);
            }


            model.AgentId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var isAlredadIn = await footballerService.IsAlreadyIn(model);
            if (isAlredadIn == true)
            {
                model.BirthDay = DateTime.Parse("2000-01-01 12:00", CultureInfo.InvariantCulture);
                model.Countries = await countryService.GetAllCuntries();
                model.Positions.Add(Position.Goalkeeper);
                model.Positions.Add(Position.Defender);
                model.Positions.Add(Position.Midfielder);
                model.Positions.Add(Position.Forward);
                model.Feet.Add(Foot.Left);
                model.Feet.Add(Foot.Right);
                model.Teams = await teamService.GetAllTeams();

                ViewBag.Comment = "Footballer Already Exists";
                return View(model);
            }

            var areDateCorrect = footballerService.AreDtaesCorrect(model);
            if (areDateCorrect == false)
            {
                model.BirthDay = DateTime.Parse("2000-01-01 12:00", CultureInfo.InvariantCulture);
                model.Countries = await countryService.GetAllCuntries();
                model.Positions.Add(Position.Goalkeeper);
                model.Positions.Add(Position.Defender);
                model.Positions.Add(Position.Midfielder);
                model.Positions.Add(Position.Forward);
                model.Feet.Add(Foot.Left);
                model.Feet.Add(Foot.Right);
                model.Teams = await teamService.GetAllTeams();

                ViewBag.Comment = "Football can not be more than 40 years old";
                return View(model);
            }
            await footballerService.CreateFootballerAsync(model);
            return RedirectToAction("MyFootballers", "Footballer");
        }


    }
}
