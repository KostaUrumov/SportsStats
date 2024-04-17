using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.GeneralModels;
using My_Transfermarkt_Core.Models.TeamModels;
using My_Transfermarkt_Infastructure.DataModels;
using System.Xml.Linq;

namespace My_Transfermarkt_Core.Services
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext data;

        public TeamService(ApplicationDbContext _data)
        {
            data = _data;
        }
       

        /// <summary>
        /// Method updates logo to team.
        /// </summary>
        /// <param name="pictureData"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task AddLogoToTeam(byte[] pictureData, int id)
        {
            var findTeam = await data.Teams.FirstAsync(t => t.Id == id);
            findTeam.Logo = pictureData;
            await data.SaveChangesAsync();

        }

        /// <summary>
        /// Method adds new team into database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddNewTeamAsync(AddNewTeamModel model)
        {

            Team team = new Team();
            team.Name = model.Name;
            team.CountryId = model.CountryId;
            team.StadiumId = model.StadiumId;
            team.Logo = model.Picture;
            
            data.AddRange(team);
            await data.SaveChangesAsync();
        }
        


        /// <summary>
        /// Method adds a team into stadium.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddToStadiumAsync(TeamToAddStadium model)
        {
            var findTeamToAddStadium = await data.Teams
                .FirstAsync(t => t.Id == model.Id);
            findTeamToAddStadium.StadiumId = model.StadiumId;

            await data.SaveChangesAsync();  
        }



        /// <summary>
        /// Method find teams into tournament by tournamentId. Might be null.
        /// </summary>
        /// <param name="tournamentId"></param>
        /// <returns>List<ShowTeamModelView></returns>
        public async Task<List<ShowTeamModelView>> CurrentTeamsInTournament(int tournamentId)
        {
            List<ShowTeamModelView> teams = await data
               .TournamentsTeams
               .Where(x=> x.TournamentId == tournamentId)
               .Select(x => new ShowTeamModelView()
               {
                   Country = x.Team.Country.Name,
                   Name = x.Team.Name,
                   Stadium = x.Team.Stadium.Name,
                   Picture = x.Team.Logo,
                   Id = x.TeamId
               })
               .OrderBy(x => x.Name)
               .ToListAsync();
            return teams;

        }




        /// <summary>
        /// Method finds team by id. If nothing dound returns null.
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns>TeamToAddStadium</returns>
        public async Task<TeamToAddStadium> FindTeam(int teamId)
        {
            List<TeamToAddStadium> retutnModel = await data.
                Teams
                .Where(t=> t.Id == teamId)
                .Select(x=> new TeamToAddStadium()
                {
                    Id = teamId,
                    TeamName = x.Name
                    
                })
                .ToListAsync();
            if (retutnModel.Count() == 0)
            {
                return null;
            }
            return retutnModel[0];
        }



        /// <summary>
        /// Method finds and returns teams by country where country name contains the string paramenter.
        /// </summary>
        /// <param name="country"></param>
        /// <returns>List<ShowTeamModelView></returns>
        public async Task<List<ShowTeamModelView>> FindTeamByCountry(string country)
        {
            var countryName = await data.Countries.FirstAsync(x => x.Name.ToLower().Contains(country.ToLower()));
            List<ShowTeamModelView> listed = await data.Teams
                .Where(x=> x.Country.Name.ToLower().Contains(country.ToLower()))
                .Select(x=> new ShowTeamModelView()
                {
                    Country = x.Country.Name,
                    Name = x.Name,
                    Stadium = x.Stadium.Name,
                    Picture = x.Logo,
                    Id = x.Id
                })
                .OrderBy(x => x.Name)
                .ToListAsync();
            
            return listed;
            
        }



        /// <summary>
        /// Method finds and returns teams where name contains the string parameter symbols.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List<ResultsViewModel></returns>
        public async Task<List<ResultsViewModel>> FindTeams(string name)
        {
            List<ResultsViewModel> searched = await data
                .Teams
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .Select(x => new ResultsViewModel()
                {
                    Name = x.Name,
                    Type = "Team",
                    Id = x.Id,
                    Team = x.Name

                })
                .ToListAsync();
            return searched;
        }




        /// <summary>
        /// Method finds the team by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AddNewTeamModel</returns>
        public async Task<AddNewTeamModel> FindTeamToBeEdited(int id)
        {
            var find = await data.Teams.FirstOrDefaultAsync(t => t.Id == id);
            if (find == null)
            {
                return null;
            }
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



        /// <summary>
        /// Method returns all teams in database
        /// </summary>
        /// <returns>List<Team></returns>
        public async Task<List<Team>> GetAllTeams()
        {
            return await data.Teams.OrderBy(x => x.Name).ToListAsync();
        }



        /// <summary>
        /// Metchod returns all available teams in ShowTeamModelView
        /// </summary>
        /// <returns>List<ShowTeamModelView></returns>
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
                .OrderBy(x => x.Name)
                .ToListAsync();
            return teams;
            
        }


        /// <summary>
        /// Method randomly returns 8 teams in modified as ShowTeamModelView
        /// </summary>
        /// <returns>List<ShowTeamModelView></returns>
        public async Task<List<ShowTeamModelView>> GetRandomListForHomePage()
        {
           List<ShowTeamModelView> result = new List<ShowTeamModelView>();
           List<ShowTeamModelView> allTeams = await data
                .Teams
                .Select (x=> new ShowTeamModelView()
                {
                    Country = x.Country.Name,
                    Name = x.Name,
                    Stadium = x.Stadium.Name,
                    Picture = x.Logo,
                    Id = x.Id
                })
                .ToListAsync ();

            Random random = new Random();

            while(true) 
            {
                if (result.Count() == 8)
                {
                    break;
                }
                int teamIndex = random.Next(0, allTeams.Count());

                if (result.Contains(allTeams[teamIndex]))
                {
                    continue;

                }
                result.Add(allTeams[teamIndex]);
            }

            return result;
         }

       

        /// <summary>
        /// Method checks if the team already exists in the database
        /// </summary>
        /// <param name="team"></param>
        /// <returns>bool</returns>
        public async Task<bool> IsAlreadyCreated(AddNewTeamModel team)
        {
            var findTeam = await data.Teams.FirstOrDefaultAsync(x => x.Name == team.Name);
            if (findTeam == null)
            {
                return false;
            }
            if (findTeam != null)
            {
                if (findTeam.Id == team.Id)
                {
                    return false;
                }
            }

            return true;

        }


        /// <summary>
        /// Method updates the new values for selected team by id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SaveChangesAsync(AddNewTeamModel model)
        {
            var team = await data.Teams.FirstAsync(t => t.Id == model.Id);
            team.Name = model.Name;
            team.CountryId = model.CountryId;
            team.StadiumId = model.StadiumId;
            team.Logo = model.Picture;
            await data.SaveChangesAsync();
        }
    }
}
