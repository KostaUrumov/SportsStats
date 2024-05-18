using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        private IEnumerable<Country>? countries;
        private ApplicationDbContext? data;

        [Test]
        public void TestFindByNameCorrectly()
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

            var result = new Country()
            {
                Id = 1,
                Name = "Spain",
                ShortName = "SP"

            };

            ICountryService service = new CountryService(data);
            var country = service.FindCountryByname("Spain");
            Assert.That(country.Id, Is.EqualTo(1));
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();
            
        }

        [Test]
        public void TestFindByNameNotCorrect()
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

            var result = new Country()
            {
                Id = 1,
                Name = "Spain",
                ShortName = "SP"
            };

            ICountryService service = new CountryService(data);
            var country = service.FindCountryByname("Germany");
            Assert.That(country.Result, Is.EqualTo(null));
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();

        }

        [Test]
        public void TestFindCountryById()
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

            var result = new Country()
            {
                Id = 1,
                Name = "Spain",
                ShortName = "SP"

            };
            ICountryService service = new CountryService(data);
            var country = service.FindCountry(1);
            Assert.That(country.Result.Id, Is.EqualTo(1));
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();
        }

        [Test]
        public void TestFindCountryReturningNull()
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

            var result = new Country()
            {
                Id = 1,
                Name = "Spain",
                ShortName = "SP"

            };
            ICountryService service = new CountryService(data);
            var country = service.FindCountry(10);
            Assert.That(country.Result, Is.EqualTo(null));
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();
        }

        [Test]
        public void TestIsAlreadyCreatedTrue()
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

            AddNewCountryModel model = new AddNewCountryModel()
            {
                Id = 1,
                Name = "Spain",
                ShortName = "SP"
            };
            ICountryService service = new CountryService(data);
            var result = service.IsAlreadyCreated(model);
            Assert.That(result.Result.ToString(), Is.EqualTo("True"));
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();
        }

        [Test]
        public void TestIsAlreadyCreatedFalse()
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

            AddNewCountryModel model = new AddNewCountryModel()
            {
                Id = 56,
                Name = "Norway",
                ShortName = "NO"
            };
            ICountryService service = new CountryService(data);
            var result = service.IsAlreadyCreated(model);
            Assert.That(result.Result.ToString(), Is.EqualTo("False"));
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();
        }

        [Test]
        public void TestIsAllCountries()
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

            ICountryService service = new CountryService(data);
            var result = service.AllCountriesAsync();
            Assert.That(result.Result.Count, Is.EqualTo(2));
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();
        }

        

        [Test]
        public void TestIsFindCountryC()
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

            ICountryService service = new CountryService(data);
            var result = service.FindCountries("Spa");
            var secondResult = service.FindCountries("bulf");

            Assert.That(result.Result.Count, Is.EqualTo(1));
            Assert.That(secondResult.Result.Count, Is.EqualTo(0));
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();
        }

        [Test]
        public void TestIsSaveChanges()
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
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();
        }

        [Test]
        public void TestGetAllContries()
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

            ICountryService service = new CountryService(data);
            var result = service.GetAllCuntries();
            Assert.That(result.Result.Count(), Is.EqualTo(2));
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();
        }

    }
}