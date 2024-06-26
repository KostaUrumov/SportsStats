﻿using Microsoft.EntityFrameworkCore;
using SportsStats_Core.Contracts;
using SportsStats_Core.Models.FootballerModels;
using SportsStats_Core.Models.GeneralModels;
using SportsStats_Infastructure.Data;
using SportsStats_Infastructure.DataModels;
using Footballer = SportsStats_Infastructure.DataModels.Footballer;


namespace SportsStats_Core.Services
{
    public class FootballerService : IFootballerService
    {
        private readonly ApplicationDbContext data;

        public FootballerService(ApplicationDbContext _data)
        {
            data = _data;
        }



        /// <summary>
        /// Method upload picture to footballer entity.
        /// </summary>
        /// <param name="pictureData"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task AddPictureToFootballer(byte[] pictureData, int Id)
        {
            var findFootballer = await data.Footballers
                .FirstAsync(f => f.Id == Id);
            findFootballer.Picture = pictureData;
            await data.SaveChangesAsync();
        }



        /// <summary>
        /// Method return all footballers available in database
        /// </summary>
        /// <param name="username"></param>
        /// <returns><List<AllFootballersViewModel></returns>
        public async Task<List<AllFootballersViewModel>> AllFootballers(string username)
        {
            List<AllFootballersViewModel> all = await data
                .Footballers
                .Select(f => new AllFootballersViewModel()
                {
                    HighestValueDate = DateOnly.FromDateTime(f.HishestValueDate),
                    Name = f.FirstName + " " + f.LastName,
                    Country = f.Country.Name,
                    PrefferedFoot = f.PreferedFoot.ToString(),
                    Position = f.Position.ToString(),
                    TeamsPlayed = f.TeamsPlayed,
                    HighestValue = f.HighestValue.ToString(),
                    CurrentValue = f.CurrentMarketValue.ToString(),
                    Caps = f.InternationalCaps,
                    CurrentTeam = f.Team.Name,
                    Photo = f.Picture,
                    IsRetired = f.IsRetired,
                    Birthday = f.BirthDay.ToString("MM/dd/yyyy"),
                    Id = f.Id,
                    CurrentUser = username

                })
                .OrderByDescending(x => x.Name)
                .ToListAsync();

            return all;
        }


        /// <summary>
        /// Method checks if the age for new footballer is correct. It can`t be more than 40 years old
        /// </summary> 
        /// <param name="model"></param>
        /// <returns>bool</returns>

        public bool AreDtaesCorrect(AddNewFootallerModel model)
        {

            int old = DateTime.UtcNow.Date.Year - model.BirthDay.Year;
            if (old > 40)
            {
                return false;
            }
            return true;

        }


        /// <summary>
        /// Method checks if the contract length is at least 6 months when sign a player to a club
        /// </summary>
        /// <param name="model"></param>
        /// <returns>bool</returns>
        public bool CheckDatesCorrectness(SignFootballerToATeam model)
        {
            if (model.StartContractDate.Date < DateTime.UtcNow.Date)
            {
                return false;
            }


            if (model.StartContractDate.AddMonths(6) > model.EndContractDate)
            {
                return false;
            }

            if (model.EndContractDate.Year - model.StartContractDate.Year > 5)
            {
                return false;
            }

            return true;

        }


        /// <summary>
        /// Method creates and save a new Footballer entity into database
        /// </summary>
        /// <param name="fooballer"></param>
        /// <returns></returns>
        public async Task CreateFootballerAsync(AddNewFootallerModel fooballer)
        {
            if (fooballer.CurrentMarketValue.Contains("."))
            {
                fooballer.CurrentMarketValue = fooballer.CurrentMarketValue.Replace(".", ",");
            }

            Footballer newFootballer = new Footballer()
            {
                LastName = fooballer.LastName,
                FirstName = fooballer.FirstName,
                Position = fooballer.Position,
                PreferedFoot = fooballer.PreferedFoot,
                InternationalCaps = fooballer.InternationalCaps,
                CurrentMarketValue = decimal.Parse(fooballer.CurrentMarketValue),
                BirthDay = fooballer.BirthDay,
                AgentId = fooballer.AgentId,
                CountryId = fooballer.CountryId,
                HighestValue = decimal.Parse(fooballer.CurrentMarketValue),
                HishestValueDate = DateTime.UtcNow,

            };

            data.AddRange(newFootballer);
            await data.SaveChangesAsync();
        }


        /// <summary>
        /// Method returns a footballer entity as ShowFootballerDetailsViewModel found by ID
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns>ShowFootballerDetailsViewModel</returns>

