﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStats_Core.Contracts;
using SportsStats_Core.Models.UserModels;
using SportsStats_Infastructure.Enums;
using System.Security.Claims;

namespace SportsStats.Controllers
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
            model.Roles.Add(Role.Agent);
            model.Roles.Add(Role.User);
            model.Roles.Add(Role.Admin);
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Register", model);
            }


            var checkedEmail = await userService.CheckEmailExist(model.Email);

            if (checkedEmail == true)
            {
                ViewBag.Comment = "Email Already In Use";
                model.Roles.Add(Role.Agent);
                model.Roles.Add(Role.User);
                model.Roles.Add(Role.Admin);
                return View(model);
            }

            var checkUsernameUsed = await userService.CheckUserNameExist(model.Username);

            if (checkUsernameUsed == true)
            {
                ViewBag.Comment = "Username Already In Use";
                model.Roles.Add(Role.Agent);
                model.Roles.Add(Role.User);
                model.Roles.Add(Role.Admin);
                return View(model);
            }


            await userService.RegisterNewUserAsync(model);
            await userService.AddToRole(model);

            return RedirectToAction(nameof(Login));
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
        public async Task<IActionResult> Login(LogInUserViewModel model)
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
