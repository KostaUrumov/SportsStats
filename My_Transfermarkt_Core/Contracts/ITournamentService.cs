using My_Transfermarkt_Core.Models.TeamModels;
using My_Transfermarkt_Core.Models.TournamentModels;

namespace My_Transfermarkt_Core.Contracts
{
    public interface ITournamentService
    {
        Task<List<ShowTournamentModel>> GetAllTournaments();
        Task<string> GetName(int id);
    }
}
