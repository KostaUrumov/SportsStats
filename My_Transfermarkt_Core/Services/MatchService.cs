using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.MatchModels;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Services
{
    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext data;

        public MatchService(ApplicationDbContext _data)
        {
            data = _data;
        }

        public async Task AddNewMatch(AddNewMatchModel model)
        {


            Match match = new Match()
            {
                HomeTeamId = model.HomeTeamId,
                HomeScore = model.HomeScore,
                AwayTeamId = model.AwayTeamId,
                AwayScore = model.AwayScore,
                MatchDate = model.Date,
                TournamentId = model.TournamentId,
                RefereeId = model.RefereeId,
                Round = model.Round,
            };
            if (model.GroupId != null)
            {
                match.GroupId = model.GroupId;
            }
            data.AddRange(match);
            await data.SaveChangesAsync();
        }

        public bool AreTeamsDifferent(AddNewMatchModel model)
        {

            var mod = (AddNewMatchModel)model;
            if (mod.HomeTeamId == mod.AwayTeamId)
            {
                return false;
            }

            return true;
        }

        public async Task<Match> FindMatch(int matchId)
        {
            return await data.Matches.FirstAsync(x => x.Id == matchId);
        }

        public async Task SaveChanges(AddNewMatchModel model)
        {
            var findmatch = await data.Matches.FirstAsync(x => x.Id == model.Id);
            findmatch.RefereeId = model.RefereeId;
            findmatch.Round = model.Round;
            findmatch.HomeScore = model.HomeScore;
            findmatch.AwayScore = model.AwayScore;
            findmatch.HomeTeamId = model.HomeTeamId;
            findmatch.AwayTeamId = model.AwayTeamId;

            await data.SaveChangesAsync();
        }
    }
}
