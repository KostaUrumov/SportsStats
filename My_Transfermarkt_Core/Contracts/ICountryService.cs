using My_Transfermarkt_Core.Models.CountryModels;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Contracts
{
    public interface ICountryService
    {
        Task AddCountryAsync(AddNewCountryModel model);
        Task<List<DisplayCountryModel>> AllCountriesAsync();
        Task<AddNewCountryModel> FindCountry(int Id);
        Task SaveChangesAsync(AddNewCountryModel model);
        Task<IEnumerable<Country>> GetAllCuntries();
        Task<bool> IsAlreadyCreated(AddNewCountryModel model);
        Task <string> FindCountryByname(string name);
    }
}
