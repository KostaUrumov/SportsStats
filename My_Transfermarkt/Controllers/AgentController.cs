using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.FootballerModels;
using My_Transfermarkt_Core.Services;
using My_Transfermarkt_Infastructure.Enums;
using System.Globalization;
using System.Security.Claims;

namespace My_Transfermarkt.Controllers
{
    [Authorize(Roles = "Agent")]
    public class AgentController : BaseController
    {
        private readonly ICountryService countryService;
        private readonly ITeamService teamService;
        private readonly IFootballerService footballerService;
        private readonly IAgentService agentService;
        public AgentController(
            ICountryService _country,
            ITeamService _teamService,
            IFootballerService _football,
            IAgentService _service)
        {
            countryService = _country;
            teamService = _teamService;
            footballerService = _football;
            agentService = _service;
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

        public async Task<IActionResult> SignFootballerToMe(int id)
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var isFootballerIn = await footballerService.FindFootballer(id);
            if (isFootballerIn == null)
            {
                ViewBag.comment = "Player do not exist";
                return View("Error404");
            }
            await agentService.SignFootballerToMe(userId, id);
            return RedirectToAction("MyFootballers", "Footballer");
        }


    }
}
