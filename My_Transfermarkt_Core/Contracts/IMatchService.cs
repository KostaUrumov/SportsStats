using My_Transfermarkt_Core.Models.MatchModels;

namespace My_Transfermarkt_Core.Contracts
{
    public interface IMatchService
    {
        bool AreTeamsDifferent(AddNewMatchModel model);
        Task AddNewMatch(AddNewMatchModel model);
    }
}
