using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.UserModels;
using System.Security.Claims;

namespace My_Transfermarkt.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        public UserController(IUserService _userServ)
        {
            userService = _userServ;
        }

        public IActionResult RegisterNewUser()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            { 
                return RedirectToAction("Index", "Home");
            }
            RegisterUserViewModel model = new RegisterUserViewModel();
            model.Roles.Add(My_Transfermarkt_Infastructure.Enums.Role.Agent);
            model.Roles.Add(My_Transfermarkt_Infastructure.Enums.Role.User);
            model.Roles.Add(My_Transfermarkt_Infastructure.Enums.Role.Admin);
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
                ViewBag.Comment = "Email Already In Use";
                return View(model);
            }

            var checkUsernameUsed = await userService.CheckUserNameExist(model.Username);

            if (checkUsernameUsed == true)
            {
                ViewBag.Comment = "Username Already In Use";
                return View(model);
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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
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
        [Authorize]
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
