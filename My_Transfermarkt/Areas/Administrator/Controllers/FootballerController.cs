using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.FootballerModels;
using My_Transfermarkt_Core.Services;
using My_Transfermarkt_Infastructure.Enums;
using System.Security.Claims;
using Umbraco.Core.Services.Implement;

namespace My_Transfermarkt.Areas.Administrator.Controllers
{
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
            var result = await footballerService.AllFootballers();
            return View(result);
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


            await footballerService.SaveChangesAsync(model);

            return RedirectToAction(nameof(GetAllFootballers));
        }
    }
}