        public async Task<ShowFootballerDetailsViewModel> Details(int playerId)
        {
            var findPLayer = await data.Footballers.FirstAsync(f => f.Id == playerId);
            List<ShowFootballerDetailsViewModel> listed = await
                data
                .Footballers
                .Where(f => f.Id == playerId)
                .Select(x => new ShowFootballerDetailsViewModel()
                {
                    HighestValueDate = DateOnly.FromDateTime(x.HishestValueDate),
                    Name = x.FirstName + " " + x.LastName,
                    Country = x.Country.Name,
                    PrefferedFoot = x.PreferedFoot.ToString(),
                    Position = x.Position.ToString(),
                    TeamsPlayed = x.TeamsPlayed,
                    HighestValue = x.HighestValue.ToString(),
                    CurrentValue = x.CurrentMarketValue.ToString(),
                    Caps = x.InternationalCaps,
                    CurrentTeam = x.Team.Name,
                    Photo = x.Picture,
                    IsRetired = x.IsRetired,
                    Birthday = x.BirthDay.ToString("MM/dd/yyyy"),
                })
                .ToListAsync();


            return listed[0];
        }



        /// <summary>
        /// Method finds footballer entity by ID converts it to AddNewFootallerModel and returns it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AddNewFootallerModel</returns>

