using My_Transfermarkt_Core.Models.RefereeModels;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Contracts
{
    public interface IRefereeService
    {
        Task<bool> ChekIfRefereeExist(AddNewRefereeModel model);
        Task AddRefereeAsync(AddNewRefereeModel model);
        Task<List<ShowRefereeModel>> GetAllReferees();
        Task<AddNewRefereeModel> FindReferee(int refereeId);
        Task SaveChangesAsync(AddNewRefereeModel model);
        Task<List<Referee>> AllReferees();
    }
}
