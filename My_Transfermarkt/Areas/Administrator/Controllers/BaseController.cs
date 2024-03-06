using Microsoft.AspNetCore.Mvc;

namespace My_Transfermarkt.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Route("/Administrator/[controller]/[Action]/{id?}")]
    
    public class BaseController : Controller
    {
        
    }
}
