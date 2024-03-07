using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.TeamModels;

namespace My_Transfermarkt.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TeamController : BaseController
    {
        private readonly ICountryService countryService;
        private readonly IStadiumService stadiumService;
        private readonly ITeamService teamService;

        public TeamController(
            ICountryService _country,
            IStadiumService _stadium,
            ITeamService _team)
        {
            countryService = _country;
            stadiumService = _stadium;
            teamService = _team;
        }

        [HttpGet]
        public async Task<IActionResult> AddNewTeam()
        {
            AddNewTeamModel team = new AddNewTeamModel();
            team.Countries = await countryService.GetAllCuntries();
            team.Stadiums = await stadiumService.GetAllStadiums();
            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewTeam(AddNewTeamModel team)
        {
            if (!ModelState.IsValid)
            {
                return View(team);
            }
            
            await teamService.AddNewTeamAsync(team);
            return RedirectToAction(nameof(AllTeams));
        }

        public async Task<IActionResult> AllTeams()
        {
            var result = await teamService.GetAllTeamsAvailable();
            return View(result);
        }

        [HttpGet]
        public IActionResult UploadLogo(int Id)
        {
            return View(Id);
        }

        [HttpPost]
        public async Task<IActionResult> UploadLogo(IFormFileCollection files, int Id)
        {
            byte[] data = new byte[files.Count];
            foreach (var file in files)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    data = ms.ToArray();
                }
            }
            await teamService.AddLogoToTeam(data, Id);
            return RedirectToAction(nameof(AllTeams));
        }

    }

}
