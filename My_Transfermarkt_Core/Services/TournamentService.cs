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
                data.AddRange(tour);
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
                        Tournament = tour
                    };
                    
                    tour.Groups.Add(group);
                   
                }
                data.Groups.AddRange(tour.Groups);
                data.AddRange(tour);
            }
            await data.SaveChangesAsync();
        }


        /// <summary>
        /// Method adds team into tournament
        /// </summary>
        /// <param name="tournamentiD"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public async Task AddTeamToTournament(int tournamentiD, int teamId)
        {
            var tournament = await data.Tournaments.FirstOrDefaultAsync(t => t.Id == tournamentiD);
            tournament.TeamsTournaments.Add(new TournamentsTeams
            {
                TeamId = teamId,
            });

            await data.SaveChangesAsync();
        }


        /// <summary>
        /// method checks if tournament is in the database searched by name and returns it
        /// </summary>
        /// <param name="tournamentName"></param>
        /// <returns>Tournament</returns>
        public async Task<Tournament> CheckIfTournamentIsIn(string tournamentName)
        {
            var tournament = await data.Tournaments.FirstOrDefaultAsync(t => t.Name == tournamentName);
            return tournament;
        }

        public async Task<List<ShowMatchModel>> FindMatchesInTournament(int tourneyId)
        {
            var result = await data
                .Matches
                .Where(x => x.TournamentId == tourneyId)
                .Select(m => new ShowMatchModel
                {
                    AwayTeam = m.AwayTeam.Name,
                    Result = m.HomeScore.ToString() + " : " + m.AwayScore.ToString(),
                    Date = m.MatchDate.ToString("dd-MM-yyyy HH:mm"),
                    HomeTeam = m.HomeTeam.Name,
                })
                .ToListAsync();
            return result;
        }


        /// <summary>
        /// Method find and return tournament searched by ID
        /// </summary>
        /// <param name="toiurnamentId"></param>
        /// <returns></returns>
        public async Task<Tournament> FindTournament(int toiurnamentId)
        {
            var result = await data.Tournaments.FirstOrDefaultAsync(t => t.Id == toiurnamentId);
            return result;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShowTournamentModel>> GetAllTournaments()
        {
            return await data.Tournaments
                .Select(x => new ShowTournamentModel()
                {
                    Name = x.Name,
                    Id = x.Id,
                })
                .ToListAsync();
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



        /// <summary>
        /// Method removes team from a tournament
        /// </summary>
        /// <param name="tournamentiD"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public async Task RemoveFromTournament(int tournamentiD, int teamId)
        {
            var removeTeam = await data.TournamentsTeams.FirstAsync(i => i.TournamentId == tournamentiD && i.TeamId == teamId);
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

            findTourneyToUpdate.Name = model.Name;
            await data.SaveChangesAsync();
        }
    }
}
