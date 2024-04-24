using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.RefereeModels;
using My_Transfermarkt_Core.Services;

namespace My_Transfermarkt.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RefereeController : BaseController
    {
        private readonly IRefereeService refService;
        private readonly ICountryService countryService;

        public RefereeController(
            IRefereeService _refService,
            ICountryService _cou)
        {
            refService = _refService;
            countryService = _cou;
        }

        [HttpGet]
        public async Task <IActionResult> AddnewRef()
        {
            AddNewRefereeModel model = new AddNewRefereeModel()
            {
                Countries = await countryService.GetAllCuntries()
            };
            return View (model);
        }

        [HttpPost]
        public async Task<IActionResult> AddnewRef(AddNewRefereeModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Countries = await countryService.GetAllCuntries();
                return View(model);
            }

            var isAlreadyCreated = await refService.ChekIfRefereeExist(model);
            if (isAlreadyCreated == true)
            {
                ViewBag.Comment = "Referee Already Exist";
                model.Countries = await countryService.GetAllCuntries();
                return View(model);
            }

            await refService.AddRefereeAsync(model);
            return RedirectToAction(nameof(AllReferees));
        }

        public async Task<IActionResult> AllReferees()
        {
            var result = await refService.GetAllReferees();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var refereeToEdit = await refService.FindReferee(id);
            if (refereeToEdit == null)
            {
                return View("Error404", new { area = "" });
            }
            refereeToEdit.Countries = await countryService.GetAllCuntries();
            return View(refereeToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddNewRefereeModel model)
        {
            var refereeToEdit = await refService.FindReferee(model.Id);
            if (refereeToEdit == null)
            {
                return View("Error404", new { area = "" });
            }
            await refService.SaveChangesAsync(model);
            return RedirectToAction(nameof(AllReferees));
        }
    }
}
