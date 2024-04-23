using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.FootballerModels;
using My_Transfermarkt_Core.Services;
using My_Transfermarkt_Infastructure.DataModels;
using My_Transfermarkt_Infastructure.Enums;
using static My_Transfermarkt_Infastructure.DataConstraints;
using Country = My_Transfermarkt_Infastructure.DataModels.Country;
using Footballer = My_Transfermarkt_Infastructure.DataModels.Footballer;
using Team = My_Transfermarkt_Infastructure.DataModels.Team;

namespace My_Transfermarkt_Tests
{
    [TestFixture]
    public class FootballerServiceTests
    {
        private IEnumerable<Country> countries;
        private IEnumerable<Footballer> footballers;
        private IEnumerable<Team> teams;
        private ApplicationDbContext data;

        [Test]
        public void TestFindFootballer()
        {
            this.countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            this.teams = new List<Team>()
            {
                new Team{Id =1, CountryId = 4, Name = "Manchester United" },
                new Team{Id =2, CountryId = 3, Name = "Botev Plovdiv" },
                new Team{Id =3, CountryId = 3, Name = "Levski Sofia" },
                new Team{Id =4, CountryId = 4, Name = "Arsenal" },
                new Team{Id =5, CountryId = 1, Name = "Barcelona" },
                new Team{Id =6, CountryId = 1, Name = "Real Madrid" },
                new Team{Id =7, CountryId = 2, Name = "Standard" },
                new Team{Id =8, CountryId = 2, Name = "Brugge" },
                new Team{Id =9, CountryId = 3, Name = "Spartak Varna" },
            };

            this.footballers = new List<Footballer>()
            {
                new Footballer(){Id = 1, FirstName = "Benjamin", LastName = "Sesko", BirthDay = DateTime.Parse("1993/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Midfielder, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Defender, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.countries);
            this.data.AddRange(this.teams);
            this.data.AddRange(this.footballers);
            this.data.SaveChanges();
            IFootballerService service = new FootballerService(data);
            var resultFootballer = service.FindFootballer(1);
            var resultFootballer2 = service.FindFootballer(10);
            Assert.That(resultFootballer.Result.FirstName, Is.EqualTo("Benjamin"));
            Assert.That(resultFootballer2.Result, Is.EqualTo(null));

            this.data.RemoveRange(this.countries);
            this.data.RemoveRange(this.teams);
            this.data.RemoveRange(this.footballers);
            this.data.SaveChanges();
        }

        [Test]
        public void TestFootballersForCountry()
        {
            this.countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            this.teams = new List<Team>()
            {
                new Team{Id =1, CountryId = 4, Name = "Manchester United" },
                new Team{Id =2, CountryId = 3, Name = "Botev Plovdiv" },
                new Team{Id =3, CountryId = 3, Name = "Levski Sofia" },
                new Team{Id =4, CountryId = 4, Name = "Arsenal" },
                new Team{Id =5, CountryId = 1, Name = "Barcelona" },
                new Team{Id =6, CountryId = 1, Name = "Real Madrid" },
                new Team{Id =7, CountryId = 2, Name = "Standard" },
                new Team{Id =8, CountryId = 2, Name = "Brugge" },
                new Team{Id =9, CountryId = 3, Name = "Spartak Varna" },
            };

            this.footballers = new List<Footballer>()
            {
                new Footballer(){Id = 1, FirstName = "Benjamin", LastName = "Sesko", BirthDay = DateTime.Parse("1993/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Midfielder, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Defender, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.countries);
            this.data.AddRange(this.teams);
            this.data.AddRange(this.footballers);
            this.data.SaveChanges();

            IFootballerService service = new FootballerService(data);
            var footballers = service.GetAllPLayersForCountry("Greece");
            var footballersNull = service.GetAllPLayersForCountry("Kazakhstan");
            Assert.That(footballersNull.Result.Count, Is.EqualTo(0));
            Assert.That(footballers.Result.Count, Is.EqualTo(2));

            this.data.RemoveRange(this.countries);
            this.data.RemoveRange(this.teams);
            this.data.RemoveRange(this.footballers);
            this.data.SaveChanges();
        }

        [Test]
        public void TestFootballersSignAndRelease()
        {
            this.countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            this.teams = new List<Team>()
            {
                new Team{Id =1, CountryId = 4, Name = "Manchester United" },
                new Team{Id =2, CountryId = 3, Name = "Botev Plovdiv" },
                new Team{Id =3, CountryId = 3, Name = "Levski Sofia" },
                new Team{Id =4, CountryId = 4, Name = "Arsenal" },
                new Team{Id =5, CountryId = 1, Name = "Barcelona" },
                new Team{Id =6, CountryId = 1, Name = "Real Madrid" },
                new Team{Id =7, CountryId = 2, Name = "Standard" },
                new Team{Id =8, CountryId = 2, Name = "Brugge" },
                new Team{Id =9, CountryId = 3, Name = "Spartak Varna" },
            };

            this.footballers = new List<Footballer>()
            {
                new Footballer(){Id = 1, FirstName = "Benjamin", LastName = "Sesko", BirthDay = DateTime.Parse("1993/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Midfielder, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Defender, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.countries);
            this.data.AddRange(this.teams);
            this.data.AddRange(this.footballers);
            this.data.SaveChanges();

            SignFootballerToATeam entity = new SignFootballerToATeam()
            {
                TeamId = 1,
                Id = 2,
                StartContractDate = DateTime.Parse("2022/03/10"),
                EndContractDate = DateTime.Parse("2022/09/10"),
            };
            SignFootballerToATeam entity2 = new SignFootballerToATeam()
            {
                TeamId = 1,
                Id = 3,
                StartContractDate = DateTime.Parse("2022/03/10"),
                EndContractDate = DateTime.Parse("2022/09/10"),
            };

            IFootballerService service = new FootballerService(data);
            service.SignToClub(entity);
            service.SignToClub(entity2);

            var footballers = service.GetAllPLayersForClub(1);
            var findFootballersNull = service.GetAllPLayersForClub(5);

            Assert.That(footballers.Result.Count, Is.EqualTo(2));
            Assert.That(data.TeamsFootballers.Count, Is.EqualTo(2));
            Assert.That(findFootballersNull.Result.Count, Is.EqualTo(0));

            service.Release(2);
            var footballersAfterReleasedOne = service.GetAllPLayersForClub(1);

            Assert.That(footballersAfterReleasedOne.Result.Count, Is.EqualTo(1));

            this.data.RemoveRange(this.countries);
            this.data.RemoveRange(this.teams);
            this.data.RemoveRange(this.footballers);
            this.data.SaveChanges();
        }

        [Test]
        public void TestFootballerRetire()
        {
            this.countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            this.teams = new List<Team>()
            {
                new Team{Id =1, CountryId = 4, Name = "Manchester United" },
                new Team{Id =2, CountryId = 3, Name = "Botev Plovdiv" },
                new Team{Id =3, CountryId = 3, Name = "Levski Sofia" },
                new Team{Id =4, CountryId = 4, Name = "Arsenal" },
                new Team{Id =5, CountryId = 1, Name = "Barcelona" },
                new Team{Id =6, CountryId = 1, Name = "Real Madrid" },
                new Team{Id =7, CountryId = 2, Name = "Standard" },
                new Team{Id =8, CountryId = 2, Name = "Brugge" },
                new Team{Id =9, CountryId = 3, Name = "Spartak Varna" },
            };

            this.footballers = new List<Footballer>()
            {
                new Footballer(){Id = 1, FirstName = "Benjamin", LastName = "Sesko", BirthDay = DateTime.Parse("1993/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Midfielder, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Defender, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.countries);
            this.data.AddRange(this.teams);
            this.data.AddRange(this.footballers);
            this.data.SaveChanges();

            IFootballerService service = new FootballerService(data);
            service.RetireFromFootball(1);
            service.RetireFromFootball(2);
            var retired = service.GetRetiredPlayers();
            Assert.That(retired.Result.Count, Is.EqualTo(2));

            this.data.RemoveRange(this.countries);
            this.data.RemoveRange(this.teams);
            this.data.RemoveRange(this.footballers);
            this.data.SaveChanges();
        }


        [Test]
        public void TestFootballerDetails()
        {
            this.countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            this.teams = new List<Team>()
            {
                new Team{Id =1, CountryId = 4, Name = "Manchester United" },
                new Team{Id =2, CountryId = 3, Name = "Botev Plovdiv" },
                new Team{Id =3, CountryId = 3, Name = "Levski Sofia" },
                new Team{Id =4, CountryId = 4, Name = "Arsenal" },
                new Team{Id =5, CountryId = 1, Name = "Barcelona" },
                new Team{Id =6, CountryId = 1, Name = "Real Madrid" },
                new Team{Id =7, CountryId = 2, Name = "Standard" },
                new Team{Id =8, CountryId = 2, Name = "Brugge" },
                new Team{Id =9, CountryId = 3, Name = "Spartak Varna" },
            };

            this.footballers = new List<Footballer>()
            {
                new Footballer(){Id = 1, FirstName = "Benjamin", LastName = "Sesko", BirthDay = DateTime.Parse("1993/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Midfielder, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Defender, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.countries);
            this.data.AddRange(this.teams);
            this.data.AddRange(this.footballers);
            this.data.SaveChanges();

            IFootballerService service = new FootballerService(data);
            var detailsOne = service.Details(1);
            var detailsTwo = service.Details(2);
            
            Assert.That(detailsOne.Result.Name, Is.EqualTo("Benjamin Sesko"));
            Assert.That(detailsTwo.Result.Name, Is.EqualTo("Stanislav Ivanov"));

            this.data.RemoveRange(this.countries);
            this.data.RemoveRange(this.teams);
            this.data.RemoveRange(this.footballers);
            this.data.SaveChanges();
        }


        [Test]
        public void TestDatesAreCorrect()
        {
            this.countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            this.teams = new List<Team>()
            {
                new Team{Id =1, CountryId = 4, Name = "Manchester United" },
                new Team{Id =2, CountryId = 3, Name = "Botev Plovdiv" },
                new Team{Id =3, CountryId = 3, Name = "Levski Sofia" },
                new Team{Id =4, CountryId = 4, Name = "Arsenal" },
                new Team{Id =5, CountryId = 1, Name = "Barcelona" },
                new Team{Id =6, CountryId = 1, Name = "Real Madrid" },
                new Team{Id =7, CountryId = 2, Name = "Standard" },
                new Team{Id =8, CountryId = 2, Name = "Brugge" },
                new Team{Id =9, CountryId = 3, Name = "Spartak Varna" },
            };

            this.footballers = new List<Footballer>()
            {
                new Footballer(){Id = 1, FirstName = "Benjamin", LastName = "Sesko", BirthDay = DateTime.Parse("1993/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Midfielder, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Defender, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.countries);
            this.data.AddRange(this.teams);
            this.data.AddRange(this.footballers);
            this.data.SaveChanges();

            IFootballerService service = new FootballerService(data);
            SignFootballerToATeam model = new SignFootballerToATeam()
            {
                Id = 1,
                TeamId = 2,
                StartContractDate = DateTime.Now,
                EndContractDate = DateTime.Now.AddMonths(7)
            };

            SignFootballerToATeam model2 = new SignFootballerToATeam()
            {
                Id = 1,
                TeamId = 2,
                StartContractDate = DateTime.Now,
                EndContractDate = DateTime.Now.AddMonths(4)
            };

            SignFootballerToATeam model3 = new SignFootballerToATeam()
            {
                Id = 1,
                TeamId = 2,
                StartContractDate = DateTime.Now,
                EndContractDate = DateTime.Now.AddYears(6)
            };

            var result = service.CheckDatesCorrectness(model);
            var result2 = service.CheckDatesCorrectness(model2);
            var result3 = service.CheckDatesCorrectness(model3);

            Assert.That(result.ToString, Is.EqualTo("True"));
            Assert.That(result2.ToString, Is.EqualTo("False"));
            Assert.That(result3.ToString, Is.EqualTo("False"));

            this.data.RemoveRange(this.countries);
            this.data.RemoveRange(this.teams);
            this.data.RemoveRange(this.footballers);
            this.data.SaveChanges();
        }

        [Test]
        public void TestYearsFootballer()
        {
            this.countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            this.teams = new List<Team>()
            {
                new Team{Id =1, CountryId = 4, Name = "Manchester United" },
                new Team{Id =2, CountryId = 3, Name = "Botev Plovdiv" },
                new Team{Id =3, CountryId = 3, Name = "Levski Sofia" },
                new Team{Id =4, CountryId = 4, Name = "Arsenal" },
                new Team{Id =5, CountryId = 1, Name = "Barcelona" },
                new Team{Id =6, CountryId = 1, Name = "Real Madrid" },
                new Team{Id =7, CountryId = 2, Name = "Standard" },
                new Team{Id =8, CountryId = 2, Name = "Brugge" },
                new Team{Id =9, CountryId = 3, Name = "Spartak Varna" },
            };

            this.footballers = new List<Footballer>()
            {
                new Footballer(){Id = 1, FirstName = "Benjamin", LastName = "Sesko", BirthDay = DateTime.Parse("1993/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Midfielder, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Defender, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.countries);
            this.data.AddRange(this.teams);
            this.data.AddRange(this.footballers);
            this.data.SaveChanges();

            IFootballerService service = new FootballerService(data);
            AddNewFootallerModel model = new AddNewFootallerModel()
            {
                FirstName = "Grigor",
                LastName = "Ivanov", 
                BirthDay = DateTime.Parse("1993/03/10"), 
                CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Midfielder,
                PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Left,
                CurrentMarketValue = "1000000",
                

            };

            AddNewFootallerModel model2 = new AddNewFootallerModel()
            {
                FirstName = "Grigor",
                LastName = "Ivanov",
                BirthDay = DateTime.Parse("1893/03/10"),
                CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Midfielder,
                PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Left,
                CurrentMarketValue = "1000000",


            };
            var result = service.AreDtaesCorrect(model);
            var result2 = service.AreDtaesCorrect(model2);

            Assert.That(result.ToString, Is.EqualTo("True"));
            Assert.That(result2.ToString, Is.EqualTo("False"));

            this.data.RemoveRange(this.countries);
            this.data.RemoveRange(this.teams);
            this.data.RemoveRange(this.footballers);
            this.data.SaveChanges();
        }

        [Test]
        public void TestFindFootballers()
        {
            this.countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            this.teams = new List<Team>()
            {
                new Team{Id =1, CountryId = 4, Name = "Manchester United" },
                new Team{Id =2, CountryId = 3, Name = "Botev Plovdiv" },
                new Team{Id =3, CountryId = 3, Name = "Levski Sofia" },
                new Team{Id =4, CountryId = 4, Name = "Arsenal" },
                new Team{Id =5, CountryId = 1, Name = "Barcelona" },
                new Team{Id =6, CountryId = 1, Name = "Real Madrid" },
                new Team{Id =7, CountryId = 2, Name = "Standard" },
                new Team{Id =8, CountryId = 2, Name = "Brugge" },
                new Team{Id =9, CountryId = 3, Name = "Spartak Varna" },
            };

            this.footballers = new List<Footballer>()
            {
                new Footballer(){Id = 1, FirstName = "Benjamin", LastName = "Sesko", BirthDay = DateTime.Parse("1993/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Midfielder, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Defender, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.countries);
            this.data.AddRange(this.teams);
            this.data.AddRange(this.footballers);
            this.data.SaveChanges();

            IFootballerService service = new FootballerService(data);
            var result = service.FindFootballers("kostadin");
            var result2 = service.FindFootballers("ses");
            var result3 = service.FindFootballers("nov");

            Assert.That(result.Result.Count, Is.EqualTo(0));
            Assert.That(result2.Result.Count, Is.EqualTo(1));
            Assert.That(result3.Result.Count, Is.EqualTo(3));

            this.data.RemoveRange(this.countries);
            this.data.RemoveRange(this.teams);
            this.data.RemoveRange(this.footballers);
            this.data.SaveChanges();
        }

        [Test]
        public void TestIsPlayerSignedToAClub()
        {
            this.countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            this.teams = new List<Team>()
            {
                new Team{Id =1, CountryId = 4, Name = "Manchester United" },
                new Team{Id =2, CountryId = 3, Name = "Botev Plovdiv" },
                new Team{Id =3, CountryId = 3, Name = "Levski Sofia" },
                new Team{Id =4, CountryId = 4, Name = "Arsenal" },
                new Team{Id =5, CountryId = 1, Name = "Barcelona" },
                new Team{Id =6, CountryId = 1, Name = "Real Madrid" },
                new Team{Id =7, CountryId = 2, Name = "Standard" },
                new Team{Id =8, CountryId = 2, Name = "Brugge" },
                new Team{Id =9, CountryId = 3, Name = "Spartak Varna" },
            };

            this.footballers = new List<Footballer>()
            {
                new Footballer(){Id = 1, FirstName = "Benjamin", LastName = "Sesko", BirthDay = DateTime.Parse("1993/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Midfielder, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Defender, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.countries);
            this.data.AddRange(this.teams);
            this.data.AddRange(this.footballers);
            this.data.SaveChanges();

            IFootballerService service = new FootballerService(data);
            var result = service.IsheSignedToAClub(1);
            var result2 = service.IsheSignedToAClub(2);

            Assert.That(result.Result.ToString, Is.EqualTo("False"));
            Assert.That(result2.Result.ToString, Is.EqualTo("True"));

            this.data.RemoveRange(this.countries);
            this.data.RemoveRange(this.teams);
            this.data.RemoveRange(this.footballers);
            this.data.SaveChanges();
        }

        [Test]
        public void TestIsPlayerAlreadyIn()
        {
            this.countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            this.teams = new List<Team>()
            {
                new Team{Id =1, CountryId = 4, Name = "Manchester United" },
                new Team{Id =2, CountryId = 3, Name = "Botev Plovdiv" },
                new Team{Id =3, CountryId = 3, Name = "Levski Sofia" },
                new Team{Id =4, CountryId = 4, Name = "Arsenal" },
                new Team{Id =5, CountryId = 1, Name = "Barcelona" },
                new Team{Id =6, CountryId = 1, Name = "Real Madrid" },
                new Team{Id =7, CountryId = 2, Name = "Standard" },
                new Team{Id =8, CountryId = 2, Name = "Brugge" },
                new Team{Id =9, CountryId = 3, Name = "Spartak Varna" },
            };

            this.footballers = new List<Footballer>()
            {
                new Footballer(){Id = 1, FirstName = "Benjamin", LastName = "Sesko", BirthDay = DateTime.Parse("1993/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Midfielder, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Forward, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Defender, PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.countries);
            this.data.AddRange(this.teams);
            this.data.AddRange(this.footballers);
            this.data.SaveChanges();

            IFootballerService service = new FootballerService(data);
            AddNewFootallerModel model = new AddNewFootallerModel()
            {
                FirstName = "Benjamin",
                LastName = "Sesko",
                BirthDay = DateTime.Parse("1993/03/10"),
                CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Midfielder,
                PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Left,
                CurrentMarketValue = "1000000" 
            };

            AddNewFootallerModel model2 = new AddNewFootallerModel()
            {
                FirstName = "Benjamin",
                LastName = "Sesko",
                BirthDay = DateTime.Parse("1998/03/10"),
                CountryId = 3,
                Position = My_Transfermarkt_Infastructure.Enums.Position.Midfielder,
                PreferedFoot = My_Transfermarkt_Infastructure.Enums.Foot.Left,
                CurrentMarketValue = "1000000"
            };
            var isIn = service.IsAlreadyIn(model);
            var isNotIn = service.IsAlreadyIn(model2);
            Assert.That(isIn.Result.ToString, Is.EqualTo("True"));
            Assert.That(isNotIn.Result.ToString, Is.EqualTo("False"));

            this.data.RemoveRange(this.countries);
            this.data.RemoveRange(this.teams);
            this.data.RemoveRange(this.footballers);
            this.data.SaveChanges();
        }
    }
}
