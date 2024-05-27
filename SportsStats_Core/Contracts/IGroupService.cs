using SportsStats_Core.Models.GroupModels;
using SportsStats_Core.Models.TeamModels;
using SportsStats_Core.Models.TournamentModels;

namespace SportsStats_Core.Contracts
{
    public interface IGroupService
    {
        Task<List<ShowGroupViewModel>> GetAllGroupsForTournament(int tournamentId);
        Task<int> FindTournament(int groupId);
        Task AddTeamsToGroup(int groupId, int[] teams);
        Task<bool> IsTeamInThisGroup(int groupId, int teamId);
        Task<int?> FindGroup(int tournamentId, int? teamId);
        public bool NumberOfGroupsAreCorrect(AddNewGroupStageTournament model);
        public bool TeamsInGroupAreCorrect(int numberTeams);
        Task<List<int>> AddRounds(int groupId);
        Task RemoveAllGroups(int tournamentId);
        Task<List<StandingsViewModel>> GetDetails(int groupId);
    }
}
