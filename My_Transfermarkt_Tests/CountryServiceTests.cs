using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.CountryModels;
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
                .UseInMemoryDatabase(databaseName: "MyMockedData")
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
        public void TestFindByNameNotCorrect()
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

        [Test]
        public void TestFindCountryById()
        {
            var result = new Country()
            {
                Id = 1,
                Name = "Spain",
                ShortName = "SP"

            };
            ICountryService service = new CountryService(data);
            var country = service.FindCountry(1);
            Assert.That(country.Id, Is.EqualTo(1));
        }

        [Test]
        public void TestFindCountryReturningNull()
        {
            var result = new Country()
            {
                Id = 1,
                Name = "Spain",
                ShortName = "SP"

            };
            ICountryService service = new CountryService(data);
            var country = service.FindCountry(10);
            Assert.That(country.Result, Is.EqualTo(null));
        }

        [Test]
        public void TestIsAlreadyCreatedTrue()
        {
            AddNewCountryModel model = new AddNewCountryModel()
            {
                Id = 1,
                Name = "Spain",
                ShortName = "SP"
            };
            ICountryService service = new CountryService(data);
            var result = service.IsAlreadyCreated(model);
            Assert.That(result.Result.ToString(), Is.EqualTo("True"));
        }

        [Test]
        public void TestIsAlreadyCreatedFalse()
        {
            AddNewCountryModel model = new AddNewCountryModel()
            {
                Id = 56,
                Name = "Norway",
                ShortName = "NO"
            };
            ICountryService service = new CountryService(data);
            var result = service.IsAlreadyCreated(model);
            Assert.That(result.Result.ToString(), Is.EqualTo("False"));
        }

        [Test]
        public void TestIsAllCountries()
        {
            ICountryService service = new CountryService(data);
            var result = service.AllCountriesAsync();
            Assert.That(result.Result.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestIsAddCountry()
        {
            AddNewCountryModel model = new AddNewCountryModel()
            {
                Id = 56,
                Name = "Norway",
                ShortName = "NO"
            };
            ICountryService service = new CountryService(data);
            service.AddCountryAsync(model);
            Assert.That(data.Countries.Count(), Is.EqualTo(3));
        }

        [Test]
        public void TestIsFindCountryC()
        {

            ICountryService service = new CountryService(data);
            var result = service.FindCountries("Spa");
            var secondResult = service.FindCountries("bulf");

            Assert.That(result.Result.Count, Is.EqualTo(1));
            Assert.That(secondResult.Result.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestIsSaveChanges()
        {
            AddNewCountryModel model = new AddNewCountryModel()
            {
                Id = 1,
                Name = "Norway",
                ShortName = "NO"
            };

            ICountryService service = new CountryService(data);
            service.SaveChangesAsync(model);
            var result = service.FindCountry(1);
            Assert.That(result.Result.Name, Is.EqualTo("Norway"));
        }

        [Test]
        public void TestGetAllContries()
        {
            ICountryService service = new CountryService(data);
            var result = service.GetAllCuntries();
            Assert.That(result.Result.Count(), Is.EqualTo(2));
        }

    }
}