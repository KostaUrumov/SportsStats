using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.FootballModels;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Services
{
    public class FootballerService : IFootballerService
    {
        private readonly ApplicationDbContext data;

        public FootballerService(ApplicationDbContext _data)
        {
            data = _data;
        }

        public async Task CreateFootballerAsync(AddNewFootallerModel fooballer)
        {

            Footballer newFootballer = new Footballer()
            {
                LastName = fooballer.LastName,
                FirstName = fooballer.FirstName,
                Position = fooballer.Position,
                PreferedFoot = fooballer.PreferedFoot,
                StartDateContract = fooballer.StartDateContract,
                EndDateContract = fooballer.EndDateContract,
                InternationalCaps = fooballer.InternationalCaps,
                CurrentMarketValue = fooballer.CurrentMarketValue,
                BirthDay = fooballer.BirthDay,
                AgentId = fooballer.AgentId,
                CountryId = fooballer.CountryId,
                HighestValue = fooballer.CurrentMarketValue,
                HishestValueDate = DateTime.UtcNow,
                

            };

            if (fooballer.TeamId != null)
            {
                newFootballer.TeamId = fooballer.TeamId;
                newFootballer.TeamFootballers.Add(new TeamsFootballers()
                {
                    TeamId = (int)fooballer.TeamId
                });

            }

            if(fooballer.StartDateContract != null)
            {
                newFootballer.StartDateContract = fooballer.StartDateContract;
                newFootballer.EndDateContract = fooballer.EndDateContract;
            }

            newFootballer.AgentsFootballers.Add(new AgentsFootballers()
            {
                AgentId = fooballer.AgentId
            });

            data.AddRange(newFootballer);
            await data.SaveChangesAsync();
        }
    }
}
