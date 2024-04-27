using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.GroupModels;
using My_Transfermarkt_Core.Models.TeamModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Transfermarkt_Core.Services
{
    public class GroupService : IGroupService
    {
        private readonly ApplicationDbContext data;

        public GroupService(ApplicationDbContext _data)
        {
            data = _data;
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

