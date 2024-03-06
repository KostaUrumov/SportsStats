using My_Transfermarkt_Core.Models.UserModels;

namespace My_Transfermarkt_Core.Contracts
{
    public interface IUserService
    {
        Task RegisterNewUserAsync(RegisterUserViewModel model);
        Task Logout(string user);
        Task<bool> LoginAsync(LogInUserViewModel model);
        Task<bool> CheckEmailExist(string email);
        Task<bool> CheckUserNameExist(string username);
        Task AddToRole(RegisterUserViewModel model);
        Task<bool> CheckIfPasswordMatch(ChangePassWordModel model);
        Task ChangePassAsync(ChangePassWordModel model);
    }
}
