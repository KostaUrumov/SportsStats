using My_Transfermarkt_Core.Models.CountryModels;

namespace My_Transfermarkt_Core.Contracts
{
    public interface ICountryService
    {
        Task AddCountryAsync(AddNewCountryModel model);
        Task<List<DisplayCountryModel>> AllCountriesAsync();
        Task<EditCountryModel> FindCountry(int Id);
        Task SaveChangesAsync(EditCountryModel model);
    }
}
