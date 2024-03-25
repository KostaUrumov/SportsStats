using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.CountryModels;
using My_Transfermarkt_Core.Models.GeneralModels;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Services
{
    public class CountryService : ICountryService
    {
        private readonly ApplicationDbContext data;

        public CountryService(ApplicationDbContext _data)
        {
            data = _data;
        }
        /// <summary>
        /// Add new country entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddCountryAsync(AddNewCountryModel model)
        {
            Country country = new Country();
            country.Name = model.Name;
            country.ShortName = model.ShortName;

            data.AddRange(country);
            await data.SaveChangesAsync();
        }
        /// <summary>
        /// Return all countries and proceed to view
        /// </summary>
        /// <returns></returns>
        public async Task<List<DisplayCountryModel>> AllCountriesAsync()
        {
            List<DisplayCountryModel> result = await data
                .Countries
                .Select(x=> new DisplayCountryModel
                {
                    Name = x.Name,
                    ShortName = x.ShortName,
                    Id = x.Id
                })
                .OrderBy(x => x.Name)
                .ToListAsync();

            return result;
        }

        public async Task<List<ResultsViewModel>> FindCountries(string name)
        {
            var found = await data
                .Countries
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .Select(c => new ResultsViewModel()
                {
                    Name = c.Name,
                    Id = c.Id,
                    Type = "Stadium"
                })
                .ToListAsync();
            return found;
        }

        /// <summary>
        /// Find specific country
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<AddNewCountryModel> FindCountry(int Id)
        {
            var result =  await data
                .Countries
                .Where(c=> c.Id == Id)
                .Select(x=> new AddNewCountryModel
                {
                    Name = x.Name,
                    ShortName = x.ShortName,
                    Id = x.Id
                })
                .ToListAsync();
            if (result.Count == 0)
            {
                return null;
            }
            return result[0];
        }

        public async Task<string> FindCountryByname(string name)
        {
            var country = await data.Countries.FirstOrDefaultAsync(c => c.Name.ToLower().Contains(name.ToLower()));
            if (country == null)
            {
                return null;
            }

            return country.Name;
        }

        /// <summary>
        /// R%eturn all countries to fil List of countries where we need them to create new entity as Footballer or Stadium
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Country>> GetAllCuntries()
        {
            return await data.Countries
                .OrderBy(x=> x.Name)
                .ToListAsync();
        }
        /// <summary>
        /// Check if country is already in the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> IsAlreadyCreated(AddNewCountryModel model)
        {
            var found = await data.Countries.FirstOrDefaultAsync(x => x.Name == model.Name);
            if (found == null)
            {
                return false;
            }

            return true;
        }

        public async Task SaveChangesAsync(AddNewCountryModel model)
        {
            var countryToChange = await data.Countries.
                FirstAsync(c => c.Id == model.Id);
            countryToChange.ShortName = model.ShortName;
            countryToChange.Name = model.Name;

            await data.SaveChangesAsync();
        }
    }
}
