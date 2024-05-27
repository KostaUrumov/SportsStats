using Microsoft.EntityFrameworkCore;
using SportsStats_Core.Contracts;
using SportsStats_Core.Models.FootballerModels;
using SportsStats_Core.Services;
using SportsStats_Infastructure.Data;
using SportsStats_Infastructure.Enums;
using Country = SportsStats_Infastructure.DataModels.Country;
using Footballer = SportsStats_Infastructure.DataModels.Footballer;
using Team = SportsStats_Infastructure.DataModels.Team;

namespace SportsStats_Tests
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
            countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            teams = new List<Team>()
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

            footballers = new List<Footballer>()
            {
                new Footballer(){Id = 1, FirstName = "Benjamin", LastName = "Sesko", BirthDay = DateTime.Parse("1993/03/10"), CountryId = 3,
                Position = Position.Midfielder, PreferedFoot = Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Defender, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.AddRange(teams);
            data.AddRange(footballers);
            data.SaveChanges();
            IFootballerService service = new FootballerService(data);
            var resultFootballer = service.FindFootballer(1);
            var resultFootballer2 = service.FindFootballer(10);
            Assert.That(resultFootballer.Result.FirstName, Is.EqualTo("Benjamin"));
            Assert.That(resultFootballer2.Result, Is.EqualTo(null));

            data.RemoveRange(countries);
            data.RemoveRange(teams);
            data.RemoveRange(footballers);
            data.SaveChanges();
        }

        [Test]
        public void TestFootballersForCountry()
        {
            countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            teams = new List<Team>()
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
                Position = Position.Midfielder, PreferedFoot = Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Defender, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.AddRange(teams);
            data.AddRange(this.footballers);
            data.SaveChanges();

            IFootballerService service = new FootballerService(data);
            var footballers = service.GetAllPLayersForCountry("Greece");
            var footballersNull = service.GetAllPLayersForCountry("Kazakhstan");
            Assert.That(footballersNull.Result.Count, Is.EqualTo(0));
            Assert.That(footballers.Result.Count, Is.EqualTo(2));

            data.RemoveRange(countries);
            data.RemoveRange(teams);
            data.RemoveRange(this.footballers);
            data.SaveChanges();
        }

        [Test]
        public void TestFootballersSignAndRelease()
        {
            countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            teams = new List<Team>()
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
                Position = Position.Midfielder, PreferedFoot = Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Defender, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.AddRange(teams);
            data.AddRange(this.footballers);
            data.SaveChanges();

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

            data.RemoveRange(countries);
            data.RemoveRange(teams);
            data.RemoveRange(this.footballers);
            data.SaveChanges();
        }

        [Test]
        public void TestFootballerRetire()
        {
            countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            teams = new List<Team>()
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

            footballers = new List<Footballer>()
            {
                new Footballer(){Id = 1, FirstName = "Benjamin", LastName = "Sesko", BirthDay = DateTime.Parse("1993/03/10"), CountryId = 3,
                Position = Position.Midfielder, PreferedFoot = Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Defender, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.AddRange(teams);
            data.AddRange(footballers);
            data.SaveChanges();

            IFootballerService service = new FootballerService(data);
            service.RetireFromFootball(1);
            service.RetireFromFootball(2);
            var retired = service.GetRetiredPlayers();
            Assert.That(retired.Result.Count, Is.EqualTo(2));

            data.RemoveRange(countries);
            data.RemoveRange(teams);
            data.RemoveRange(footballers);
            data.SaveChanges();
        }


        [Test]
        public void TestFootballerDetails()
        {
            countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            teams = new List<Team>()
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

            footballers = new List<Footballer>()
            {
                new Footballer(){Id = 1, FirstName = "Benjamin", LastName = "Sesko", BirthDay = DateTime.Parse("1993/03/10"), CountryId = 3,
                Position = Position.Midfielder, PreferedFoot = Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Defender, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.AddRange(teams);
            data.AddRange(footballers);
            data.SaveChanges();

            IFootballerService service = new FootballerService(data);
            var detailsOne = service.Details(1);
            var detailsTwo = service.Details(2);

            Assert.That(detailsOne.Result.Name, Is.EqualTo("Benjamin Sesko"));
            Assert.That(detailsTwo.Result.Name, Is.EqualTo("Stanislav Ivanov"));

            data.RemoveRange(countries);
            data.RemoveRange(teams);
            data.RemoveRange(footballers);
            data.SaveChanges();
        }


        [Test]
        public void TestDatesAreCorrect()
        {
            countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            teams = new List<Team>()
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

            footballers = new List<Footballer>()
            {
                new Footballer(){Id = 1, FirstName = "Benjamin", LastName = "Sesko", BirthDay = DateTime.Parse("1993/03/10"), CountryId = 3,
                Position = Position.Midfielder, PreferedFoot = Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Defender, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.AddRange(teams);
            data.AddRange(footballers);
            data.SaveChanges();

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

            data.RemoveRange(countries);
            data.RemoveRange(teams);
            data.RemoveRange(footballers);
            data.SaveChanges();
        }

        [Test]
        public void TestYearsFootballer()
        {
            countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            teams = new List<Team>()
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

            footballers = new List<Footballer>()
            {
                new Footballer(){Id = 1, FirstName = "Benjamin", LastName = "Sesko", BirthDay = DateTime.Parse("1993/03/10"), CountryId = 3,
                Position = Position.Midfielder, PreferedFoot = Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Defender, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.AddRange(teams);
            data.AddRange(footballers);
            data.SaveChanges();

            IFootballerService service = new FootballerService(data);
            AddNewFootallerModel model = new AddNewFootallerModel()
            {
                FirstName = "Grigor",
                LastName = "Ivanov",
                BirthDay = DateTime.Parse("1993/03/10"),
                CountryId = 3,
                Position = Position.Midfielder,
                PreferedFoot = Foot.Left,
                CurrentMarketValue = "1000000",


            };

            AddNewFootallerModel model2 = new AddNewFootallerModel()
            {
                FirstName = "Grigor",
                LastName = "Ivanov",
                BirthDay = DateTime.Parse("1893/03/10"),
                CountryId = 3,
                Position = Position.Midfielder,
                PreferedFoot = Foot.Left,
                CurrentMarketValue = "1000000",


            };
            var result = service.AreDtaesCorrect(model);
            var result2 = service.AreDtaesCorrect(model2);

            Assert.That(result.ToString, Is.EqualTo("True"));
            Assert.That(result2.ToString, Is.EqualTo("False"));

            data.RemoveRange(countries);
            data.RemoveRange(teams);
            data.RemoveRange(footballers);
            data.SaveChanges();
        }

        [Test]
        public void TestFindFootballers()
        {
            countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            teams = new List<Team>()
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

            footballers = new List<Footballer>()
            {
                new Footballer(){Id = 1, FirstName = "Benjamin", LastName = "Sesko", BirthDay = DateTime.Parse("1993/03/10"), CountryId = 3,
                Position = Position.Midfielder, PreferedFoot = Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Defender, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.AddRange(teams);
            data.AddRange(footballers);
            data.SaveChanges();

            IFootballerService service = new FootballerService(data);
            var result = service.FindFootballers("kostadin");
            var result2 = service.FindFootballers("ses");
            var result3 = service.FindFootballers("nov");

            Assert.That(result.Result.Count, Is.EqualTo(0));
            Assert.That(result2.Result.Count, Is.EqualTo(1));
            Assert.That(result3.Result.Count, Is.EqualTo(3));

            data.RemoveRange(countries);
            data.RemoveRange(teams);
            data.RemoveRange(footballers);
            data.SaveChanges();
        }

        [Test]
        public void TestIsPlayerSignedToAClub()
        {
            countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            teams = new List<Team>()
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

            footballers = new List<Footballer>()
            {
                new Footballer(){Id = 1, FirstName = "Benjamin", LastName = "Sesko", BirthDay = DateTime.Parse("1993/03/10"), CountryId = 3,
                Position = Position.Midfielder, PreferedFoot = Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Defender, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.AddRange(teams);
            data.AddRange(footballers);
            data.SaveChanges();

            IFootballerService service = new FootballerService(data);
            var result = service.IsheSignedToAClub(1);
            var result2 = service.IsheSignedToAClub(2);

            Assert.That(result.Result.ToString, Is.EqualTo("False"));
            Assert.That(result2.Result.ToString, Is.EqualTo("True"));

            data.RemoveRange(countries);
            data.RemoveRange(teams);
            data.RemoveRange(footballers);
            data.SaveChanges();
        }

        [Test]
        public void TestIsPlayerAlreadyIn()
        {
            countries = new List<Country>()
            {
                 new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                 new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                 new Country(){Id = 3, Name= "Bulgaria", ShortName = "BG"},
                 new Country(){Id = 4, Name= "Greece", ShortName="GR"},
                 new Country(){Id = 5, Name= "Guadalupe", ShortName="GU"},
                 new Country(){Id = 6, Name= "Sweden", ShortName="SD"}
            };

            teams = new List<Team>()
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

            footballers = new List<Footballer>()
            {
                new Footballer(){Id = 1, FirstName = "Benjamin", LastName = "Sesko", BirthDay = DateTime.Parse("1993/03/10"), CountryId = 3,
                Position = Position.Midfielder, PreferedFoot = Foot.Left,
                CurrentMarketValue = decimal.Parse("1000000"),HighestValue = decimal.Parse("1000000"), HishestValueDate = DateTime.Parse("2013/03/10"), },

                new Footballer(){Id = 2, FirstName = "Stanislav", LastName = "Ivanov", BirthDay = DateTime.Parse("2003/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("2000000"),HighestValue = decimal.Parse("3000000"), HishestValueDate = DateTime.Parse("2018/03/10"), TeamId =2},

                new Footballer(){Id = 3, FirstName = "Petar", LastName = "Minchev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10"), TeamId = 2 },

                new Footballer(){Id = 4, FirstName = "Venzislav", LastName = "Urumov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 4,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id = 5, FirstName = "Slivko", LastName = "Trifonov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 6,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10") },

                new Footballer(){Id =6, FirstName = "Milen", LastName = "Inanov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 2,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 7, FirstName = "Florentino", LastName = "Magdanozov", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Forward, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")},

                new Footballer(){Id = 8, FirstName = "Trifon", LastName = "Pachev", BirthDay = DateTime.Parse("2002/03/10"), CountryId = 3,
                Position = Position.Defender, PreferedFoot = Foot.Right,
                CurrentMarketValue = decimal.Parse("200000"),HighestValue = decimal.Parse("1500000"), HishestValueDate = DateTime.Parse("2022/03/10")}
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "MyMockedData")
                  .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(countries);
            data.AddRange(teams);
            data.AddRange(footballers);
            data.SaveChanges();

            IFootballerService service = new FootballerService(data);
            AddNewFootallerModel model = new AddNewFootallerModel()
            {
                FirstName = "Benjamin",
                LastName = "Sesko",
                BirthDay = DateTime.Parse("1993/03/10"),
                CountryId = 3,
                Position = Position.Midfielder,
                PreferedFoot = Foot.Left,
                CurrentMarketValue = "1000000"
            };

            AddNewFootallerModel model2 = new AddNewFootallerModel()
            {
                FirstName = "Benjamin",
                LastName = "Sesko",
                BirthDay = DateTime.Parse("1998/03/10"),
                CountryId = 3,
                Position = Position.Midfielder,
                PreferedFoot = Foot.Left,
                CurrentMarketValue = "1000000"
            };
            var isIn = service.IsAlreadyIn(model);
            var isNotIn = service.IsAlreadyIn(model2);
            Assert.That(isIn.Result.ToString, Is.EqualTo("True"));
            Assert.That(isNotIn.Result.ToString, Is.EqualTo("False"));

            data.RemoveRange(countries);
            data.RemoveRange(teams);
            data.RemoveRange(footballers);
            data.SaveChanges();
        }
    }
}
