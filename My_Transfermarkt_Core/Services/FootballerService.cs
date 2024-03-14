using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.FootballModels;
using My_Transfermarkt_Infastructure.DataModels;
using static My_Transfermarkt_Infastructure.DataConstraints;
using Footballer = My_Transfermarkt_Infastructure.DataModels.Footballer;


namespace My_Transfermarkt_Core.Services
{
    public class FootballerService : IFootballerService
    {
        private readonly ApplicationDbContext data;

        public FootballerService(ApplicationDbContext _data)
        {
            data = _data;
        }

        public async Task AddPictureToFootballer(byte[] pictureData, int Id)
        {
            var findFootballer = await data.Footballers
                .FirstAsync(f => f.Id == Id);
            findFootballer.Picture = pictureData;
            await data.SaveChangesAsync();
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
                InternationalCaps = fooballer.InternationalCaps,
                CurrentMarketValue = fooballer.CurrentMarketValue,
                BirthDay = fooballer.BirthDay,
                AgentId = fooballer.AgentId,
                CountryId = fooballer.CountryId,
                HighestValue = fooballer.CurrentMarketValue,
                HishestValueDate = DateTime.UtcNow,
                
            };

            newFootballer.AgentsFootballers.Add(new AgentsFootballers()
            {
                AgentId = fooballer.AgentId
            });

            data.AddRange(newFootballer);
            await data.SaveChangesAsync();
        }

        public async Task<AddNewFootallerModel> FindFootballer(int id)
        {
            List<AddNewFootallerModel> result = await data.Footballers
                .Where(x => x.Id == id)
                .Select(m => new AddNewFootallerModel()
                {
                    AgentId = m.AgentId,
                    BirthDay = m.BirthDay,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    PreferedFoot = m.PreferedFoot,
                    CountryId = m.CountryId,
                    InternationalCaps = m.InternationalCaps,
                    CurrentMarketValue = m.CurrentMarketValue,
                    Id = m.Id
                })
                .ToListAsync();
            return result[0];
                
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
                    Position = n.Footballer.Position.ToString(),
                    Photo = n.Footballer.Picture
                })
                .OrderBy(x=>x.Name)
                .ToListAsync();

            return models;
        }

        public async Task Release(int Id)
        {
            List<TeamsFootballers> result = await data
                .TeamsFootballers
                .Where(x => x.FootballerId == Id)
                .ToListAsync();
            var findFootballer = await data
                .Footballers.FirstAsync(f => f.Id == Id);
            findFootballer.TeamId = null;

            data.RemoveRange(result);
            await data.SaveChangesAsync();
        }

        public async Task SaveChangesAsync(AddNewFootallerModel footballer)
        {
            var findFootballer = await data.Footballers.FirstAsync(f => f.Id == footballer.Id);
            if (footballer.CurrentMarketValue > findFootballer.HighestValue)
            {
                findFootballer.HighestValue = footballer.CurrentMarketValue;
                findFootballer.HishestValueDate = DateTime.UtcNow;
            }
            findFootballer.LastName = footballer.LastName;
            findFootballer.FirstName = footballer.FirstName;
            findFootballer.Position = footballer.Position;
            findFootballer.PreferedFoot = footballer.PreferedFoot;
            findFootballer.CurrentMarketValue = footballer.CurrentMarketValue;
            findFootballer.InternationalCaps = footballer.InternationalCaps;
            findFootballer.CountryId = footballer.CountryId;
            findFootballer.BirthDay = footballer.BirthDay;
            if (footballer.TeamId != null)
            {
                findFootballer.TeamId = footballer.TeamId;
                findFootballer.TeamFootballers.Add(new TeamsFootballers()
                {
                    TeamId = (int)footballer.TeamId
                });
            }

            await data.SaveChangesAsync();
        }

        public async Task SignToClub(SignFootballerToATeam model)
        {
            var updateTeam = await data.Footballers
                 .FirstAsync(f => f.Id == model.Id);
            updateTeam.TeamId = model.TeamId;
            updateTeam.TeamFootballers.Add(new TeamsFootballers
            {
                TeamId = model.TeamId
            });
            var findTeam = await data.Teams
                .FirstAsync(t => t.Id == model.TeamId);
            updateTeam.TeamsPlayed.Add(findTeam);

            await data.SaveChangesAsync();
        }
    }
}









