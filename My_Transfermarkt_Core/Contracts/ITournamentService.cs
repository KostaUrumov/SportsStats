using My_Transfermarkt_Core.Models.TeamModels;
using My_Transfermarkt_Core.Models.TournamentModels;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Contracts
{
    public interface ITournamentService
    {
        Task<List<ShowTournamentModel>> GetAllTournaments();
        Task<string> GetName(int id);
        Task AddTeamToTournament(int tournamentiD, int teamId);
        Task<Tournament> FindTournament(int toiurnamentId);
        Task<TournamentViewModel> GetDetails(int tournamentId);
    }
}
