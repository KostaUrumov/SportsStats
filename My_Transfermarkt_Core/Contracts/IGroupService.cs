using My_Transfermarkt_Core.Models.GroupModels;
using My_Transfermarkt_Core.Models.TournamentModels;

namespace My_Transfermarkt_Core.Contracts
{
    public interface IGroupService
    {
        Task<List<ShowGroupViewModel>> GetAllGroupsForTournament(int tournamentId);
        Task<int> FindTournament(int groupId);
        Task AddTeamsToGroup(int groupId, int[] teams);
        Task<bool> IsTeamInThisGroup(int groupId, int teamId);
        Task<int> FindGroup(int tournamentId, int teamId);
        public bool NumberOfGroupsAreCorrect(AddNewGroupStageTournament model);
        public bool TeamsInGroupAreCorrect(int numberTeams);
        Task<List<int>> AddRounds(int groupId);
    }
}
