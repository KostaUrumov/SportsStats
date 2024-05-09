using My_Transfermarkt_Core.Models.MatchModels;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Contracts
{
    public interface IMatchService
    {
        bool AreTeamsDifferent(AddNewMatchModel model);
        Task AddNewMatch(AddNewMatchModel model);
        Task<Match> FindMatch(int matchId);
        Task SaveChanges(AddNewMatchModel model);
    }
}
