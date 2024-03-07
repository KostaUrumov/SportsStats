using My_Transfermarkt_Core.Models.StadiumModels;

namespace My_Transfermarkt_Core.Contracts
{
    public interface IStadiumService
    {
        Task CreateStadiumAsync(AddNewStadiumModel stadium);
        Task<List<StadiumViewModel>> GetAllStadiums();
    }
}
