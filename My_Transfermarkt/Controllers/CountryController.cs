using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;

namespace My_Transfermarkt.Controllers
{
    public class CountryController : Controller
    {
        private readonly IFootballerService footballerService;

        public CountryController(IFootballerService _footballer)
        {
            footballerService = _footballer;
        }
        
    }
}
