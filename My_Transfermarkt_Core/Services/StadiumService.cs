using Microsoft.EntityFrameworkCore;
using SportsStats_Core.Contracts;
using SportsStats_Core.Models.GeneralModels;
using SportsStats_Core.Models.StadiumModels;
using SportsStats_Infastructure.Data;
using SportsStats_Infastructure.DataModels;

namespace SportsStats_Core.Services
{
    public class StadiumService : IStadiumService
    {
        private readonly ApplicationDbContext data;

        public StadiumService(ApplicationDbContext _data)
        {
            data = _data;
        }



        /// <summary>
        /// Methdos create new stadium in database
        /// </summary>
        /// <param name="stadium"></param>
        /// <returns></returns>
        public async Task CreateStadiumAsync(AddNewStadiumModel stadium)
        {
            Stadium newStadium = new Stadium()
            {
                Build = stadium.Build,
                Capacity = stadium.Capacity,
                CountryId = stadium.CountryId,
                Name = stadium.Name
            };

            data.AddRange(newStadium);
            await data.SaveChangesAsync();
        }


        /// <summary>
        /// Method returns all stadiums as List
        /// </summary>
        /// <returns>List<StadiumViewModel></returns>
        public async Task<List<StadiumViewModel>> AllAvailableStadiums()
        {

            List<StadiumViewModel> allStadiums = await data
                .Stadiums
                .Select(s => new StadiumViewModel
                {
                    Built = s.Build.ToString("dd/MM/yyyy"),
                    Capacity = s.Capacity,
                    Country = s.Country.Name,
                    Id = s.Id,
                    Name = s.Name

                })
                .OrderBy(x => x.Name)
                .ToListAsync();
            foreach (var item in allStadiums)
            {
                foreach (var team in data.Teams)
                {
                    if (item.Id == team.StadiumId)
                    {
                        item.Team = team.Name;
                    }
                }
            }

            return allStadiums;
        }



        /// <summary>
        /// Method returns all stadiums as List
        /// </summary>
        /// <returns>List<Stadium></returns>
        public async Task<List<Stadium>> GetAllStadiums()
        {
            return await data.Stadiums
                .OrderBy(x => x.Name)
                .ToListAsync();
        }




        /// <summary>
        /// Method finds and returns stadium as AddNewStadiumModel by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AddNewStadiumModel</returns>
        public async Task<AddNewStadiumModel?> FindToEdit(int id)
        {
            var result = await data.Stadiums
                .FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return null;
            }
            AddNewStadiumModel model = new AddNewStadiumModel()
            {
                Build = result.Build,
                Capacity = result.Capacity,
                CountryId = result.CountryId,
                Id = result.Id,
                Name = result.Name
            };

            return model;
        }




        /// <summary>
        /// Method update existing stadium in database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SaveChangesAsync(AddNewStadiumModel model)
        {
            var editStadium = await data.Stadiums
                .FirstAsync(s => s.Id == model.Id);
            editStadium.Name = model.Name;
            editStadium.Build = model.Build;
            editStadium.Capacity = model.Capacity;
            editStadium.CountryId = model.CountryId;

            await data.SaveChangesAsync();
        }



        /// <summary>
        /// Mtehod remove stadium from database
        /// </summary>
        /// <param name="stadiumId"></param>
        /// <returns></returns>
        public async Task RemoveStadium(int stadiumId)
        {
            var findStadium = await data.Stadiums.FirstOrDefaultAsync(s => s.Id == stadiumId);
            if (findStadium == null)
            {
                return;
            }
            foreach (var item in data.Teams)
            {
                if (item.StadiumId == stadiumId)
                {
                    var stadiumToBeRemoved = await data.Teams.FirstAsync(r => r.Id == item.Id);
                    stadiumToBeRemoved.StadiumId = null;
                }
            }

            data.RemoveRange(findStadium);

            await data.SaveChangesAsync();
        }



        /// <summary>
        /// Method checks if stadium with the same name and country already exists.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>bool</returns>

        public async Task<bool> IsStadiumAlreadyIn(AddNewStadiumModel model)
        {
            var isItIn = await data.Stadiums.FirstOrDefaultAsync(s => s.Name == model.Name && s.CountryId == model.CountryId);
            if (isItIn == null)
            {
                return false;
            }

            return true;
        }



        /// <summary>
        /// Method find a stadiums where names contain the given parameter.Possible no matches
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List<ResultsViewModel></returns>
        public async Task<List<ResultsViewModel>> FindStadiums(string name)
        {
            var result = await data
                .Stadiums
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .Select(x => new ResultsViewModel()
                {
                    Name = x.Name,
                    Id = x.Id,
                    Type = "Stadium"

                })
                .ToListAsync();

            foreach (var item in result)
            {
                foreach (var team in data.Teams)
                {
                    if (item.Id == team.StadiumId)
                    {
                        item.Team = team.Name;
                    }
                }
            }
            return result;
        }
    }
}
