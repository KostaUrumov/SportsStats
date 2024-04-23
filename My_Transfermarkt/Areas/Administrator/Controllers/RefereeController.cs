using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.RefereeModels;

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
    }
}
