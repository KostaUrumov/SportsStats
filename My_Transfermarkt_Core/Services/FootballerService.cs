using Microsoft.EntityFrameworkCore;
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

        public bool AreDtaesCorrect(AddNewFootallerModel model)
        {

            int old = DateTime.UtcNow.Date.Year - model.BirthDay.Year;
            if (old> 40)
            {
                return false;
            }
            return true;

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

        public async Task<bool> IsAlreadyIn(AddNewFootallerModel model)
        {
            string name = model.FirstName + model.LastName;
            var find = await data.Footballers
                .FirstOrDefaultAsync(x=> x.FirstName + x.LastName == name && x.BirthDay == x.BirthDay);

            if (find == null)
            {
                return false;
            }

            return true;
        }

        public async Task<List<ShowFootballerModel>> MyFootballers(string userId)
        {
            List<ShowFootballerModel> models = await data
                .AgentsFootballers
                .Where(a => a.AgentId == userId)
                .Select(n => new ShowFootballerModel()
                {
                    BirthDay = n.Footballer.BirthDay.ToString("MM/dd/yyyy"),
                    Country = n.Footballer.Country.Name,
                    HighestValue = n.Footballer.HighestValue.ToString(),
                    Name = n.Footballer.FirstName + " " + n.Footballer.LastName,
                    InternationalCaps = n.Footballer.InternationalCaps,
                    CurrentValue = n.Footballer.CurrentMarketValue.ToString(),
                    CurrentTeam = n.Footballer.Team.Name,
                    Id = n.Footballer.Id,
                    Foot = n.Footballer.PreferedFoot.ToString(),
                    Position = n.Footballer.Position.ToString()

                })
                .OrderBy(x=>x.Name)
                .ToListAsync();

            return models;
        }
    }
}
