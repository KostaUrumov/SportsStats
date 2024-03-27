using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.TeamModels;
using My_Transfermarkt_Core.Pagening;

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

            var isThereAlready = await teamService.IsAlreadyCreated(team);
            if (isThereAlready == true)
            {
                ViewBag.Comment = "Team Already Exists";
                team.Countries = await countryService.GetAllCuntries();
                team.Stadiums = await stadiumService.GetAllStadiums();
                return View(team);
            }

            await teamService.AddNewTeamAsync(team);
            return RedirectToAction(nameof(AllTeams));
        }

        public async Task<IActionResult> AllTeams(int pg =1)
        {
            var result = await teamService.GetAllTeamsAvailable();
            const int pageSize = 9;
            if (pg < 1) pg = 1;
            int resCounts =  result.Count();
            var pager = new Pager(resCounts, pg, pageSize);
            int recSkip = (pg-1)* pageSize;
            var data = result.Skip(recSkip).Take(pager.PageSize).ToList();
            pager.TotalPages = resCounts/pageSize;
            this.ViewBag.Pager = pager;
            return View(data);
        }

        [HttpGet]
        public async Task <IActionResult> UploadLogo(int Id)
        {
            var result = await teamService.FindTeamToBeEdited(Id);
            if (result == null)
            {
                return View("Error404", new { area = "" });
            }
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

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var result = await teamService.FindTeamToBeEdited(Id);
            if (result == null)
            {
                return View("Error404", new { area = "" });
            }
            result.Stadiums = await stadiumService.GetAllStadiums();
            result.Countries = await countryService.GetAllCuntries();
            result.Id = Id;
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddNewTeamModel team)
        {
            if (!ModelState.IsValid)
            {
                return View(team);
            }

            var isThereAlready = await teamService.IsAlreadyCreated(team);
            if (isThereAlready == true)
            {
                ViewBag.Comment = "Team Already Exists";
                team.Countries = await countryService.GetAllCuntries();
                team.Stadiums = await stadiumService.GetAllStadiums();
                return View(team);
            }
            await teamService.SaveChangesAsync(team);
            return RedirectToAction(nameof(AllTeams));
        }

        [HttpGet]
        public async Task<IActionResult> AddStadium(int Id)
        {
            var team = await teamService.FindTeam(Id);
            if (team == null)
            {
                return View("Error404", new { area = "" });
            }
            team.Stadiums = await stadiumService.GetAllStadiums();
            return View(team);
        }

        [HttpPost]
        public async Task<IActionResult> AddStadium(TeamToAddStadium model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await teamService.AddToStadiumAsync(model);
            return RedirectToAction(nameof(AllTeams));
        }

        
    }

}
