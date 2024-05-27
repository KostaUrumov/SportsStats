using Microsoft.EntityFrameworkCore;
using SportsStats_Core.Contracts;
using SportsStats_Core.Models.TeamModels;
using SportsStats_Core.Services;
using SportsStats_Infastructure.Data;
using SportsStats_Infastructure.DataModels;

namespace SportsStats_Tests
{
    [TestFixture]
    public class TeamServiceTests
    {
        private ApplicationDbContext data;
        private IEnumerable<Team> teams;
        private IEnumerable<Country> countries;
        private IEnumerable<Stadium> stadiums;



        [Test]
        public void TestGetAllTeams()
        {

            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
            };
            stadiums = new List<Stadium>()
            {
                new Stadium { Id = 4, CountryId = 2, Build = DateTime.Parse("1934/08/06"), Capacity = 74667, Name = "Olympiastadion" },
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

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(this.teams);
            data.AddRange(countries);
            data.AddRange(stadiums);
            data.SaveChanges();


            ITeamService service = new TeamService(data);
            var teams = service.GetAllTeams();
            Assert.That(teams.Result.Count, Is.EqualTo(9));

            data.RemoveRange(this.teams);
            data.RemoveRange(countries);
            data.RemoveRange(stadiums);
            data.SaveChanges();

        }

        [Test]
        public void TestFindTeamToEdit()
        {

            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
            };
            stadiums = new List<Stadium>()
            {
                new Stadium { Id = 4, CountryId = 2, Build = DateTime.Parse("1934/08/06"), Capacity = 74667, Name = "Olympiastadion" },
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

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(this.teams);
            data.AddRange(countries);
            data.AddRange(stadiums);
            data.SaveChanges();

            ITeamService service = new TeamService(data);
            var teams = service.FindTeamToBeEdited(1);
            Assert.That(teams.Result.Name, Is.EqualTo("Manchester United"));

            data.RemoveRange(this.teams);
            data.RemoveRange(countries);
            data.RemoveRange(stadiums);
            data.SaveChanges();


        }

        [Test]
        public void TestFindTeamToEditNull()
        {

            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
            };
            stadiums = new List<Stadium>()
            {
                new Stadium { Id = 4, CountryId = 2, Build = DateTime.Parse("1934/08/06"), Capacity = 74667, Name = "Olympiastadion" },
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

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(this.teams);
            data.AddRange(countries);
            data.AddRange(stadiums);
            data.SaveChanges();

            ITeamService service = new TeamService(data);
            var teams = service.FindTeamToBeEdited(100);
            Assert.That(teams.Result, Is.EqualTo(null));

            data.RemoveRange(this.teams);
            data.RemoveRange(countries);
            data.RemoveRange(stadiums);
            data.SaveChanges();

        }

        [Test]
        public void TestIsalreadyCreatedTrue()
        {

            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
            };
            stadiums = new List<Stadium>()
            {
                new Stadium { Id = 4, CountryId = 2, Build = DateTime.Parse("1934/08/06"), Capacity = 74667, Name = "Olympiastadion" },
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

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(this.teams);
            data.AddRange(countries);
            data.AddRange(stadiums);
            data.SaveChanges();

            AddNewTeamModel model = new AddNewTeamModel()
            {
                Id = 4,
                CountryId = 4,
                Name = "Arsenal"
            };

            AddNewTeamModel model2 = new AddNewTeamModel()
            {
                Id = 5,
                CountryId = 4,
                Name = "Arsenal"
            };


            ITeamService service = new TeamService(data);
            var teams = service.IsAlreadyCreated(model);
            var teams2 = service.IsAlreadyCreated(model2);
            Assert.That(teams.Result.ToString, Is.EqualTo("False"));
            Assert.That(teams2.Result.ToString, Is.EqualTo("True"));

            data.RemoveRange(this.teams);
            data.RemoveRange(countries);
            data.RemoveRange(stadiums);
            data.SaveChanges();

        }

