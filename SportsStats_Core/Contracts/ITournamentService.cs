using SportsStats_Core.Models.MatchModels;
using SportsStats_Core.Models.TeamModels;
using SportsStats_Core.Models.TournamentModels;
using SportsStats_Infastructure.DataModels;

namespace SportsStats_Core.Contracts
{
    public interface ITournamentService
    {
        Task<List<ShowTournamentModel>> GetAllTournaments();
        Task<string> GetName(int id);
        Task AddTeamToTournament(int tournamentiD, int teamId);
        Task<object?> FindTournament(int toiurnamentId);
        Task<TournamentViewModel> GetDetails(int tournamentId);
        Task<bool> IsTeamInTournament(int tournamentId, int teamId);
        Task<Tournament?> CheckIfTournamentIsIn(string tournamentName);
        Task AddNewTournamentAsync(object tournament);
        Task SaveChangesAsync(EditTournamentModel model);
        Task RemoveFromTournament(int tournamentiD, int teamId);
        Task<List<ShowMatchModel>> FindMatchesInTournament(int tourneyId);
        Task<bool> IsItGroupStageTournament(int tourId);
        Task RemoveFromGroup(int tournamentId, int teamId);
        public bool AreDatesCorrect(object tournament);
        public bool TotalTeamsAreCorrect(int numberOfTeams);
        public Task<List<int>> AddRounds(int tournamentId);
        public Task<int?> FindTournamentIdByGroup(int groupId);
        Task<List<ShowMatchModel>> FindMatchesInGroup(int groupId);
        Task<List<StandingsViewModel>> StandongsInTournament(int tourId);
        Task<List<ShowMatchModel>> FindMatchesPerTeamInTournament(int teamId, int TourId);
    }
}
