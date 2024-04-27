using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.GroupModels;
using My_Transfermarkt_Core.Models.TeamModels;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Services
{
    public class GroupService : IGroupService
    {
        private readonly ApplicationDbContext data;

        public GroupService(ApplicationDbContext _data)
        {
            data = _data;
        }

        public async Task<string> FindGroup(int groupId)
        {
            var findGroup = await data.Groups.FirstAsync(x => x.Id == groupId);
            return findGroup.Name;
        }

        public async Task<int> FindTournament(int groupId)
        {
            var findGroup = await data.Groups.FirstAsync(x => x.Id == groupId);
            return (int)findGroup.TournamentID;
        }

        public async Task<List<ShowGroupViewModel>> GetAllGroupsForTournament(int tournamentId)
        {
            List<ShowGroupViewModel> groups = await data
                .Groups
                .Where(x=> x.TournamentID == tournamentId)
                .Select(g=> new ShowGroupViewModel
                {
                    Name = g.Name,
                    Id = g.Id,
                    Teams = (ICollection<Models.TeamModels.ShowTeamModelView>)g.Teams
                    .Select(x=> new ShowTeamModelView
                    {
                        Name = x.Name
                    })
                })
                .OrderBy(x => x.Name)
                .ToListAsync();
            return groups;
        } 
    }
}

