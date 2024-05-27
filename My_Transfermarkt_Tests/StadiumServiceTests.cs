using Microsoft.EntityFrameworkCore;
using SportsStats_Core.Services;
using SportsStats_Core.Contracts;
using SportsStats_Core.Models.StadiumModels;
using SportsStats_Infastructure.Data;
using Country = SportsStats_Infastructure.DataModels.Country;
using Stadium = SportsStats_Infastructure.DataModels.Stadium;

namespace SportsStats_Tests
{
    [TestFixture]
    public class StadiumServiceTests
    {
        private ICollection<Stadium>? stadiums;
        private ApplicationDbContext? data;
        private IEnumerable<Country>? countries;



        [Test]
        public void TestAllAvailableStadiums()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };

            stadiums = new List<Stadium>()
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
            data = new ApplicationDbContext(options);
            data.AddRange(stadiums);
            data.AddRange(countries);
            data.SaveChanges();


            IStadiumService service = new StadiumService(data);
            var result = service.AllAvailableStadiums();
            Assert.That(result.Result.Count(), Is.EqualTo(7));

            data.RemoveRange(stadiums);
            data.RemoveRange(countries);
            data.SaveChanges();

        }

        [Test]
        public void TestGetAllStadiums()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };

            stadiums = new List<Stadium>()
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
            data = new ApplicationDbContext(options);
            data.AddRange(stadiums);
            data.AddRange(countries);
            data.SaveChanges();


            IStadiumService service = new StadiumService(data);
            var result = service.GetAllStadiums();
            Assert.That(result.Result.Count(), Is.EqualTo(7));


            data.RemoveRange(stadiums);
            data.RemoveRange(countries);
            data.SaveChanges();
        }

        [Test]
        public void TestFindStadiumToEdit()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };

            stadiums = new List<Stadium>()
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
            data = new ApplicationDbContext(options);
            data.AddRange(stadiums);
            data.AddRange(countries);
            data.SaveChanges();



            IStadiumService service = new StadiumService(data);
            var result = service.FindToEdit(1);
            Assert.That(result.Result.Name, Is.EqualTo("New Anfield"));

            data.RemoveRange(stadiums);
            data.RemoveRange(countries);
            data.SaveChanges();
        }

        [Test]
        public void TestFindStadiumToEditWrong()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };

            stadiums = new List<Stadium>()
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
            data = new ApplicationDbContext(options);
            data.AddRange(stadiums);
            data.AddRange(countries);
            data.SaveChanges();



            IStadiumService service = new StadiumService(data);
            var result = service.FindToEdit(100);
            Assert.That(result.Result, Is.EqualTo(null));

            data.RemoveRange(stadiums);
            data.RemoveRange(countries);
            data.SaveChanges();
        }


        [Test]
        public void TestRemoveStadium()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };

            stadiums = new List<Stadium>()
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
            data = new ApplicationDbContext(options);
            data.AddRange(stadiums);
            data.AddRange(countries);
            data.SaveChanges();




            IStadiumService service = new StadiumService(data);
            service.RemoveStadium(1);
            Assert.That(data.Stadiums.Count, Is.EqualTo(6));

            Stadium one = stadiums.First(x => x.Id == 1);
            var removed = stadiums.Remove(one);

            data.RemoveRange(stadiums);
            data.RemoveRange(countries);
            data.SaveChanges();
        }

        [Test]
        public void TestRemoveStadiumUnexpectedId()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };

            stadiums = new List<Stadium>()
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
            data = new ApplicationDbContext(options);
            data.AddRange(stadiums);
            data.AddRange(countries);
            data.SaveChanges();



            IStadiumService service = new StadiumService(data);
            service.RemoveStadium(100);
            Assert.That(data.Stadiums.Count, Is.EqualTo(7));

            data.RemoveRange(stadiums);
            data.RemoveRange(countries);
            data.SaveChanges();
        }

        [Test]
        public void TestCreatenewStadium()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };

            stadiums = new List<Stadium>()
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
            data = new ApplicationDbContext(options);
            data.AddRange(stadiums);
            data.AddRange(countries);
            data.SaveChanges();



            AddNewStadiumModel model = new AddNewStadiumModel()
            {
                Id = 40,
                CountryId = 2,
                Build = DateTime.Parse("1934/08/06"),
                Capacity = 74667,
                Name = "Olympiastadionv2"
            };

            IStadiumService service = new StadiumService(data);
            service.CreateStadiumAsync(model);
            Assert.That(data.Stadiums.Count, Is.EqualTo(8));

            data.RemoveRange(stadiums);
            data.RemoveRange(countries);
            data.SaveChanges();
        }

        [Test]
        public void TestIsStadiumInTrue()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };

            stadiums = new List<Stadium>()
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
            data = new ApplicationDbContext(options);
            data.AddRange(stadiums);
            data.AddRange(countries);
            data.SaveChanges();



            AddNewStadiumModel model = new AddNewStadiumModel()
            {
                CountryId = 2,
                Build = DateTime.Parse("1934/08/06"),
                Capacity = 74667,
                Name = "Olympiastadion"
            };

            IStadiumService service = new StadiumService(data);
            var result = service.IsStadiumAlreadyIn(model);
            Assert.That(result.Result.ToString(), Is.EqualTo("True"));

            data.RemoveRange(stadiums);
            data.RemoveRange(countries);
            data.SaveChanges();
        }

        [Test]
        public void TestIsStadiumInFalse()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };

            stadiums = new List<Stadium>()
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
            data = new ApplicationDbContext(options);
            data.AddRange(stadiums);
            data.AddRange(countries);
            data.SaveChanges();



            AddNewStadiumModel model = new AddNewStadiumModel()
            {
                CountryId = 2,
                Build = DateTime.Parse("1934/08/06"),
                Capacity = 74667,
                Name = "Olympiastadionv2"
            };

            IStadiumService service = new StadiumService(data);
            var result = service.IsStadiumAlreadyIn(model);
            Assert.That(result.Result.ToString(), Is.EqualTo("False"));

            data.RemoveRange(stadiums);
            data.RemoveRange(countries);
            data.SaveChanges();
        }

        [Test]
        public void TestIsFindStasiums()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"}
            };

            stadiums = new List<Stadium>()
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
            data = new ApplicationDbContext(options);
            data.AddRange(stadiums);
            data.AddRange(countries);
            data.SaveChanges();



            IStadiumService service = new StadiumService(data);
            var result = service.FindStadiums("Olym");
            var result2 = service.FindStadiums("re");
            Assert.That(result.Result.Count(), Is.EqualTo(1));
            Assert.That(result2.Result.Count(), Is.EqualTo(5));

            data.RemoveRange(stadiums);
            data.RemoveRange(countries);
            data.SaveChanges();
        }
    }
}