        public async Task<AddNewFootallerModel?> FindFootballer(int id)
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
                    CurrentMarketValue = m.CurrentMarketValue.ToString(),
                    Id = m.Id,
                    isRetired = m.IsRetired
                })
                .ToListAsync();
            if (result.Count() == 0)
            {
                return null;
            }
            return result[0];

        }



        /// <summary>
        /// Method checks if there are any or more footballer entities containing the given string paramenter.
        /// </summary>
        /// <param name="search"></param>
        /// <returns>List<ResultsViewModel></returns>

        public async Task<List<ResultsViewModel>> FindFootballers(string search)
        {
            List<ResultsViewModel> results = await data
                .Footballers
                .Where(x => (x.FirstName + " " + x.LastName).ToLower().Contains(search.ToLower()))
                .Select(x => new ResultsViewModel()
                {
                    Name = x.FirstName + " " + x.LastName,
                    Type = "Footballer",
                    Id = x.Id
                })
                .ToListAsync();

            return results;

        }




        /// <summary>
        /// Method return all players signed to a club.
        /// </summary>
        /// <param name="clubId"></param>
        /// <returns>List<ShowFootballerToClubModel></returns>
        public async Task<List<ShowFootballerToClubModel>> GetAllPLayersForClub(int clubId)
        {
            List<ShowFootballerToClubModel> playersToClub = await data
                .TeamsFootballers
                .Where(t => t.TeamId == clubId)
                .Select(x => new ShowFootballerToClubModel()
                {
                    BirthDay = x.Footballer.BirthDay.ToString("MM/dd/yyyy"),
                    Country = x.Footballer.Country.Name,
                    Name = x.Footballer.FirstName + " " + x.Footballer.LastName,
                    Id = x.Footballer.Id,
                    Position = x.Footballer.Position.ToString(),
                    Picture = x.Footballer.Picture,
                    StartContract = DateOnly.FromDateTime((DateTime)x.Footballer.StartDateContract),
                    EndContract = DateOnly.FromDateTime((DateTime)x.Footballer.EndDateContract),
                    Team = x.Footballer.Team.Name,
                    InternationaCaps = x.Footballer.InternationalCaps

                })
                .ToListAsync();

            return playersToClub;
        }



        /// <summary>
        /// Method return all players from the given country.
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns>List<ShowFootballerDetailsViewModel></returns>
        public async Task<List<ShowFootballerDetailsViewModel>> GetAllPLayersForCountry(string countryName)
        {
            List<ShowFootballerDetailsViewModel> playersToCountry = await data
                .Footballers
                .Where(f => f.Country.Name.ToLower() == countryName.ToLower())
                .Select(x => new ShowFootballerDetailsViewModel()
                {
                    HighestValueDate = DateOnly.FromDateTime(x.HishestValueDate),
                    Name = x.FirstName + " " + x.LastName,
                    Country = x.Country.Name,
                    PrefferedFoot = x.PreferedFoot.ToString(),
                    Position = x.Position.ToString(),
                    TeamsPlayed = x.TeamsPlayed,
                    HighestValue = x.HighestValue.ToString(),
                    CurrentValue = x.CurrentMarketValue.ToString(),
                    Caps = x.InternationalCaps,
                    CurrentTeam = x.Team.Name,
                    Photo = x.Picture,
                    IsRetired = x.IsRetired,
                    Birthday = x.BirthDay.ToString("MM/dd/yyyy"),

                })
                .ToListAsync();

            return playersToCountry;

        }



        /// <summary>
        /// Method returns all retired footballer entities in the database.
        /// </summary>
        /// <returns>List<ShowFootballerDetailsViewModel></returns>
        public async Task<List<ShowFootballerDetailsViewModel>> GetRetiredPlayers()
        {
            List<ShowFootballerDetailsViewModel> retired = await data
                 .Footballers
                 .Where(f => f.IsRetired == true)
                 .Select(x => new ShowFootballerDetailsViewModel()
                 {
                     HighestValueDate = DateOnly.FromDateTime(x.HishestValueDate),
                     Name = x.FirstName + " " + x.LastName,
                     Country = x.Country.Name,
                     PrefferedFoot = x.PreferedFoot.ToString(),
                     Position = x.Position.ToString(),
                     TeamsPlayed = x.TeamsPlayed,
                     HighestValue = x.HighestValue.ToString(),
                     CurrentValue = x.CurrentMarketValue.ToString(),
                     Caps = x.InternationalCaps,
                     CurrentTeam = x.Team.Name,
                     Photo = x.Picture,
                     IsRetired = x.IsRetired

                 })
                 .OrderByDescending(x => x.Caps)
                 .ToListAsync();

            return retired;
        }




        /// <summary>
        /// Method checks if the footballer entity with same names and birthday already exist in the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>bool</returns>
        public async Task<bool> IsAlreadyIn(AddNewFootallerModel model)
        {
            string name = model.FirstName + model.LastName;
            var find = await data.Footballers
                .FirstOrDefaultAsync(x => x.FirstName + x.LastName == name && x.BirthDay == model.BirthDay);

            if (find == null)
            {
                return false;
            }
            if (find != null)
            {
                if (find.Id == model.Id)
                {
                    return false;
                }
            }
            return true;
        }



        /// <summary>
        /// Method check if a footballer entity already has a club.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public async Task<bool> IsheSignedToAClub(int id)
        {
            var findFootballer = await data.Footballers.FirstOrDefaultAsync(x => x.Id == id);
            if (findFootballer.TeamId == null)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Method releases a footballer from a team. Footballer has no team afterwards.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task Release(int Id)
        {
            List<TeamsFootballers> result = await data
                .TeamsFootballers
                .Where(x => x.FootballerId == Id)
                .ToListAsync();

            var findFootballer = await data
                .Footballers.FirstAsync(f => f.Id == Id);
            var findTeam = await data
                .Teams
                .FirstAsync(t => t.Id == findFootballer.TeamId);
            findFootballer.TeamsPlayed.Add(findTeam);
            findFootballer.TeamId = null;

            data.RemoveRange(result);
            await data.SaveChangesAsync();
        }



        /// <summary>
        /// Method retires a footballer. He can not sign contracts any more.
        /// </summary>
        /// <param name="footballerId"></param>
        /// <returns></returns>
        public async Task RetireFromFootball(int footballerId)
        {
            var findPlayer = await data.Footballers
                .FirstAsync(f => f.Id == footballerId);
            findPlayer.IsRetired = true;
            await data.SaveChangesAsync();
        }



        /// <summary>
        /// Method updates new values for already existing footballer.
        /// </summary>
        /// <param name="footballer"></param>
        /// <returns></returns>
        public async Task SaveChangesAsync(AddNewFootallerModel footballer)
        {
            if (footballer.CurrentMarketValue.Contains('.'))
            {
                footballer.CurrentMarketValue = footballer.CurrentMarketValue.Replace(".", ",");
            }

            decimal currentValue = decimal.Parse(footballer.CurrentMarketValue);
            var findFootballer = await data.Footballers.FirstAsync(f => f.Id == footballer.Id);
            if (currentValue > findFootballer.HighestValue)
            {
                findFootballer.HighestValue = currentValue;
                findFootballer.HishestValueDate = DateTime.UtcNow;
            }
            findFootballer.LastName = footballer.LastName;
            findFootballer.FirstName = footballer.FirstName;
            findFootballer.Position = footballer.Position;
            findFootballer.PreferedFoot = footballer.PreferedFoot;
            findFootballer.CurrentMarketValue = currentValue;
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



        /// <summary>
        /// Method signs a player to a club
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SignToClub(SignFootballerToATeam model)
        {
            var updateFootballer = await data.Footballers
                 .FirstAsync(f => f.Id == model.Id);
            updateFootballer.TeamId = model.TeamId;

            updateFootballer.TeamFootballers.Add(new TeamsFootballers
            {
                TeamId = model.TeamId
            });

            var findTeam = await data.Teams
                .FirstAsync(t => t.Id == model.TeamId);
            updateFootballer.StartDateContract = model.StartContractDate;
            updateFootballer.EndDateContract = model.EndContractDate;

            await data.SaveChangesAsync();
        }
    }
}









