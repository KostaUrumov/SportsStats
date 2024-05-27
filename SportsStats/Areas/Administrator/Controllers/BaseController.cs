using Microsoft.AspNetCore.Mvc;

namespace SportsStats.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Route("/Administrator/[controller]/[Action]/{id?}")]

    [AutoValidateAntiforgeryToken]
    public class BaseController : Controller
    {

    }
}
