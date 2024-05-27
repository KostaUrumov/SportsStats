using SportsStats_Core.Models.CountryModels;
using SportsStats_Core.Models.GeneralModels;
using SportsStats_Infastructure.DataModels;

namespace SportsStats_Core.Contracts
{
    public interface ICountryService
    {
        Task AddCountryAsync(AddNewCountryModel model);
        Task<List<DisplayCountryModel>> AllCountriesAsync();
        Task<AddNewCountryModel?> FindCountry(int Id);
        Task SaveChangesAsync(AddNewCountryModel model);
        Task<IEnumerable<Country>> GetAllCuntries();
        Task<bool> IsAlreadyCreated(AddNewCountryModel model);
        Task<string> FindCountryByname(string name);
        Task<List<ResultsViewModel>> FindCountries(string name);
    }
}
