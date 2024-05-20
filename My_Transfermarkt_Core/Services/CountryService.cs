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
        /// Method adds new country into database.
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
        /// Method return all country entities like  List of DisplayCountryModel
        /// </summary>
        /// <returns>List<DisplayCountryModel></returns>

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


        /// <summary>
        /// Method checks if there are any or more country entities containing the given string paramenter.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>List<ResultsViewModel></returns>
        public async Task<List<ResultsViewModel>> FindCountries(string name)
        {
            var found = await data
                .Countries
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .Select(c => new ResultsViewModel()
                {
                    Name = c.Name,
                    Id = c.Id,
                    Type = "Country"
                })
                .ToListAsync();
            return found;
        }


        /// <summary>
        /// Method checks if there is country with the given id and returns it as AddNewCountryModel model. If no matches returns null
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>AddNewCountryModel</returns>
        public async Task<AddNewCountryModel?> FindCountry(int Id)
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


        /// <summary>
        /// Method checks if there are country entities matching the parameter string. Returns the country name matching or null. Could be only one match
        /// </summary>
        /// <param name="name"></param>
        /// <returns>String</returns>
        public async Task<string?> FindCountryByname(string name)
        {
            var country = await data.Countries.FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
            if (country == null)
            {
                return null;
            }

            return country.Name;
        }


        /// <summary>
        /// Returns all countries as a List.
        /// </summary>
        /// <returns>List<Country></returns>
        public async Task<IEnumerable<Country>> GetAllCuntries()
        {
            return await data.Countries
                .OrderBy(x=> x.Name)
                .ToListAsync();
        }
        /// <summary>
        /// Method checks if country already exists in database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>bool</returns>
        public async Task<bool> IsAlreadyCreated(AddNewCountryModel model)
        {
            var found = await data.Countries.FirstOrDefaultAsync(x => x.Name == model.Name && x.ShortName == model.ShortName);
            if (found != null)
            {
                for (int i = 0; i < found.Name.Length; i++)
                {
                    if (found.Name[i] != model.Name[i])
                    {
                        return false;
                    }

                }

            }
            
            if (found == null)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Edit already existing country entity and save changes into database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
