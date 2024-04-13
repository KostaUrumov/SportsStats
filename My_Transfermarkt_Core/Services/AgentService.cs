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

        public async Task SignFootballerToMe(string userId, int footballerId)
        {
            var findFootballer = await data.Footballers.FirstOrDefaultAsync(x => x.Id == footballerId);
            var findUser = await data.Agents.FirstOrDefaultAsync(x=> x.Id == userId);
            findFootballer.AgentId = findUser.Id;
            findUser.AgentFootballers.Add(new AgentsFootballers
            {
                FootballerId = footballerId,
            });
           
            await data.SaveChangesAsync();
        }
    }
}
