using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
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

        public async Task AddNewTournamentAsync(AddNewTournamentModel model)
        {
            Tournament newTour = new Tournament()
            {
                Name = model.Name,
            };
            data.Tournaments.Add(newTour);

            await data.SaveChangesAsync();
        }

        public async Task AddTeamToTournament(int tournamentiD, int teamId)
        {
            var tournament =await data.Tournaments.FirstOrDefaultAsync(t=> t.Id == tournamentiD);
            tournament.TeamsTournaments.Add(new TournamentsTeams
            {
                TeamId = teamId,
            });
            
            await data.SaveChangesAsync();
        }

        public async Task<Tournament> CheckIfTournamentIsIn(string tournamentName)
        {
            var tournament = await data.Tournaments.FirstOrDefaultAsync(t=> t.Name ==  tournamentName);
            return tournament;
        }

        public async Task<Tournament> FindTournament(int toiurnamentId)
        {
            var result = await data.Tournaments.FirstOrDefaultAsync(t => t.Id == toiurnamentId);
            return result;
        }

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

        public async Task<TournamentViewModel> GetDetails(int tournamentId)
        {
            var result = await data
                .Tournaments
                .Where(t=> t.Id == tournamentId)
                .Select(x=> new TournamentViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Teams = x.TeamsTournaments
                    .Where(t=> t.TournamentId == tournamentId)
                    .Select (x=> new ShowTeamModelView()
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

        public async Task<string> GetName(int id)
        {
            var result =  await data
                .Tournaments.Where(t => t.Id == id)
                .ToArrayAsync();

            return result[0].Name;
        }

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
