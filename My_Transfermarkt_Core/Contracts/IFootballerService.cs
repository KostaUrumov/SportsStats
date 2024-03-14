using My_Transfermarkt_Core.Models.FootballModels;

namespace My_Transfermarkt_Core.Contracts
{
    public interface IFootballerService
    {
        Task CreateFootballerAsync(AddNewFootallerModel footballer);
        Task<bool> IsAlreadyIn(AddNewFootallerModel model);
        bool AreDtaesCorrect(AddNewFootallerModel model);
        Task<List<ShowFootballerModel>> MyFootballers(string userId);
        Task SaveChangesAsync(AddNewFootallerModel footballer);
        Task<AddNewFootallerModel> FindFootballer(int id);
        Task SignToClub(SignFootballerToATeam model);
        Task Release(int Id);
        Task AddPictureToFootballer(byte[]pictureData, int Id);
    }
}
