using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.GeneralModels;
using My_Transfermarkt_Core.Models.StadiumModels;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Services
{
    public class StadiumService : IStadiumService
    {
        private readonly ApplicationDbContext data;

        public StadiumService(ApplicationDbContext _data)
        {
            data = _data;
        }
        /// <summary>
        /// Cteate new stadium entity
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
        /// Return all stadiums in database to proceed to a view
        /// </summary>
        /// <returns></returns>
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
        /// Return all stadiums to list to be selected when adding a new team
        /// </summary>
        /// <returns></returns>
        public async Task<List<Stadium>> GetAllStadiums()
        {
            return await data.Stadiums
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
        /// <summary>
        /// Find stadium to be edited
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AddNewStadiumModel> FindToEdit(int id)
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
        /// Save changes to a stadium
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
        /// Remove stadium from database
        /// </summary>
        /// <param name="stadiumId"></param>
        /// <returns></returns>
        public async Task RemoveStadium(int stadiumId)
        {
            var findStadium = await data.Stadiums.FirstAsync(s => s.Id == stadiumId);
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
        /// Check if stadium already exists
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> IsStadiumAlreadyIn(AddNewStadiumModel model)
        {
            var isItIn = await data.Stadiums.FirstOrDefaultAsync(s => s.Name == model.Name && s.CountryId == model.CountryId);
            if (isItIn == null)
            {
                return false;
            }

            return true;
        }

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
