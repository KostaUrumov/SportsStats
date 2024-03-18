﻿using My_Transfermarkt_Core.Models.FootballerModels;

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
        Task <List<ShowFootballerToClubModel>> GetAllPLayersForClub(int clubId);
        Task<ShowFootballerDetailsViewModel> Details(int playerId);
        Task RetireFromFootball(int footballerId);
        Task<List<ShowFootballerDetailsViewModel>> GetRetiredPlayers();
    }
}
