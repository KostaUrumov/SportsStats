using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.UserModels;
using System.Security.Claims;

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

        [HttpPost]
        public async Task<IActionResult> Register (RegisterUserViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Register", model);
            }

            var checkedEmail = await userService.CheckEmailExist(model.Email);

            if (checkedEmail == true)
            {
                return RedirectToAction("Register");
            }

            var checkUsernameUsed = await userService.CheckUserNameExist(model.Username);

            if (checkUsernameUsed == true)
            {
                return RedirectToAction("Register");
            }

            await userService.RegisterNewUserAsync(model);
            await userService.AddToRole(model);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            var user = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await userService.Logout(user);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            LogInUserViewModel model = new LogInUserViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login (LogInUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Login));
            }

            var loggedIn = await userService.LoginAsync(model);

            if (loggedIn == false)
            {
                return RedirectToAction(nameof(Login));
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            ChangePassWordModel model = new ChangePassWordModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassWordModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ChangePassword", model);
            }
            if (model.NewPassword != model.RepeatNewPassword)
            {
                return RedirectToAction("ChangePassword", model);
            }
            model.UserId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var isOldPassTheSame = await userService.CheckIfPasswordMatch(model);
            if (isOldPassTheSame == false)
            {
                return RedirectToAction(nameof(ChangePassword));
            }

            await userService.ChangePassAsync(model);
            return RedirectToAction("Index", "Home");

        }


    }
}
