using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.CountryModels;

namespace My_Transfermarkt.Areas.Administrator.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CountryController : BaseController
    {
        private readonly ICountryService countryService;

        public CountryController(
            ICountryService _countryServ)
        {
            countryService = _countryServ;
        }
        [HttpGet]
        public IActionResult AddNewCountry()
        {
            AddNewCountryModel model  = new AddNewCountryModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCountry(AddNewCountryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await countryService.AddCountryAsync(model);

            return RedirectToAction(nameof(AllCountries));
        }

        public async Task<IActionResult> AllCountries()
        {
            var displayResult =  await countryService.AllCountriesAsync();
            return View(displayResult);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            EditCountryModel model = await countryService.FindCountry(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCountryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await countryService.SaveChangesAsync(model);

            return RedirectToAction(nameof(AllCountries));
        }
    }
}
