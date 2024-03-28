using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Services;
using Country = My_Transfermarkt_Infastructure.DataModels.Country;

namespace My_Transfermarkt_Tests
{
    [TestFixture]
    public class CountryServiceTests
    {
        private IEnumerable<Country> countries;
        private ApplicationDbContext data;

        [SetUp]
        public void SetUp()
        {
            this.countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .   UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.countries);
            this.data.SaveChanges();
        }

       
        [Test]
        public void TestFindByNameCorrectly()
        {
            var result = new Country()
            {
                Id = 1,
                Name = "Spain",
                ShortName = "SP"

            };

            ICountryService service = new CountryService(data);
            var country = service.FindCountryByname("Spain");
            Assert.That(country.Id, Is.EqualTo(1));
        }

        [Test]
        public void TestFindByNameNotCorrectly()
        {
            var result = new Country()
            {
                Id = 1,
                Name = "Spain",
                ShortName = "SP"

            };

            ICountryService service = new CountryService(data);
            var country = service.FindCountryByname("Germany");

            Assert.That(country.Result, Is.EqualTo(null));
        }
    }
}