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
        public StadiumController(
            ICountryService _countryService,
            IStadiumService stadiumService)
        {
            countryService = _countryService;
            this.stadiumService = stadiumService;
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
            await stadiumService.CreateStadiumAsync(stadium);
            return RedirectToAction(nameof(AllStadiums));
        }

        public async Task<IActionResult> AllStadiums()
        {
            var result = await stadiumService.AllAvailableStadiums();
            return View(result);
        }
    }
}
