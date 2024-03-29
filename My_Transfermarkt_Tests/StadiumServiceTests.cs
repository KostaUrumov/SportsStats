using Microsoft.EntityFrameworkCore;
using Moq;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Services;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Tests
{
    [TestFixture]
    public class StadiumServiceTests
    {
        private IEnumerable<Stadium> stadiums;
        private ApplicationDbContext data;
        private IEnumerable<Country> countries;

        [SetUp]
        public void SetUp()
        {
            this.countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };

            this.stadiums = new List<Stadium>()
            {
                new Stadium { Id = 1, CountryId = 1, Build = DateTime.Parse("1999/01/01"), Capacity = 19999, Name = "New Anfield" },
                new Stadium { Id = 2, CountryId = 1, Build = DateTime.Parse("1899/01/01"), Capacity = 33999, Name = "Arena Koblenz" },
                new Stadium { Id = 3, CountryId = 1, Build = DateTime.Parse("1999/01/01"), Capacity = 75024, Name = "Alianz Arena" },
                new Stadium { Id = 4, CountryId = 2, Build = DateTime.Parse("1934/08/06"), Capacity = 74667, Name = "Olympiastadion" },
                new Stadium { Id = 5, CountryId = 2, Build = DateTime.Parse("1969/10/06"), Capacity = 24310, Name = "Bochum Arena" },
                new Stadium { Id = 6, CountryId = 2, Build = DateTime.Parse("1977/01/01"), Capacity = 33979, Name = "Arena Diesel" },
                new Stadium { Id = 7, CountryId = 2, Build = DateTime.Parse("1948/01/01"), Capacity = 51024, Name = "Solna Arena" },
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.stadiums);
            this.data.AddRange(this.countries);
            this.data.SaveChanges();
        }

        [Test]
        public void TestAllAvailableStadiums()
        {
            IStadiumService service = new StadiumService(data);
            var result = service.AllAvailableStadiums();
            Assert.That(result.Result.Count(), Is.EqualTo(7));
        }

        [Test]
        public void TestGetAllStadiums()
        {
            IStadiumService service = new StadiumService(data);
            var result = service.GetAllStadiums();
            Assert.That(result.Result.Count(), Is.EqualTo(7));
        }
    }
}
