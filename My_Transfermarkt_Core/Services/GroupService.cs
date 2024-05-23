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
            var group = await data.Groups.FirstAsync(g => g.Id == groupId);
            List<GroupTeams> newGroupTeams = new List<GroupTeams>();
            var teamsInGroup = await data
                .GroupsTeams
                .Where(t => t.GroupId == group.Id)
                .ToListAsync();
            if (teamsInGroup.Count < group.TeamsNumber)
            {
                for (int i = 0; i < teams.Length; i++)
                {
                    var team = await data.Teams.FirstOrDefaultAsync(t => t.Id == teams[i]);

                    GroupTeams groupTeam = new GroupTeams()
                    {
                        TeamId = team.Id,
                        GroupId = groupId

                    };
                    newGroupTeams.Add(groupTeam);
                    data.AddRange(newGroupTeams);
                }
            }

            await data.SaveChangesAsync();
        }

        public async Task<int?> FindGroup(int tournamentId, int? teamId)
        {
            if (teamId == null)
            {
                var group = await data
                    .Groups
                    .FirstOrDefaultAsync(x => x.TournamentID == tournamentId);
                if (group == null)
                {
                    return null;
                }
                return group.NumberOfRounds;
            }
            var tournament = await data
                .GroupStageTournaments
                .Where(t => t.Id == tournamentId)
                .ToListAsync();
            var findGroup = await data.GroupsTeams.FirstOrDefaultAsync(x => x.TeamId == teamId && x.Group.TournamentID == tournamentId);
            if (findGroup == null)
            {
                return null;
            }
            return findGroup.GroupId;
        }

        public async Task<int> FindTournament(int groupId)
        {
            var group = await data.Groups.FirstAsync(g => g.Id == groupId);
            return (int)group.TournamentID;

        }

        public async Task<List<ShowGroupViewModel>> GetAllGroupsForTournament(int tournamentId)
        {
            List<ShowGroupViewModel> groups = await data
                .GroupsTournaments
                .Where(x => x.TournamenId == tournamentId)
                .Select(g => new ShowGroupViewModel
                {
                    TournamentName = g.Tournament.Name,
                    TournamentId = g.Tournament.Id,
                    Name = g.Group.Name,
                    Id = g.GroupId,
                    Teams = (ICollection<Models.TeamModels.ShowTeamModelView>)g.Group.Teams
                    .Select(x => new ShowTeamModelView
                    {
                        Name = x.Team.Name,
                        Picture = x.Team.Logo
                    })
                })
                .OrderBy(x => x.Name)
                .ToListAsync();

            for (int i = 0; i < groups.Count; i++)
            {
                foreach (var match in data.Matches)
                {
                    if (groups[i].Id == match.GroupId)
                    {
                        groups[i].AreThereAnyMatches = true;
                    }
                }
            }
            return groups;
        }

        public async Task<List<StandingsViewModel>> GetDetails(int groupId)
        {
            List<StandingsViewModel> listed = await data
                .GroupsTeams
                .Where(x => x.GroupId == groupId)
                .Select(x => new StandingsViewModel
                {
                    Team = x.Team,
                    TeamName = x.Team.Name,
                    Picture = x.Team.Logo
                })
                .ToListAsync();


            for (int i = 0; i < listed.Count; i++)
            {
                foreach (var match in data.Matches)
                {
                    if (match.GroupId == groupId && match.HomeTeamId == listed[i].Team.Id)
                    {
                        listed[i].Matches += 1;
                        if (match.HomeScore != null && match.AwayScore != null)
                        {
                            if (match.HomeScore > match.AwayScore)
                            {
                                listed[i].Points += 3;
                                listed[i].Wins += 1;
                                listed[i].GoalsFor += (int)match.HomeScore;
                                listed[i].GoalsAgainst += (int)match.AwayScore;
                                ;
                            }

                            if (match.HomeScore == match.AwayScore)
                            {
                                listed[i].Points += 1;
                                listed[i].Draws += 1;
                                listed[i].GoalsFor += (int)match.HomeScore;
                                listed[i].GoalsAgainst += (int)match.AwayScore;
                            }
                            if (match.HomeScore < match.AwayScore)
                            {
                                listed[i].Losses += 1;
                                listed[i].GoalsFor += (int)match.HomeScore;
                                listed[i].GoalsAgainst += (int)match.AwayScore;
                            }
                        }

                    }

                    if (match.GroupId == groupId && match.AwayTeamId == listed[i].Team.Id)
                    {
                        listed[i].Matches += 1;
                        if (match.AwayScore != null && match.HomeScore != null)
                        {

                            if (match.AwayScore > match.HomeScore)
                            {
                                listed[i].Points += 3;
                                listed[i].Wins += 1;
                                listed[i].GoalsFor += (int)match.AwayScore;
                                listed[i].GoalsAgainst += (int)match.HomeScore;
                            }

                            if (match.AwayScore == match.HomeScore)
                            {
                                listed[i].Points += 1;
                                listed[i].Draws += 1;
                                listed[i].GoalsFor += (int)match.AwayScore;
                                listed[i].GoalsAgainst += (int)match.HomeScore;
                            }

                            if (match.AwayScore < match.HomeScore)
                            {
                                listed[i].Losses += 1;
                                listed[i].GoalsFor += (int)match.AwayScore;
                                listed[i].GoalsAgainst += (int)match.HomeScore;
                            }
                        }
                    }
                }

            }

            var sortedList = listed.OrderByDescending(x => x.Points).ToList();
            return sortedList;


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

        public async Task RemoveAllGroups(int tournamentId)
        {
            var groups = await
                data.GroupsTournaments
                .Where(x => x.TournamenId == tournamentId)
                .ToListAsync();

            data.RemoveRange(groups);
            await data.SaveChangesAsync();
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

