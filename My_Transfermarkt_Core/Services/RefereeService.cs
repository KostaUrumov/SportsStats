using Microsoft.EntityFrameworkCore;
using SportsStats_Core.Contracts;
using SportsStats_Core.Models.RefereeModels;
using SportsStats_Infastructure.Data;
using SportsStats_Infastructure.DataModels;

namespace SportsStats_Core.Services
{
    public class RefereeService : IRefereeService
    {
        private readonly ApplicationDbContext data;

        public RefereeService(ApplicationDbContext _data)
        {
            data = _data;
        }

        public async Task AddRefereeAsync(AddNewRefereeModel model)
        {
            Referee referee = new Referee()
            {
                CountryID = model.CountryID,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            data.AddRange(referee);
            await data.SaveChangesAsync();
        }

        public async Task<List<Referee>> AllReferees()
        {
            return await data.Referees.ToListAsync();
        }

        public async Task<bool> ChekIfRefereeExist(AddNewRefereeModel model)
        {
            var result = await data.Referees.FirstOrDefaultAsync(
                x => x.FirstName == model.FirstName
                && x.LastName == model.LastName);
            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task<AddNewRefereeModel?> FindReferee(int refereeId)
        {
            var referee = await data
                .Referees
                .Where(x => x.Id == refereeId)
                .Select(r => new AddNewRefereeModel
                {
                    FirstName = r.FirstName,
                    Id = r.Id,
                    LastName = r.LastName,
                    CountryID = r.CountryID,
                })
                .ToListAsync();
            if (referee.Count == 0)
            {
                return null;
            }
            return referee[0];
        }

        public async Task<List<ShowRefereeModel>> GetAllReferees()
        {
            var result = await data
                .Referees
                .Select(x => new ShowRefereeModel
                {
                    Id = x.Id,
                    Name = x.FirstName + " " + x.LastName,
                    Rating = x.Rating.ToString(),
                    Country = x.Country.Name
                })
                .ToListAsync();

            return result;
        }

        public async Task SaveChangesAsync(AddNewRefereeModel model)
        {
            var refereeToEdit = await data.Referees.FirstAsync(x => x.Id == model.Id);
            refereeToEdit.FirstName = model.FirstName;
            refereeToEdit.LastName = model.LastName;
            refereeToEdit.CountryID = model.CountryID;
            await data.SaveChangesAsync();
        }
    }
}
