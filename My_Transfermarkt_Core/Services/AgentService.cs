using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Services
{
    public class AgentService : IAgentService
    {
        private readonly ApplicationDbContext data;

        public AgentService(ApplicationDbContext _data)
        {
            data = _data;
        }


        /// <summary>
        /// Method makes the agent agent of the footballer
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="footballerId"></param>
        /// <returns></returns>
        public async Task SignFootballerToMe(string userId, int footballerId)
        {
            var findFootballer = await data.Footballers.FirstOrDefaultAsync(x => x.Id == footballerId);
            var findUser = await data.Agents.FirstOrDefaultAsync(x => x.Id == userId);
            if (findUser != null && findFootballer != null)
            {
                findFootballer.AgentId = findUser.Id;
                findUser.AgentFootballers.Add(new AgentsFootballers
                {
                    FootballerId = footballerId,
                });

            }
            await data.SaveChangesAsync();
        }
    }
}
