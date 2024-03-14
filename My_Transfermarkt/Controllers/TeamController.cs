using Microsoft.AspNetCore.Mvc;

namespace My_Transfermarkt.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
