using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.StadiumModels;
using NPoco.Expressions;
using System.Globalization;

namespace My_Transfermarkt.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StadiumController : BaseController
    {
        private readonly ICountryService countryService;
        private readonly IStadiumService stadiumService;
        private readonly ITeamService teamService;
        public StadiumController(
            ICountryService _countryService,
            IStadiumService _stadiumService,
            ITeamService _teamService)
        {
            countryService = _countryService;
            stadiumService = _stadiumService;
            teamService = _teamService;
        }

        [HttpGet]
        public async Task<IActionResult> AddNewStadium()
        {
            AddNewStadiumModel stadium = new AddNewStadiumModel();
            stadium.Countries = await countryService.GetAllCuntries();
            stadium.Build = DateTime.Parse("1980-01-01 12:00", CultureInfo.InvariantCulture);
            return View(stadium);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewStadium (AddNewStadiumModel stadium)
        {
            if (!ModelState.IsValid)
            {
                return View(stadium);
            }
            var isStadiumAlreadyIn = await stadiumService.IsStadiumAlreadyIn(stadium);
            if (isStadiumAlreadyIn == true)
            {
                return View(stadium);
            }

            await stadiumService.CreateStadiumAsync(stadium);
            return RedirectToAction(nameof(AllStadiums));
        }

        public async Task<IActionResult> AllStadiums()
        {
            var result = await stadiumService.AllAvailableStadiums();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit (int id)
        {
            var model = await stadiumService.FindToEdit(id);
            model.Countries = await countryService.GetAllCuntries();
            return View(model);
        }

        public async Task<IActionResult> Edit(AddNewStadiumModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var isStadiumAlreadyIn = await stadiumService.IsStadiumAlreadyIn(model);
            if (isStadiumAlreadyIn == true)
            {
                return View(model);
            }
            await stadiumService.SaveChangesAsync(model);
            return RedirectToAction(nameof(AllStadiums));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await stadiumService.RemoveStadium(Id);

            return RedirectToAction(nameof(AllStadiums));
        }

    }
}
