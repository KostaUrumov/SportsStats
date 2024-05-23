using My_Transfermarkt_Core.Models.GeneralModels;
using My_Transfermarkt_Core.Models.TeamModels;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Contracts
{
    public interface ITeamService
    {
        Task AddNewTeamAsync(AddNewTeamModel model);
        Task SaveChangesAsync(AddNewTeamModel model);
        Task<List<ShowTeamModelView>> GetAllTeamsAvailable(int tournamentId);
        Task<List<Team>> GetAllTeamsForTournament(int tournamentId);
        public int TotalTeamNumber();
        Task<List<ShowTeamModelView>> GetTeams(int pageSize, int page);
        Task<List<Team>> GetAllTeams();
        Task AddLogoToTeam(byte[] data, int id);
        Task<AddNewTeamModel?> FindTeamToBeEdited(int id);
        Task<TeamToAddStadium> FindTeam(int teamId);
        Task AddToStadiumAsync(TeamToAddStadium model);
        Task<bool> IsAlreadyCreated(AddNewTeamModel team);
        Task<List<ShowTeamModelView>> GetRandomListForHomePage();
        Task<List<ShowTeamModelView>> FindTeamByCountry(string country);

        Task<List<ResultsViewModel>> FindTeams(string name);
        Task<List<ShowTeamModelView>> CurrentTeamsInTournament(int tournamentId);
        Task<List<Team>> GetTeamsByGroupId(int groupId);
        Task RemoveFromGroup(int teamId, int groupId);
    }
}
