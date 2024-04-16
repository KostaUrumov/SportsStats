using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.FootballerModels;
using My_Transfermarkt_Infastructure.Enums;
using System.Security.Claims;
using Umbraco.Core;

namespace My_Transfermarkt.Controllers
{
    public class FootballerController : BaseController
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

        [Authorize(Roles = "Agent")]

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId  = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
           
            var findFootballer = await footballerService.FindFootballer(id);

            if (findFootballer == null)
            {
                ViewBag.comment = "Player do not exist";
                return View("Error404");
            }

            if (findFootballer.isRetired == true)
            {
                ViewBag.comment = "Player is already retired";
                return View("Error404");
            }

            if (findFootballer.AgentId != userId)
            {
                ViewBag.comment = "Not Authorized";
                return View("NotAuthorize");
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

        [Authorize(Roles = "Agent")]
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

        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> MyFootballers()
        {
            var agentId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var result = await footballerService.MyFootballers(agentId);
            return View(result);
        }

        [Authorize(Roles = "Agent")]
        [HttpGet]
        public async Task<IActionResult> SignToClub(int Id)
        {
            var findFootballer = await footballerService.FindFootballer(Id);
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (findFootballer.AgentId != userId)
            {
                ViewBag.comment = "Not Authorized";
                return View("NotAuthorize");
            }

            if (findFootballer.isRetired == true)
            {
                ViewBag.comment = "Player is already retired";
                return View("Error404");
            }

            SignFootballerToATeam model = new SignFootballerToATeam
            {
                Id = Id,
                Teams = await teamService.GetAllTeams()
            };
            model.StartContractDate = DateTime.Now;
            model.EndContractDate = DateTime.Now.AddMonths(6);

            return View(model);
        }

        [Authorize(Roles = "Agent")]
        [HttpPost]
        public async Task<IActionResult> SignToClub(SignFootballerToATeam model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isPlayerAlreadySigned = await footballerService.IsheSignedToAClub(model.Id);
            if (isPlayerAlreadySigned == true)
            {
                
                return RedirectToAction(nameof(MyFootballers));
            }

            var areDtaesCorrect = footballerService.CheckDatesCorrectness(model);
            if (areDtaesCorrect == false)
            {
                ViewBag.Comment = "Shortest contract is 6 months";
                model.Teams = await teamService.GetAllTeams();
                return View(model);
            }
            await footballerService.SignToClub(model);
            return RedirectToAction(nameof(MyFootballers));
        }

        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> Release (int Id)
        {
            var findFootballer = await footballerService.FindFootballer(Id);
            if (findFootballer == null)
            {
                ViewBag.Comment = "Player do not exists";
                return View("Error404");
            }
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (findFootballer.AgentId != userId)
            {
                ViewBag.comment = "Not Authorized";
                return View("NotAuthorize");
            }

            await footballerService.Release(Id);
            return RedirectToAction(nameof(MyFootballers));
        }

        [Authorize(Roles = "Agent")]
        [HttpGet]
        public async Task< IActionResult> UploadPicture(int Id)
        {
            var findFootballer = await footballerService.FindFootballer(Id);
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (findFootballer.AgentId != userId)
            {
                ViewBag.comment = "Not Authorized";
                return View("NotAuthorize");
            }
            if (findFootballer == null)
            {
                return View("Error404");
            }
            return View(Id);
        }

        [Authorize(Roles = "Agent")]
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

        [Authorize(Roles = "Agent, User")]
        
        public async Task<IActionResult> GetAllPlayersForClub(int Id)
        {
            var listedPlayers = await footballerService.GetAllPLayersForClub(Id);
            var team = await teamService.FindTeam(Id);
            if (team == null)
            {
                return View("Error404");
            }
            ViewBag.TeamName = team.TeamName;
            return View(listedPlayers);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details (int Id)
        {
            
            var findFootballer = await footballerService.FindFootballer(Id);
            if (findFootballer == null)
            {

                return View("Error404");
            }

            var result = await footballerService.Details(Id);
            return View(result);
        }

        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> Retire(int Id)
        {
            var findFootballer = await footballerService.FindFootballer(Id);

            if (findFootballer == null)
            {
                ViewBag.comment = "Player do not exist";
                return View("Error404");
            }
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (findFootballer.AgentId != userId)
            {
                ViewBag.comment = "Not Authorized";
                return View("NotAuthorize");
            }

            

            await footballerService.Release(Id);
            await footballerService.RetireFromFootball(Id);
            return RedirectToAction(nameof(MyFootballers));
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> RetiredPlayers()
        {
           
            var getRetiredPlayers = await footballerService.GetRetiredPlayers();
            return View(getRetiredPlayers);
        }

        [Authorize]
        [Authorize(Roles = "Agent, User")]
        public async Task<IActionResult> GetAllFootballers()
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var result = await footballerService.AllFootballers(userId);
            
            return View(result);
        }

        [HttpGet]
        [Authorize(Roles = "Agent, User")]
        public IActionResult SearchFootballersForCountry()
        {
            
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Agent, User")]
        public async Task<IActionResult> SearchFootballersForCountry(SearchByCountryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View (model);
            }


            var isCountryIn = await countryService.FindCountryByname(model.Country);
            if (isCountryIn == null)
            {
                ViewBag.comment = "No Such Country";
                return View("Error404");
            }


            var listetPlayers = await footballerService.GetAllPLayersForCountry(model.Country);
            model.Country = (char.ToUpper(model.Country[0]) + model.Country.Substring(1));
            if (listetPlayers.Count == 0)
            {
                listetPlayers.Add(new ShowFootballerDetailsViewModel()
                {
                    Country = isCountryIn
                });

                ViewBag.comment = "No players for";
                return View("Result", listetPlayers);
            }
            return View("Result", listetPlayers);
        }

        [Authorize(Roles = "Agent, User")]
        public IActionResult Result(List<ShowFootballerDetailsViewModel> listetPlayers)
        {
            return View(listetPlayers);
        }
    }
}
