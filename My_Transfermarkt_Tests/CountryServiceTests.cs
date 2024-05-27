using Microsoft.EntityFrameworkCore;
using SportsStats_Core.Contracts;
using SportsStats_Core.Models.CountryModels;
using SportsStats_Core.Services;
using SportsStats_Infastructure.Data;
using Country = SportsStats_Infastructure.DataModels.Country;

namespace SportsStats_Tests
{
    [TestFixture]
    public class CountryServiceTests
    {
        private IEnumerable<Country>? countries;
        private ApplicationDbContext? data;

        [Test]
        public void TestFindByNameCorrectly()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.SaveChanges();

            var result = new Country()
            {
                Id = 1,
                Name = "Spain",
                ShortName = "SP"

            };

            ICountryService service = new CountryService(data);
            var country = service.FindCountryByname("Spain");
            Assert.That(country.Id, Is.EqualTo(1));
            data.RemoveRange(countries);
            data.SaveChanges();

        }

        [Test]
        public void TestFindByNameNotCorrect()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.SaveChanges();

            var result = new Country()
            {
                Id = 1,
                Name = "Spain",
                ShortName = "SP"
            };

            ICountryService service = new CountryService(data);
            var country = service.FindCountryByname("Germany");
            Assert.That(country.Result, Is.EqualTo(null));
            data.RemoveRange(countries);
            data.SaveChanges();

        }

        [Test]
        public void TestFindCountryById()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.SaveChanges();

            var result = new Country()
            {
                Id = 1,
                Name = "Spain",
                ShortName = "SP"

            };
            ICountryService service = new CountryService(data);
            var country = service.FindCountry(1);
            Assert.That(country.Result.Id, Is.EqualTo(1));
            data.RemoveRange(countries);
            data.SaveChanges();
        }

        [Test]
        public void TestFindCountryReturningNull()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.SaveChanges();

            var result = new Country()
            {
                Id = 1,
                Name = "Spain",
                ShortName = "SP"

            };
            ICountryService service = new CountryService(data);
            var country = service.FindCountry(10);
            Assert.That(country.Result, Is.EqualTo(null));
            data.RemoveRange(countries);
            data.SaveChanges();
        }

        [Test]
        public void TestIsAlreadyCreatedTrue()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.SaveChanges();

            AddNewCountryModel model = new AddNewCountryModel()
            {
                Id = 1,
                Name = "Spain",
                ShortName = "SP"
            };
            ICountryService service = new CountryService(data);
            var result = service.IsAlreadyCreated(model);
            Assert.That(result.Result.ToString(), Is.EqualTo("True"));
            data.RemoveRange(countries);
            data.SaveChanges();
        }

        [Test]
        public void TestIsAlreadyCreatedFalse()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.SaveChanges();

            AddNewCountryModel model = new AddNewCountryModel()
            {
                Id = 56,
                Name = "Norway",
                ShortName = "NO"
            };
            ICountryService service = new CountryService(data);
            var result = service.IsAlreadyCreated(model);
            Assert.That(result.Result.ToString(), Is.EqualTo("False"));
            data.RemoveRange(countries);
            data.SaveChanges();
        }

        [Test]
        public void TestIsAllCountries()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.SaveChanges();

            ICountryService service = new CountryService(data);
            var result = service.AllCountriesAsync();
            Assert.That(result.Result.Count, Is.EqualTo(2));
            data.RemoveRange(countries);
            data.SaveChanges();
        }



        [Test]
        public void TestIsFindCountryC()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.SaveChanges();

            ICountryService service = new CountryService(data);
            var result = service.FindCountries("Spa");
            var secondResult = service.FindCountries("bulf");

            Assert.That(result.Result.Count, Is.EqualTo(1));
            Assert.That(secondResult.Result.Count, Is.EqualTo(0));
            data.RemoveRange(countries);
            data.SaveChanges();
        }

        [Test]
        public void TestIsSaveChanges()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.SaveChanges();

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
            data.RemoveRange(countries);
            data.SaveChanges();
        }

        [Test]
        public void TestGetAllContries()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.SaveChanges();

            ICountryService service = new CountryService(data);
            var result = service.GetAllCuntries();
            Assert.That(result.Result.Count(), Is.EqualTo(2));
            data.RemoveRange(countries);
            data.SaveChanges();
        }

    }
}