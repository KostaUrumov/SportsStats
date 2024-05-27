using Microsoft.AspNetCore.Mvc;
using SportsStats_Core.Contracts;

namespace SportsStats.Controllers
{
    public class MatchController : BaseController
    {
        private readonly IMatchService matchService;

        public MatchController(IMatchService _matchService)
        {
            matchService = _matchService;
        }

        public async Task<IActionResult> Details(int matchId)
        {

            return View();
        }
    }
}
