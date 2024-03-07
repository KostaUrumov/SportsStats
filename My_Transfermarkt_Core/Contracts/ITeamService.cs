using My_Transfermarkt_Core.Models.TeamModels;

namespace My_Transfermarkt_Core.Contracts
{
    public interface ITeamService
    {
        Task AddNewTeamAsync(AddNewTeamModel model);
        Task SaveChangesAsync(AddNewTeamModel model);
        Task<List<ShowTeamModelView>> GetAllTeamsAvailable();
        Task AddLogoToTeam(byte[] data, int id);
        Task<AddNewTeamModel> FindTeamToBeEdited(int id);
    }
}
