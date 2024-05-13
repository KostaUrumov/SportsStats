using Microsoft.EntityFrameworkCore;
using My_Transfermarkt.Data;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.TournamentModels;
using My_Transfermarkt_Core.Services;
using My_Transfermarkt_Infastructure.DataModels;

namespace My_Transfermarkt_Tests
{
    [TestFixture]
    public class TournamentServiceTests
    {
        private IEnumerable<Tournament> tournament;
        private IEnumerable<Team> teams;
        private ApplicationDbContext data;
        private IEnumerable<Country> countries;

       

        [Test]
        public void TestGetAllTournaments()
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
                new Team{Id =4, CountryId = 4, Name = "Arsenal" },
                new Team{Id =5, CountryId = 1, Name = "Barcelona" },
                new Team{Id =6, CountryId = 1, Name = "Real Madrid" },
                new Team{Id =7, CountryId = 2, Name = "Standard" },
                new Team{Id =8, CountryId = 2, Name = "Brugge" },
                new Team{Id =9, CountryId = 3, Name = "Spartak Varna" },
            };
            this.tournament = new List<Tournament>()
            {
                new Tournament{Id = 1,Name = "Champions League 18/19"},
                new Tournament{Id = 12,Name = "Bundesliga 18/19"},
                new Tournament{Id = 13,Name = "LaLiga 18/19"},
                new Tournament{Id = 11,Name = "Parva Liga 18/19"},
                new Tournament{Id = 14,Name = "Vtora Liga 18/19"}

            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.teams);
            this.data.AddRange(this.countries);
            this.data.AddRange(this.tournament);
            this.data.SaveChanges();

            ITournamentService service = new TournamentService(data);
            var tournaments = service.GetAllTournaments();
            Assert.That(tournaments.Result.Count, Is.EqualTo(5));

