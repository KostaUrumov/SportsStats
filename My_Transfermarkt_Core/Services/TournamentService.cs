using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.MatchModels;
using My_Transfermarkt_Core.Models.TeamModels;
using My_Transfermarkt_Core.Models.TournamentModels;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly ApplicationDbContext data;
        public TournamentService(
            ApplicationDbContext _data)
        {
            data = _data;
        }


        /// <summary>
        /// Method adds a new tournament into database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddNewTournamentAsync(object tournamentModel)
        {
            if (tournamentModel.GetType() == typeof(AddNewSingleGroupTournamentModel))
            {
                
                var model = (AddNewSingleGroupTournamentModel)tournamentModel;
                
                var tour = new SingleGroupTournament();
                tour.Name = model.Name;
                tour.StartDate = model.StartDate;
                tour.EndDate = model.EndtDate;
                tour.NumberOfTeams = model.NumberOfTeams;
                tour.Rounds = (model.NumberOfTeams - 1) * 2;
                data.SingleGroupTournaments.Add(tour);
            }

            if (tournamentModel.GetType() == typeof(AddNewGroupStageTournament))
            {
                var model = (AddNewGroupStageTournament)tournamentModel;
                var tour = new GroupStageTournament();
                tour.Name = model.Name;
                tour.StartDate = model.StartDate;
                tour.EndDate = model.EndtDate;
                tour.NumberOfTeams = model.NumberOfTeams;
                tour.NumberOfGroups = model.NumberOfGroups;


                for (int i = 0; i < model.NumberOfGroups; i++)
                {
                    string groupLetter = Convert.ToChar(65 + i).ToString();
                    Group group = new Group
                    {
                        Name = "Group " + groupLetter,
                        Tournament = tour,
                        NumberOfRounds = model.RoundsPerGroup,
                        TeamsNumber = model.TeamsNumberInGroup
                    };
                   
                    GroupsTournament groupTournament = new GroupsTournament()
                    {
                        Group = group,
                        Tournament = tour
                    };
                    tour.Groups.Add(groupTournament);
                    data.Add(group);
                    data.Add(groupTournament);

                }
                data.Add(tour);
            }
            await data.SaveChangesAsync();
        }

        public async Task<List<int>?> AddRounds(int tournamentId)
        {
            List<int> roundBNumber = new List<int>();
            var findTournament = await data.Tournaments.FirstOrDefaultAsync(x => x.Id == tournamentId);
            if (findTournament == null)
            {
                return null;
            }
            if (findTournament.GetType() == typeof(SingleGroupTournament))
            {
                var tour = (SingleGroupTournament)findTournament;
                for (int i = 0; i < tour.Rounds; i++)
                {
                    roundBNumber.Add(i + 1);
                }
            }
            
            

            return roundBNumber;
        }


        /// <summary>
        /// Method adds team into tournament
        /// </summary>
        /// <param name="tournamentiD"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public async Task AddTeamToTournament(int tournamentiD, int teamId)
        {
            var tournament = await data.SingleGroupTournaments.FirstOrDefaultAsync(t => t.Id == tournamentiD);
            if (tournament != null)
            {
                var results = await data
                    .TournamentsTeams
                    .Where(x=> x.TournamentId ==tournament.Id)
                    .ToListAsync();

                if (tournament.NumberOfTeams >results.Count)
                {
                    tournament.TeamsTournaments.Add(new TournamentsTeams
                    {
                        TeamId = teamId,
                    });
                }

            }
            var tournamentGroup = await data.GroupStageTournaments.FirstOrDefaultAsync(t => t.Id == tournamentiD);

            if (tournamentGroup != null)
            {
                var countAllTeamsIn = await data
                     .TournamentsTeams
                     .Where(x => x.TournamentId == tournamentGroup.Id)
                     .ToListAsync();
                if (countAllTeamsIn.Count < tournamentGroup.NumberOfTeams)
                {
                    tournamentGroup.TeamsTournaments.Add(new TournamentsTeams
                    {
                        TeamId = teamId,
                    });
                }
                    

            }

            await data.SaveChangesAsync();
        }

        public bool AreDatesCorrect(object tournament)
        {
            if (tournament.GetType() == typeof(AddNewSingleGroupTournamentModel))
            {

                var model = (AddNewSingleGroupTournamentModel)tournament;
                if (model.EndtDate <= model.StartDate)
                {
                    return false;
                }
            }
            else if(tournament.GetType() == typeof(AddNewGroupStageTournament))
            {
                var model = (AddNewGroupStageTournament)tournament;
                if (model.EndtDate <= model.StartDate)
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// method checks if tournament is in the database searched by name and returns it
        /// </summary>
        /// <param name="tournamentName"></param>
        /// <returns>Tournament</returns>
        public async Task<Tournament?> CheckIfTournamentIsIn(string tournamentName)
        {
            var tournament = await data.Tournaments.FirstOrDefaultAsync(t => t.Name == tournamentName);
            if (tournament == null)
            {
                return null;
            }
            return tournament;
        }

        public async Task<List<ShowMatchModel>> FindMatchesInGroup(int groupId)
        {
            var result = await data
                .Matches
                .Where(x => x.GroupId == groupId)
                .OrderBy(x=>x.MatchDate)
                .Select(m => new ShowMatchModel
                {
                    Id = m.Id,
                    GroupId = groupId,
                    TournamentId = m.TournamentId,
                    AwayTeam = m.AwayTeam.Name,
                    Result = m.HomeScore.ToString() + " : " + m.AwayScore.ToString(),
                    Date = m.MatchDate.ToString("dd-MM-yyyy HH:mm"),
                    HomeTeam = m.HomeTeam.Name,
                    GroupName = m.Group.Name,
                    Round = m.Round,
                    HomeLogo = m.HomeTeam.Logo,
                    AwayLogo = m.AwayTeam.Logo
                })
                .OrderBy(x=> x.Round)
                .ToListAsync();

            return result;
        }

        public async Task<List<ShowMatchModel>> FindMatchesInTournament(int tourneyId)
        {
            var findTour = await data.GroupStageTournaments.FirstOrDefaultAsync(x => x.Id == tourneyId);
            if (findTour != null)
            {
                var resul = await data
                .Matches
                .Where(x => x.TournamentId == findTour.Id)
                .Select(m => new ShowMatchModel
                {
                    Id = m.Id,
                    TournamentId = tourneyId,
                    GroupId = m.GroupId,
                    GroupName = m.Group.Name,
                    AwayTeam = m.AwayTeam.Name,
                    Result = m.HomeScore.ToString() + " : " + m.AwayScore.ToString(),
                    Date = m.MatchDate.ToString("dd-MM-yyyy HH:mm"),
                    HomeTeam = m.HomeTeam.Name,
                    Round = m.Round,
                    HomeLogo = m.HomeTeam.Logo,
                    AwayLogo = m.AwayTeam.Logo
                })
                .ToListAsync();

                return resul;

            }

            var result = await data
                .Matches
                .Where(x => x.TournamentId == tourneyId)
                .Select(m => new ShowMatchModel
                {
                    Id = m.Id,
                    TournamentId = tourneyId,
                    GroupId = m.GroupId,
                    GroupName = m.Group.Name,
                    AwayTeam = m.AwayTeam.Name,
                    Result = m.HomeScore.ToString() + " : " + m.AwayScore.ToString(),
                    Date = m.MatchDate.ToString("dd-MM-yyyy HH:mm"),
                    HomeTeam = m.HomeTeam.Name,
                    Round = m.Round,
                    HomeLogo = m.HomeTeam.Logo,
                    AwayLogo = m.AwayTeam.Logo
                })
                .ToListAsync();
            return result;
        }


        /// <summary>
        /// Method find and return tournament searched by ID
        /// </summary>
        /// <param name="toiurnamentId"></param>
        /// <returns></returns>
        public async Task<object?> FindTournament(int toiurnamentId)
        {
            var result = await data.Tournaments.FirstOrDefaultAsync(t => t.Id == toiurnamentId);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<int?> FindTournamentIdByGroup(int groupId)
        {
            var res = await data.GroupsTournaments.FirstOrDefaultAsync(x => x.GroupId == groupId);
            if (res == null)
            {
                return null;
            }
            return res.TournamenId;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShowTournamentModel>> GetAllTournaments()
        {
            var result = await data.Tournaments
                .Select(x => new ShowTournamentModel()
                {
                    Name = x.Name,
                    Id = x.Id,
                })
                .ToListAsync();

            for (int i = 0; i < result.Count; i++)
            {
                foreach (var team in data.GroupStageTournaments)
                {
                    if (team.Id == result[i].Id)
                    {
                        result[i].isGroupTournament = true;
                    }
                }
            }

            return result;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <returns></returns>
        public async Task<TournamentViewModel> GetDetails(int tournamentId)
        {
            var result = await data
                .Tournaments
                .Where(t => t.Id == tournamentId)
                .Select(x => new TournamentViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Teams = x.TeamsTournaments
                    .Where(t => t.TournamentId == tournamentId)
                    .Select(x => new ShowTeamModelView()
                    {
                        Name = x.Team.Name,
                        Id = x.Team.Id,
                        Stadium = x.Team.Stadium.Name,
                        Picture = x.Team.Logo
                    })
                    .ToList()
                })
                .ToListAsync();

            return result[0];

        }



        /// <summary>
        /// Method returns a name of a searched tournament by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>string</returns>
        public async Task<string> GetName(int id)
        {
            var result = await data
                .Tournaments.Where(t => t.Id == id)
                .ToArrayAsync();

            return result[0].Name;
        }

        public async Task<bool> IsItGroupStageTournament(int tourId)
        {
            var find = await data.GroupStageTournaments.FirstOrDefaultAsync(t => t.Id == tourId);
            if (find == null)
            {
                return false;
            }

            return true;
        }



        /// <summary>
        /// Method checks if team is in the tournament alrady
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <param name="teamId"></param>
        /// <returns>bool</returns>
        public async Task<bool> IsTeamInTournament(int tournamentId, int teamId)
        {
            var result = await data
                .TournamentsTeams
                .FirstOrDefaultAsync(x => x.TournamentId == tournamentId && x.TeamId == teamId);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        public async Task RemoveFromGroup(int tournamentId, int teamId)
        {
            var findGroups = await data
                .GroupsTournaments
                .Where(x => x.TournamenId == tournamentId)
                .ToListAsync();

            var findTournaments = await data
                .TournamentsTeams
                .FirstAsync(t => t.TournamentId == tournamentId && t.TeamId == teamId);

            var resultTodelete = await data.GroupsTeams
                .FirstOrDefaultAsync(g => g.Group.TournamentID == tournamentId);
            if (resultTodelete == null)
            {
                return;
            }
            data.Remove(resultTodelete);
            
            await data.SaveChangesAsync();
        }



        /// <summary>
        /// Method removes team from a tournament
        /// </summary>
        /// <param name="tournamentiD"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public async Task RemoveFromTournament(int tournamentiD, int teamId)
        {
            var removeTeam = await data.TournamentsTeams.FirstAsync(i => i.TournamentId == tournamentiD && i.TeamId == teamId);
            var teamDelete = await data.Teams.FirstAsync(t => t.Id == teamId);
            
            data.Remove(removeTeam);
            await data.SaveChangesAsync();
        }



        /// <summary>
        /// method updates already existing tournament in database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SaveChangesAsync(EditTournamentModel model)
        {
            var findTourneyToUpdate = await data
                 .Tournaments
                 .FirstAsync(t => t.Id == model.Id);

            if (findTourneyToUpdate.GetType() == typeof(SingleGroupTournament))
            {
                var tour = (SingleGroupTournament)findTourneyToUpdate;
                tour.Name = model.Name;
                tour.StartDate = model.StartDate;
                tour.EndDate = model.EndtDate;
                tour.NumberOfTeams = model.NumberOfTeams;
                tour.Rounds = (model.NumberOfTeams - 1) * 2;

            }

            if(findTourneyToUpdate.GetType() == typeof(GroupStageTournament))
            {
                var tour = (GroupStageTournament)findTourneyToUpdate;
                tour.Name = model.Name;
                tour.StartDate = model.StartDate;
                tour.EndDate = model.EndtDate;
                tour.NumberOfTeams = model.NumberOfTeams;
                for (int i = 0; i < model.Groups; i++)
                {
                    string groupLetter = Convert.ToChar(65 + i).ToString();
                    Group group = new Group
                    {
                        Name = "Group " + groupLetter,
                        Tournament = tour,
                        NumberOfRounds = model.Rounds,
                        TeamsNumber = model.NumberOfTeams
                    };

                    GroupsTournament groupTournament = new GroupsTournament()
                    {
                        Group = group,
                        Tournament = tour
                    };
                    tour.Groups.Add(groupTournament);
                    data.Add(group);
                    data.Add(groupTournament);

                }

            }

                await data.SaveChangesAsync();
        }

        public bool TotalTeamsAreCorrect(int numberOfTeams)
        {
            if (numberOfTeams < 2 || numberOfTeams > 100)
            {
                return false;
            }

            return true;
        }
    }
}
