using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Infastructure.Enums;
using System.Security.Claims;

namespace My_Transfermarkt.Controllers
{
    public class FootballerController : Controller
    {
        private readonly IFootballerService footballerService;
        private readonly ICountryService countryService;
        private readonly ITeamService teamService;

        public FootballerController(
            IFootballerService _footballervice,
            ICountryService _countryService,
            ITeamService _teamService)
        {
            footballerService = _footballervice;
            countryService = _countryService;
            teamService = _teamService;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId  = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var findFootballer = await footballerService.FindFootballer(id);
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
    }
}
