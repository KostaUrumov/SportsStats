using SportsStats_Core.Models.GeneralModels;
using SportsStats_Core.Models.StadiumModels;
using SportsStats_Infastructure.DataModels;

namespace SportsStats_Core.Contracts
{
    public interface IStadiumService
    {
        Task CreateStadiumAsync(AddNewStadiumModel stadium);
        Task<List<StadiumViewModel>> AllAvailableStadiums();
        Task<List<Stadium>> GetAllStadiums();
        Task<AddNewStadiumModel> FindToEdit(int id);
        Task SaveChangesAsync(AddNewStadiumModel model);
        Task RemoveStadium(int stadiumId);
        Task<bool> IsStadiumAlreadyIn(AddNewStadiumModel model);
        Task<List<ResultsViewModel>> FindStadiums(string name);

    }
}
