using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.RefereeModels;

namespace My_Transfermarkt.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RefereeController : BaseController
    {
        private readonly IRefereeService refereeController;

        public RefereeController(IRefereeService _refereeController)
        {
            refereeController = _refereeController;
        }

        [HttpGet]
        public  IActionResult AddnewRef()
        {
            AddNewRefereeModel model = new AddNewRefereeModel();
            return View (model);
        }
    }
}
