using My_Transfermarkt_Core.Models.FootballModels;

namespace My_Transfermarkt_Core.Contracts
{
    public interface IFootballerService
    {
        Task CreateFootballerAsync(AddNewFootallerModel footballer);
    }
}
