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
                RefereeId = model.RefereeId
            };
            data.AddRange(match);
            await data.SaveChangesAsync();
        }

        public bool AreTeamsDifferent(AddNewMatchModel model)
        {
            if (model.HomeTeamId == model.AwayTeamId)
            {
                return false;
            }

            return true;
        }
    }
}