        [Test]
        public void TestIsalreadyCreatedFalse()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
            };
            stadiums = new List<Stadium>()
            {
                new Stadium { Id = 4, CountryId = 2, Build = DateTime.Parse("1934/08/06"), Capacity = 74667, Name = "Olympiastadion" },
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

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(this.teams);
            data.AddRange(countries);
            data.AddRange(stadiums);
            data.SaveChanges();


            AddNewTeamModel model = new AddNewTeamModel()
            {
                Id = 1,
                CountryId = 4,
                Name = "Gotebord"
            };

            ITeamService service = new TeamService(data);
            var teams = service.IsAlreadyCreated(model);
            Assert.That(teams.Result.ToString, Is.EqualTo("False"));

            data.RemoveRange(this.teams);
            data.RemoveRange(countries);
            data.RemoveRange(stadiums);
            data.SaveChanges();

        }

        [Test]
        public void TestIFindTeamTrue()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
            };
            stadiums = new List<Stadium>()
            {
                new Stadium { Id = 4, CountryId = 2, Build = DateTime.Parse("1934/08/06"), Capacity = 74667, Name = "Olympiastadion" },
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

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(this.teams);
            data.AddRange(countries);
            data.AddRange(stadiums);
            data.SaveChanges();


            ITeamService service = new TeamService(data);
            var teams = service.FindTeam(2);
            Assert.That(teams.Result.TeamName, Is.EqualTo("Botev Plovdiv"));

            data.RemoveRange(this.teams);
            data.RemoveRange(countries);
            data.RemoveRange(stadiums);
            data.SaveChanges();

        }

        [Test]
        public void TestIFindTeamFalse()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
            };
            stadiums = new List<Stadium>()
            {
                new Stadium { Id = 4, CountryId = 2, Build = DateTime.Parse("1934/08/06"), Capacity = 74667, Name = "Olympiastadion" },
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

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(this.teams);
            data.AddRange(countries);
            data.AddRange(stadiums);
            data.SaveChanges();


            ITeamService service = new TeamService(data);
            var teams = service.FindTeam(200);
            Assert.That(teams.Result, Is.EqualTo(null));

            data.RemoveRange(this.teams);
            data.RemoveRange(countries);
            data.RemoveRange(stadiums);
            data.SaveChanges();

        }
        [Test]
        public void TestAddToStadium()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
            };
            stadiums = new List<Stadium>()
            {
                new Stadium { Id = 4, CountryId = 2, Build = DateTime.Parse("1934/08/06"), Capacity = 74667, Name = "Olympiastadion" },
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

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(teams);
            data.AddRange(countries);
            data.AddRange(stadiums);
            data.SaveChanges();

            TeamToAddStadium team = new TeamToAddStadium()
            {
                Id = 1,
                StadiumId = 4,
            };

            ITeamService service = new TeamService(data);
            service.AddToStadiumAsync(team);
            var stadiumName = data.Teams.First(x => x.Id == 1);
            Assert.That(stadiumName.StadiumId, Is.EqualTo(4));

            data.RemoveRange(teams);
            data.RemoveRange(countries);
            data.RemoveRange(stadiums);
            data.SaveChanges();

        }

        [Test]
        public void TestRandomList()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
            };
            stadiums = new List<Stadium>()
            {
                new Stadium { Id = 4, CountryId = 2, Build = DateTime.Parse("1934/08/06"), Capacity = 74667, Name = "Olympiastadion" },
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

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(teams);
            data.AddRange(countries);
            data.AddRange(stadiums);
            data.SaveChanges();

            ITeamService service = new TeamService(data);
            var result = service.GetRandomListForHomePage();
            Assert.That(result.Result.Count(), Is.EqualTo(8));

            data.RemoveRange(teams);
            data.RemoveRange(countries);
            data.RemoveRange(stadiums);
            data.SaveChanges();


        }

        [Test]
        public void TestFindteamByCountry()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
            };
            stadiums = new List<Stadium>()
            {
                new Stadium { Id = 4, CountryId = 2, Build = DateTime.Parse("1934/08/06"), Capacity = 74667, Name = "Olympiastadion" },
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

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(teams);
            data.AddRange(countries);
            data.AddRange(stadiums);
            data.SaveChanges();

            string country = "Spain";

            ITeamService service = new TeamService(data);
            var result = service.FindTeamByCountry(country);
            Assert.That(result.Result.Count(), Is.EqualTo(2));

            var result2 = service.FindTeamByCountry("spa");
            Assert.That(result2.Result.Count(), Is.EqualTo(2));

            var result3 = service.FindTeamByCountry("Bulga");
            Assert.That(result3.Result.Count(), Is.EqualTo(3));

            data.RemoveRange(teams);
            data.RemoveRange(countries);
            data.RemoveRange(stadiums);
            data.SaveChanges();


        }
        [Test]
        public void TestFindteamByBame()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
            };
            stadiums = new List<Stadium>()
            {
                new Stadium { Id = 4, CountryId = 2, Build = DateTime.Parse("1934/08/06"), Capacity = 74667, Name = "Olympiastadion" },
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

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(teams);
            data.AddRange(countries);
            data.AddRange(stadiums);
            data.SaveChanges();

            ITeamService service = new TeamService(data);
            var result = service.FindTeams("manche");
            var result2 = service.FindTeams("ars");
            var result3 = service.FindTeams("ar");

            Assert.That(result.Result.Count(), Is.EqualTo(1));
            Assert.That(result2.Result.Count(), Is.EqualTo(1));
            Assert.That(result3.Result.Count(), Is.EqualTo(4));

            data.RemoveRange(teams);
            data.RemoveRange(countries);
            data.RemoveRange(stadiums);
            data.SaveChanges();

        }

        [Test]
        public void TestAddNewTeam()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
            };
            stadiums = new List<Stadium>()
            {
                new Stadium { Id = 4, CountryId = 2, Build = DateTime.Parse("1934/08/06"), Capacity = 74667, Name = "Olympiastadion" },
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

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(teams);
            data.AddRange(countries);
            data.AddRange(stadiums);
            data.SaveChanges();

            ITeamService service = new TeamService(data);
            AddNewTeamModel model = new AddNewTeamModel()
            {
                StadiumId = 1,
                Id = 1,
                Name = "Strumska Slava",
                CountryId = 1,

            };
            service.AddNewTeamAsync(model);
            Assert.That(data.Teams.Count, Is.EqualTo(10));

            data.RemoveRange(teams);
            data.RemoveRange(countries);
            data.RemoveRange(stadiums);
            data.SaveChanges();


        }

        [Test]
        public void TestChangeTeam()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
            };
            stadiums = new List<Stadium>()
            {
                new Stadium { Id = 4, CountryId = 2, Build = DateTime.Parse("1934/08/06"), Capacity = 74667, Name = "Olympiastadion" },
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

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            data = new ApplicationDbContext(options);
            data.AddRange(teams);
            data.AddRange(countries);
            data.AddRange(stadiums);
            data.SaveChanges();

            ITeamService service = new TeamService(data);
            AddNewTeamModel model = new AddNewTeamModel()
            {
                StadiumId = 1,
                Id = 1,
                Name = "Strumska Slava",
                CountryId = 1,

            };
            service.SaveChangesAsync(model);
            var result = service.FindTeamToBeEdited(1);
            Assert.That(result.Result.Name, Is.EqualTo("Strumska Slava"));
            Assert.That(result.Result.StadiumId, Is.EqualTo(1));
            Assert.That(result.Result.CountryId, Is.EqualTo(1));

            data.RemoveRange(teams);
            data.RemoveRange(countries);
            data.RemoveRange(stadiums);
            data.SaveChanges();

        }



    }

}
