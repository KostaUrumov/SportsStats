using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.TeamModels;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Services
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext data;

        public TeamService(ApplicationDbContext _data)
        {
            data = _data;
        }

        public async Task AddLogoToTeam(byte[] pictureData, int id)
        {
            var findTeam = await data.Teams.FirstAsync(t => t.Id == id);
            findTeam.Logo = pictureData;
            await data.SaveChangesAsync();

        }

        public async Task AddNewTeamAsync(AddNewTeamModel model)
        {

            Team team = new Team();
            team.Name = model.Name;
            team.CountryId = model.CountryId;
            team.StadiumId = model.StadiumId;
            
            data.AddRange(team);
            await data.SaveChangesAsync();
        }

        public async Task AddToStadiumAsync(TeamToAddStadium model)
        {
            var findTeamToAddStadium = await data.Teams
                .FirstAsync(t => t.Id == model.Id);
            findTeamToAddStadium.StadiumId = model.StadiumId;

            await data.SaveChangesAsync();  
        }

        public async Task<TeamToAddStadium> FindTeam(int teamId)
        {
            List<TeamToAddStadium> retutnModel = await data.
                Teams
                .Where(t=> t.Id == teamId)
                .Select(x=> new TeamToAddStadium()
                {
                    Id = teamId,
                    
                })
                .ToListAsync();
            return retutnModel[0];
        }

        public async Task<AddNewTeamModel> FindTeamToBeEdited(int id)
        {
            var find = await data.Teams.FirstAsync(t => t.Id == id);
            AddNewTeamModel model;
            if (find.StadiumId == null)
            {
                model = new AddNewTeamModel()
                {
                    Name = find.Name,
                    CountryId = find.CountryId,
                    Id = find.Id

                };
            }

            else
            {
                model = new AddNewTeamModel()
                {
                    Name = find.Name,
                    CountryId = find.CountryId,
                    StadiumId = (int)find.StadiumId,
                    Id = find.Id

                };

            }
            

            return model;
        }

        public async Task<List<Team>> GetAllTeams()
        {
            return await data.Teams.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<List<ShowTeamModelView>> GetAllTeamsAvailable()
        {
            List<ShowTeamModelView> teams = await data
                .Teams
                .Select(x => new ShowTeamModelView()
                {
                    Country = x.Country.Name,
                    Name = x.Name,
                    Stadium = x.Stadium.Name,
                    Picture = x.Logo,
                    Id = x.Id
                })
                .ToListAsync();
            return teams;
            
        }

        public async Task<bool> IsAlreadyCreated(AddNewTeamModel team)
        {
            var result = await data.Teams.FirstOrDefaultAsync(t => t.Name == team.Name);
            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task SaveChangesAsync(AddNewTeamModel model)
        {
            var team = await data.Teams.FirstAsync(t => t.Id == model.Id);
            team.Name = model.Name;
            team.CountryId = model.CountryId;
            team.StadiumId = model.StadiumId;
            team.Logo = null;
            await data.SaveChangesAsync();
        }
    }
}
