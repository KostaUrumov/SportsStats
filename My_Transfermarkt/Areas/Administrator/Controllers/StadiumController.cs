using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.StadiumModels;

namespace My_Transfermarkt.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StadiumController : BaseController
    {
        private readonly ICountryService countryService;
        public StadiumController(ICountryService _countryService)
        {
            countryService = _countryService;
        }

        [HttpGet]
        public async Task<IActionResult> AddNewStadium()
        {
            AddNewStadiumModel stadium = new AddNewStadiumModel();
            stadium.Countries = await countryService.GetAllCuntries();

            return View(stadium);
        }
    }
}
