using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Models.FootballModels;

namespace My_Transfermarkt.Controllers
{
    [Authorize(Roles = "Agent")]
    public class AgentController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> AddFootballer()
        {
            AddNewFootallerModel footballer = new AddNewFootallerModel();

            return View(footballer);
        }
    }
}
