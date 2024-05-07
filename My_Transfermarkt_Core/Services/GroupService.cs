using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.GroupModels;
using My_Transfermarkt_Core.Models.TeamModels;
using My_Transfermarkt_Core.Models.TournamentModels;
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

        public async Task<List<int>> AddRounds(int groupId)
        {
            List<int> rounds = new List<int>();

            var group = await data.Groups.FirstOrDefaultAsync(x => x.Id == groupId);
            for (int i = 0; i < group.NumberOfRounds; i++)
            {
                rounds.Add(i + 1);
            }
            return rounds;
        }

        public async Task AddTeamsToGroup(int groupId, int[] teams)
        {
            var group = await data.Groups.FirstAsync(g=> g.Id == groupId);
            List<GroupTeams> newGroupTeams = new List<GroupTeams>();
            
            for (int i = 0; i < teams.Length; i++)
            {
                var team = await data.Teams.FirstOrDefaultAsync(t=> t.Id == teams[i]);

                GroupTeams groupTeam = new GroupTeams()
                {
                    TeamId = team.Id,
                    GroupId = groupId

                };
                newGroupTeams.Add(groupTeam);
                
            }
            group.Teams = newGroupTeams;
            data.AddRange(newGroupTeams);
            await data.SaveChangesAsync();
        }

        public async Task<int> FindGroup(int tournamentId, int teamId)
        {
            int number = 0;
            var tournament = await data
                .GroupStageTournaments
                .Where(t=> t.Id == tournamentId)
                .ToListAsync();
            var findGroup = await data.GroupsTeams.FirstOrDefaultAsync(x => x.TeamId == teamId && x.Group.TournamentID == tournamentId);
            return number;
        }

        public async Task<int> FindTournament(int groupId)
        {
            var group = await data.Groups.FirstAsync(g=> g.Id == groupId);
            return (int)group.TournamentID;
            
        }

        public async Task<List<ShowGroupViewModel>> GetAllGroupsForTournament(int tournamentId)
        {
            List<ShowGroupViewModel> groups = await data
                .GroupsTournaments
                .Where(x=> x.TournamenId == tournamentId)
                .Select(g=> new ShowGroupViewModel
                {
                    TournamentName = g.Tournament.Name,
                    Name = g.Group.Name,
                    Id = g.GroupId,
                    Teams = (ICollection<Models.TeamModels.ShowTeamModelView>)g.Group.Teams
                    .Select(x=> new ShowTeamModelView
                    {
                        Name = x.Team.Name,
                    })
                })
                .OrderBy(x => x.Name)
                .ToListAsync();
            return groups;
        }

        public async Task<bool> IsTeamInThisGroup(int groupId, int teamId)
        {
            var group = await data.Groups.FindAsync(groupId);
            if (group.Teams.Count == 0)
            {
                return false;
            }
            foreach (var item in group.Teams)
            {
                if (item.TeamId == teamId)
                {
                    return true;
                }
            }
            return false;
        }

        public bool NumberOfGroupsAreCorrect(AddNewGroupStageTournament model)
        {
            if (model.NumberOfGroups < 2 || model.NumberOfGroups > 25)
            {
                return false;
            }

            return true;
        }

        public bool TeamsInGroupAreCorrect(int numberTeams)
        {
            if (numberTeams < 2 || numberTeams > 12)
            {
                return false;
            }

            return true;
        }
    }
}

