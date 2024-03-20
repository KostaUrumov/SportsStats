using My_Transfermarkt_Core.Models.TeamModels;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Contracts
{
    public interface ITeamService
    {
        Task AddNewTeamAsync(AddNewTeamModel model);
        Task SaveChangesAsync(AddNewTeamModel model);
        Task<List<ShowTeamModelView>> GetAllTeamsAvailable();
        Task<List<Team>> GetAllTeams();
        Task AddLogoToTeam(byte[] data, int id);
        Task<AddNewTeamModel> FindTeamToBeEdited(int id);
        Task<TeamToAddStadium> FindTeam(int teamId);
        Task AddToStadiumAsync(TeamToAddStadium model);
        Task<bool> IsAlreadyCreated(AddNewTeamModel team);
        Task<List<ShowTeamModelView>> GetRandomListForHomePage();
    }
}
