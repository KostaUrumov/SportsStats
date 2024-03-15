using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.FootballerModels;
using My_Transfermarkt_Infastructure.Enums;
using System.Runtime.CompilerServices;
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

        [HttpPost]
        public async Task<IActionResult> Edit(AddNewFootallerModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            

            await footballerService.SaveChangesAsync(model);

            return RedirectToAction(nameof(MyFootballers));
        }

        public async Task<IActionResult> MyFootballers()
        {
            var agentId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var result = await footballerService.MyFootballers(agentId);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> SignToClub(int Id)
        {
            SignFootballerToATeam model = new SignFootballerToATeam
            {
                Id = Id,
                Teams = await teamService.GetAllTeams()
            };
            model.StartContractDate = DateTime.Now;
            model.EndContractDate = DateTime.Now.AddMonths(6);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignToClub(SignFootballerToATeam model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            await footballerService.SignToClub(model);
            return RedirectToAction(nameof(MyFootballers));
        }

        public async Task<IActionResult> Release (int Id)
        {
            await footballerService.Release(Id);
            return RedirectToAction(nameof(MyFootballers));
        }

        [HttpGet]
        public IActionResult UploadPicture(int Id)
        {
            return View(Id);
        }

        [HttpPost]
        public async Task<IActionResult> UploadPicture(IFormFileCollection files, int Id)
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
            await footballerService.AddPictureToFootballer(data, Id);
            return RedirectToAction(nameof(MyFootballers));
        }

        public async Task<IActionResult> GetAllPlayersForClub(int Id)
        {
            var listedPlayers = await footballerService.GetAllPLayersForClub(Id);
            return View(listedPlayers);
        }

        public async Task<IActionResult> Details (int Id)
        {
            var result = await footballerService.Details(Id);
            return View(result);
        }
    }
}