            this.data.RemoveRange(this.teams);
            this.data.RemoveRange(this.countries);
            this.data.RemoveRange(this.tournament);
            this.data.SaveChanges();
        }

        [Test]
        public void TestFindTournament()
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
                new Team{Id =4, CountryId = 4, Name = "Arsenal" },
                new Team{Id =5, CountryId = 1, Name = "Barcelona" },
                new Team{Id =6, CountryId = 1, Name = "Real Madrid" },
                new Team{Id =7, CountryId = 2, Name = "Standard" },
                new Team{Id =8, CountryId = 2, Name = "Brugge" },
                new Team{Id =9, CountryId = 3, Name = "Spartak Varna" },
            };
            this.tournament = new List<Tournament>()
            {
                new Tournament{Id = 1,Name = "Champions League 18/19"},
                new Tournament{Id = 12,Name = "Bundesliga 18/19"},
                new Tournament{Id = 13,Name = "LaLiga 18/19"},
                new Tournament{Id = 11,Name = "Parva Liga 18/19"},
                new Tournament{Id = 14,Name = "Vtora Liga 18/19"}

            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.teams);
            this.data.AddRange(this.countries);
            this.data.AddRange(this.tournament);
            this.data.SaveChanges();

            ITournamentService service = new TournamentService(data);
            var tournament = service.FindTournament(1);
            var tournament2 = service.FindTournament(12);
            
            this.data.RemoveRange(this.teams);
            this.data.RemoveRange(this.countries);
            this.data.RemoveRange(this.tournament);
            this.data.SaveChanges();
        }

        [Test]
        public void TestSaveChanges()
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
                new Team{Id =4, CountryId = 4, Name = "Arsenal" },
                new Team{Id =5, CountryId = 1, Name = "Barcelona" },
                new Team{Id =6, CountryId = 1, Name = "Real Madrid" },
                new Team{Id =7, CountryId = 2, Name = "Standard" },
                new Team{Id =8, CountryId = 2, Name = "Brugge" },
                new Team{Id =9, CountryId = 3, Name = "Spartak Varna" },
            };
            this.tournament = new List<Tournament>()
            {
                new Tournament{Id = 1,Name = "Champions League 18/19"},
                new Tournament{Id = 12,Name = "Bundesliga 18/19"},
                new Tournament{Id = 13,Name = "LaLiga 18/19"},
                new Tournament{Id = 11,Name = "Parva Liga 18/19"},
                new Tournament{Id = 14,Name = "Vtora Liga 18/19"}

            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.teams);
            this.data.AddRange(this.countries);
            this.data.AddRange(this.tournament);
            this.data.SaveChanges();

            ITournamentService service = new TournamentService(data);
            EditTournamentModel model = new EditTournamentModel()
            {
                Id = 1,
                Name = "Test"
            };
            service.SaveChangesAsync(model);
            Assert.That(data.Tournaments.First(x => x.Id == 1).Name, Is.EqualTo("Test"));

            this.data.RemoveRange(this.teams);
            this.data.RemoveRange(this.countries);
            this.data.RemoveRange(this.tournament);
            this.data.SaveChanges();
        }

        [Test]
        public void TestIfTournamentIsIn()
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
                new Team{Id =4, CountryId = 4, Name = "Arsenal" },
                new Team{Id =5, CountryId = 1, Name = "Barcelona" },
                new Team{Id =6, CountryId = 1, Name = "Real Madrid" },
                new Team{Id =7, CountryId = 2, Name = "Standard" },
                new Team{Id =8, CountryId = 2, Name = "Brugge" },
                new Team{Id =9, CountryId = 3, Name = "Spartak Varna" },
            };
            this.tournament = new List<Tournament>()
            {
                new Tournament{Id = 1,Name = "Champions League 18/19"},
                new Tournament{Id = 12,Name = "Bundesliga 18/19"},
                new Tournament{Id = 13,Name = "LaLiga 18/19"},
                new Tournament{Id = 11,Name = "Parva Liga 18/19"},
                new Tournament{Id = 14,Name = "Vtora Liga 18/19"}

            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.teams);
            this.data.AddRange(this.countries);
            this.data.AddRange(this.tournament);
            this.data.SaveChanges();

            ITournamentService service = new TournamentService(data);
            var result = service.CheckIfTournamentIsIn("Bundesliga 18/19");
            var resultFalse = service.CheckIfTournamentIsIn("Toto Liga");
            Assert.That(result.Result.Id, Is.EqualTo(12));
            Assert.That(resultFalse.Result, Is.EqualTo(null));

            this.data.RemoveRange(this.teams);
            this.data.RemoveRange(this.countries);
            this.data.RemoveRange(this.tournament);
            this.data.SaveChanges();
        }

        [Test]
        public void TestTournamentDetails()
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
                new Team{Id =4, CountryId = 4, Name = "Arsenal" },
                new Team{Id =5, CountryId = 1, Name = "Barcelona" },
                new Team{Id =6, CountryId = 1, Name = "Real Madrid" },
                new Team{Id =7, CountryId = 2, Name = "Standard" },
                new Team{Id =8, CountryId = 2, Name = "Brugge" },
                new Team{Id =9, CountryId = 3, Name = "Spartak Varna" },
            };
            this.tournament = new List<Tournament>()
            {
                new Tournament{Id = 1,Name = "Champions League 18/19"},
                new Tournament{Id = 12,Name = "Bundesliga 18/19"},
                new Tournament{Id = 13,Name = "LaLiga 18/19"},
                new Tournament{Id = 11,Name = "Parva Liga 18/19"},
                new Tournament{Id = 14,Name = "Vtora Liga 18/19"}

            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.teams);
            this.data.AddRange(this.countries);
            this.data.AddRange(this.tournament);
            this.data.SaveChanges();

            ITournamentService service = new TournamentService(data);
            var result = service.GetDetails(12);
            Assert.That(result.Result.Name, Is.EqualTo("Bundesliga 18/19"));

            this.data.RemoveRange(this.teams);
            this.data.RemoveRange(this.countries);
            this.data.RemoveRange(this.tournament);
            this.data.SaveChanges();
        }

        [Test]
        public void TestGetName()
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
                new Team{Id =4, CountryId = 4, Name = "Arsenal" },
                new Team{Id =5, CountryId = 1, Name = "Barcelona" },
                new Team{Id =6, CountryId = 1, Name = "Real Madrid" },
                new Team{Id =7, CountryId = 2, Name = "Standard" },
                new Team{Id =8, CountryId = 2, Name = "Brugge" },
                new Team{Id =9, CountryId = 3, Name = "Spartak Varna" },
            };
            this.tournament = new List<Tournament>()
            {
                new Tournament{Id = 1,Name = "Champions League 18/19"},
                new Tournament{Id = 12,Name = "Bundesliga 18/19"},
                new Tournament{Id = 13,Name = "LaLiga 18/19"},
                new Tournament{Id = 11,Name = "Parva Liga 18/19"},
                new Tournament{Id = 14,Name = "Vtora Liga 18/19"}

            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MyMockedData")
                   .Options;
            this.data = new ApplicationDbContext(options);
            this.data.AddRange(this.teams);
            this.data.AddRange(this.countries);
            this.data.AddRange(this.tournament);
            this.data.SaveChanges();

            ITournamentService service = new TournamentService(data);
            var result = service.GetName(14);
            Assert.That(result.Result, Is.EqualTo("Vtora Liga 18/19"));

            this.data.RemoveRange(this.teams);
            this.data.RemoveRange(this.countries);
            this.data.RemoveRange(this.tournament);
            this.data.SaveChanges();
        }
    }
}
