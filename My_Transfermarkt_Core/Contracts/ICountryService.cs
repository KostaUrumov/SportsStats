using My_Transfermarkt_Core.Models.CountryModels;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Core.Contracts
{
    public interface ICountryService
    {
        Task AddCountryAsync(AddNewCountryModel model);
        Task<List<DisplayCountryModel>> AllCountriesAsync();
        Task<EditCountryModel> FindCountry(int Id);
        Task SaveChangesAsync(EditCountryModel model);
        Task<IEnumerable<Country>> GetAllCuntries();
        Task<bool> IsAlreadyCreated(AddNewCountryModel model);
        Task<bool> IsAlreadyCreated(EditCountryModel model);

    }
}
