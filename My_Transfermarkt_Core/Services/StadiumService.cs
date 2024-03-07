using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
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

            return allStadiums;
        }

        public async Task<List<Stadium>> GetAllStadiums()
        {
            return await data.Stadiums
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
    }
}
