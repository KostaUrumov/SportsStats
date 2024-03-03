using Microsoft.AspNetCore.Mvc;

namespace My_Transfermarkt.Areas.Administrator.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
