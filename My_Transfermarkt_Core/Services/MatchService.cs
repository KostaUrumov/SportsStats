﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> CheckIfMatchExists(AddNewMatchModel model, int tourneyId)
        {
            var findMatch = await data.Matches
                .FirstOrDefaultAsync(x =>
                x.HomeTeamId == model.HomeTeamId &&
                x.AwayTeamId == model.AwayTeamId &&
                x.TournamentId == tourneyId &&
                x.Round == model.Round);
            if (findMatch != null)
            {
                if (findMatch.Id == model.Id)
                {
                    return false;
                }
                return true;
            }

            return false;
                
        }

        public async Task<Match> FindMatch(int matchId)
        {
            return await data.Matches.FirstAsync(x => x.Id == matchId);
        }

        public async Task<bool> IsTeamAssignedToMatch(int teamId, int tourneyId)
        {
            var matches = await data
                .Matches
                .Where(x => x.HomeTeamId == teamId || x.AwayTeamId == teamId)
                .ToListAsync();
            foreach (var match in matches)
            {
                if (match.TournamentId == tourneyId)
                {
                    return true;
                }
            }
            

            return false;
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
