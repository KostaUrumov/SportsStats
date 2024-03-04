using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.UserModels;

namespace My_Transfermarkt.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService _userServ)
        {
            userService = _userServ;
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterUserViewModel model = new RegisterUserViewModel();
            return View(model);
        }
    }
}
