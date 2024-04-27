using My_Transfermarkt_Core.Models.GroupModels;

namespace My_Transfermarkt_Core.Contracts
{
    public interface IGroupService
    {
        Task<List<ShowGroupViewModel>> GetAllGroupsForTournament(int tournamentId);
    }
}
