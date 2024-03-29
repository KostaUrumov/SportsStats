using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.TeamModels;
using My_Transfermarkt_Core.Services;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Tests
{
    [TestFixture]
    public class TeamServiceTests
    {
        private ApplicationDbContext data;
        private IEnumerable<Team> teams;
        private IEnumerable<Country> countries;


        [SetUp]
        public void Setup()
        {
            this.countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
            };
            
            this.teams = new List<Team>()
            {
                new Team{Id =1, CountryId = 4, Name = "Manchester United" },
                new Team{Id =2, CountryId = 3, Name = "Botev Plovdiv" },
                new Team{Id =3, CountryId = 3, Name = "Levski Sofia" },
                new Team{Id =4, CountryId = 4, Name = "Arsenal" }
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.teams);
            this.data.AddRange(this.countries);
            this.data.SaveChanges();

        }

        [Test]
        public void TestGetAllTeams()
        {
            ITeamService service = new TeamService(data);
            var teams = service.GetAllTeams();
            Assert.That(teams.Result.Count, Is.EqualTo(4));
        }

        [Test]
        public void TestFindTeamToEdit()
        {
            ITeamService service = new TeamService(data);
            var teams = service.FindTeamToBeEdited(1);
            Assert.That(teams.Result.Name, Is.EqualTo("Manchester United"));
        }

        [Test]
        public void TestFindTeamToEditNull()
        {
            ITeamService service = new TeamService(data);
            var teams = service.FindTeamToBeEdited(100);
            Assert.That(teams.Result, Is.EqualTo(null));
        }

        [Test]
        public void TestIsalreadyCreatedTrue()
        {
            AddNewTeamModel model = new AddNewTeamModel()
            {
                Id = 4,
                CountryId = 4,
                Name = "Arsenal"
            };

            ITeamService service = new TeamService(data);
            var teams = service.IsAlreadyCreated(model);
            Assert.That(teams.Result.ToString, Is.EqualTo("True"));
        }

        [Test]
        public void TestIsalreadyCreatedFalse()
        {
            AddNewTeamModel model = new AddNewTeamModel()
            {
                Id = 1,
                CountryId = 4,
                Name = "Gotebord"
            };

            ITeamService service = new TeamService(data);
            var teams = service.IsAlreadyCreated(model);
            Assert.That(teams.Result.ToString, Is.EqualTo("False"));
        }
    }
}
