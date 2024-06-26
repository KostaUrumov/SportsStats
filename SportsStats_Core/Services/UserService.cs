﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportsStats_Core.Contracts;
using SportsStats_Core.Models.UserModels;
using SportsStats_Infastructure.Data;
using System.Security.Cryptography;
using User = SportsStats_Infastructure.DataModels.User;

namespace SportsStats_Core.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public UserService(
            ApplicationDbContext _data,
            SignInManager<User> _signInManager,
            UserManager<User> _userManager,
            RoleManager<IdentityRole> _roleManager)

        {
            data = _data;
            signInManager = _signInManager;
            userManager = _userManager;
            roleManager = _roleManager;
        }



        /// <summary>
        /// Method adds user to a role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddToRole(RegisterUserViewModel model)
        {
            var findUser = data.Users.First(x => x.UserName == model.Username);
            if (model.Role.ToString() == "Admin")
            {
                await userManager.AddToRoleAsync(findUser, "Admin");
            }

            if (model.Role.ToString() == "Agent")
            {
                await userManager.AddToRoleAsync(findUser, "Agent");
            }

            if (model.Role.ToString() == "User")
            {
                await userManager.AddToRoleAsync(findUser, "User");
            }
            await data.SaveChangesAsync();
        }



        /// <summary>
        /// Method changes user`s password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task ChangePassAsync(ChangePassWordModel model)
        {
            var findUser = await data.Users.FirstAsync(x => x.Id == model.UserId);
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(model.NewPassword, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            findUser.PasswordHash = savedPasswordHash;

            await data.SaveChangesAsync();

        }



        /// <summary>
        /// Method checks if e-mail is already in the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns>bool</returns>
        public async Task<bool> CheckEmailExist(string email)
        {
            var emailIsThere = await data.Users.FirstOrDefaultAsync
                (e => e.Email == email);
            if (emailIsThere != null)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Method checks if password and entered password in a log in are matching
        /// </summary>
        /// <param name="model"></param>
        /// <returns>bool</returns>
        public async Task<bool> CheckIfPasswordMatch(ChangePassWordModel model)
        {
            var findUser = await data.Users.FirstAsync(x => x.Id == model.UserId);
            string savedPasswordHash = findUser.PasswordHash;

            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(model.CurrentPassword, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }

            }

            return true;

        }


        /// <summary>
        /// Method checks if username is already in the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns>bool</returns>
        public async Task<bool> CheckUserNameExist(string username)
        {
            var usernameIsThere = await data.Users.FirstOrDefaultAsync
                (e => e.UserName == username);
            if (usernameIsThere != null)
            {
                return true;
            }

            return false;

        }


        /// <summary>
        /// Method logs in user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>bool</returns>
        public async Task<bool> LoginAsync(LogInUserViewModel model)
        {
            var findUser = await data.Users.FirstOrDefaultAsync(x => x.UserName == model.Username);
            if (findUser == null)
            {
                return false;
            }
            string savedPasswordHash = findUser.PasswordHash;

            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(model.Password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }

            }

            await signInManager.SignInAsync(findUser, isPersistent: false);
            return true;
        }



        /// <summary>
        /// Method logs out user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Logout(string user)
        {
            var foundUser = await data
                .Users.FirstAsync(x => x.Id == user);

            await signInManager.SignOutAsync();
        }


        /// <summary>
        /// Method registers new user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task RegisterNewUserAsync(RegisterUserViewModel model)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(model.Password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            User user = new User()
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                PasswordHash = savedPasswordHash
            };
            await userManager.CreateAsync(user);


            await data.SaveChangesAsync();



        }
    }
}
