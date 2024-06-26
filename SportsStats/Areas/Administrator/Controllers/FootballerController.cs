﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStats_Core.Contracts;
using SportsStats_Core.Models.FootballerModels;
using SportsStats_Infastructure.Enums;
using System.Globalization;
using System.Security.Claims;

namespace SportsStats.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FootballerController : BaseController
    {
        private readonly IFootballerService footballerService;
        private readonly ICountryService countryService;
        private readonly ITeamService teamService;

        public FootballerController(
            IFootballerService _footballerService,
            ICountryService _country,
            ITeamService _teamService)
        {
            footballerService = _footballerService;
            countryService = _country;
            teamService = _teamService;

        }
        public async Task<IActionResult> GetAllFootballers()
        {

            var user = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            {
                if (user != null)
                {
                    var result = await footballerService.AllFootballers(user);
                    return View(result);
                }
            }

            return View();

        }
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var findFootballer = await footballerService.FindFootballer(id);

            if (findFootballer == null)
            {
                return View("Error404");
            }
            findFootballer.Countries = await countryService.GetAllCuntries();
            findFootballer.Teams = await teamService.GetAllTeams();
            findFootballer.Positions.Add(Position.Goalkeeper);
            findFootballer.Positions.Add(Position.Defender);
            findFootballer.Positions.Add(Position.Midfielder);
            findFootballer.Positions.Add(Position.Forward);
            findFootballer.Feet.Add(Foot.Left);
            findFootballer.Feet.Add(Foot.Right);
            ViewBag.FootballerName = findFootballer.FirstName + " " + findFootballer.LastName;
            return View(findFootballer);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(AddNewFootallerModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

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
            await footballerService.SaveChangesAsync(model);

            return RedirectToAction(nameof(GetAllFootballers));
        }
    }
}
