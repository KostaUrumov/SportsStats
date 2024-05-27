using SportsStats_Core.Models.FootballerModels;
using SportsStats_Core.Models.GeneralModels;

namespace SportsStats_Core.Contracts
{
    public interface IFootballerService
    {
        Task CreateFootballerAsync(AddNewFootallerModel footballer);
        Task<bool> IsAlreadyIn(AddNewFootallerModel model);
        bool AreDtaesCorrect(AddNewFootallerModel model);
        Task SaveChangesAsync(AddNewFootallerModel footballer);
        Task<AddNewFootallerModel?> FindFootballer(int id);
        Task SignToClub(SignFootballerToATeam model);
        Task Release(int Id);
        Task AddPictureToFootballer(byte[] pictureData, int Id);
        Task<List<ShowFootballerToClubModel>> GetAllPLayersForClub(int clubId);

        Task<List<ShowFootballerDetailsViewModel>> GetAllPLayersForCountry(string countryName);
        Task<ShowFootballerDetailsViewModel> Details(int playerId);
        Task RetireFromFootball(int footballerId);
        Task<List<ShowFootballerDetailsViewModel>> GetRetiredPlayers();
        bool CheckDatesCorrectness(SignFootballerToATeam model);
        Task<bool> IsheSignedToAClub(int id);
        Task<List<AllFootballersViewModel>> AllFootballers(string username);
        Task<List<ResultsViewModel>> FindFootballers(string search);
    }
}
