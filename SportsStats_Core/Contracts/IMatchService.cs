﻿using SportsStats_Core.Models.MatchModels;
using SportsStats_Infastructure.DataModels;

namespace SportsStats_Core.Contracts
{
    public interface IMatchService
    {
        bool AreTeamsDifferent(AddNewMatchModel model);
        Task AddNewMatch(AddNewMatchModel model);
        Task<Match> FindMatch(int matchId);
        Task SaveChanges(AddNewMatchModel model);
        Task<bool> IsTeamAssignedToMatch(int teamId, int tourneyId);
        Task<bool> IsTeamAssignedToMatchInGroup(int teamdId, int groupId);

        Task<bool> CheckIfMatchExists(AddNewMatchModel model, int tourneyId);

    }
}
