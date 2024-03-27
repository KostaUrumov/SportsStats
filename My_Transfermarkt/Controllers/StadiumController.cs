using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;

namespace My_Transfermarkt.Controllers
{
    public class StadiumController : BaseController
    {
        private readonly IStadiumService stadiumService;

        public StadiumController(IStadiumService _stadiumService)
        {
            stadiumService = _stadiumService;
        }

        public async Task<IActionResult> AllStadiums()
        {
            var result = await stadiumService.AllAvailableStadiums();
            return View(result);
        }
    }
}
