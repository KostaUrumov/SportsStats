using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.StadiumModels;
using My_Transfermarkt_Core.Services;
using Country = My_Transfermarkt_Infastructure.DataModels.Country;
using Stadium = My_Transfermarkt_Infastructure.DataModels.Stadium;

namespace My_Transfermarkt_Tests
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


            IStadiumService service = new StadiumService(data);
            var result = service.AllAvailableStadiums();
            Assert.That(result.Result.Count(), Is.EqualTo(7));

            this.data.RemoveRange(this.stadiums);
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();

        }

        [Test]
        public void TestGetAllStadiums()
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


            IStadiumService service = new StadiumService(data);
            var result = service.GetAllStadiums();
            Assert.That(result.Result.Count(), Is.EqualTo(7));


            this.data.RemoveRange(this.stadiums);
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();
        }

        [Test]
        public void TestFindStadiumToEdit()
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



            IStadiumService service = new StadiumService(data);
            var result = service.FindToEdit(1);
            Assert.That(result.Result.Name, Is.EqualTo("New Anfield"));

            this.data.RemoveRange(this.stadiums);
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();
        }

        [Test]
        public void TestFindStadiumToEditWrong()
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



            IStadiumService service = new StadiumService(data);
            var result = service.FindToEdit(100);
            Assert.That(result.Result, Is.EqualTo(null));

            this.data.RemoveRange(this.stadiums);
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();
        }


        [Test]
        public void TestRemoveStadium()
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




            IStadiumService service = new StadiumService(data);
            service.RemoveStadium(1);
            Assert.That(data.Stadiums.Count, Is.EqualTo(6));

            Stadium one = this.stadiums.First(x => x.Id == 1);
            var removed = this.stadiums.Remove(one);

            this.data.RemoveRange(this.stadiums);
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();
        }

        [Test]
        public void TestRemoveStadiumUnexpectedId()
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



            IStadiumService service = new StadiumService(data);
            service.RemoveStadium(100);
            Assert.That(data.Stadiums.Count, Is.EqualTo(7));

            this.data.RemoveRange(this.stadiums);
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();
        }

        [Test]
        public void TestCreatenewStadium()
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

            this.data.RemoveRange(this.stadiums);
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();
        }

        [Test]
        public void TestIsStadiumInTrue()
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

            this.data.RemoveRange(this.stadiums);
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();
        }

        [Test]
        public void TestIsStadiumInFalse()
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

            this.data.RemoveRange(this.stadiums);
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();
        }

        [Test]
        public void TestIsFindStasiums()
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



            IStadiumService service = new StadiumService(data);
            var result = service.FindStadiums("Olym");
            var result2 = service.FindStadiums("re");
            Assert.That(result.Result.Count(), Is.EqualTo(1));
            Assert.That(result2.Result.Count(), Is.EqualTo(5));

            this.data.RemoveRange(this.stadiums);
            this.data.RemoveRange(this.countries);
            this.data.SaveChanges();
        }
    }
}
