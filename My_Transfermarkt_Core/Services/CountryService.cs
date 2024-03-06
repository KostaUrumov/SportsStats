using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.CountryModels;
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

        public async Task AddCountryAsync(AddNewCountryModel model)
        {
            Country country = new Country();
            country.Name = model.Name;
            country.ShortName = model.ShortName;

            data.AddRange(country);
            await data.SaveChangesAsync();
        }

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

        public async Task<EditCountryModel> FindCountry(int Id)
        {
            var result =  await data
                .Countries
                .Where(c=> c.Id == Id)
                .Select(x=> new EditCountryModel
                {
                    Name = x.Name,
                    ShortName = x.ShortName,
                    Id = x.Id
                })
                .ToListAsync();
            return result[0];
        }

        public async Task SaveChangesAsync(EditCountryModel model)
        {
            var countryToChange = await data.Countries.
                FirstAsync(c => c.Id == model.Id);
            countryToChange.ShortName = model.ShortName;
            countryToChange.Name = model.Name;

            await data.SaveChangesAsync();
        }
    }
}
