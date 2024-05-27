using Microsoft.EntityFrameworkCore;
using SportsStats_Core.Contracts;
using SportsStats_Core.Models.TournamentModels;
using SportsStats_Core.Services;
using SportsStats_Infastructure.Data;
using SportsStats_Infastructure.DataModels;

namespace SportsStats_Tests
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

            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
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
            tournament = new List<Tournament>()
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
            data = new ApplicationDbContext(options);
            data.AddRange(teams);
            data.AddRange(countries);
            data.AddRange(tournament);
            data.SaveChanges();

            ITournamentService service = new TournamentService(data);
            var tournaments = service.GetAllTournaments();
            Assert.That(tournaments.Result.Count, Is.EqualTo(5));

            data.RemoveRange(teams);
            data.RemoveRange(countries);
            data.RemoveRange(tournament);
            data.SaveChanges();
        }

        [Test]
        public void TestFindTournament()
        {

            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
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
            data = new ApplicationDbContext(options);
            data.AddRange(teams);
            data.AddRange(countries);
            data.AddRange(this.tournament);
            data.SaveChanges();

            ITournamentService service = new TournamentService(data);
            var tournament = service.FindTournament(1);
            var tournament2 = service.FindTournament(12);

            data.RemoveRange(teams);
            data.RemoveRange(countries);
            data.RemoveRange(this.tournament);
            data.SaveChanges();
        }

        [Test]
        public void TestSaveChanges()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
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
            tournament = new List<Tournament>()
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
            data = new ApplicationDbContext(options);
            data.AddRange(teams);
            data.AddRange(countries);
            data.AddRange(tournament);
            data.SaveChanges();

            ITournamentService service = new TournamentService(data);
            EditTournamentModel model = new EditTournamentModel()
            {
                Id = 1,
                Name = "Test"
            };
            service.SaveChangesAsync(model);
            Assert.That(data.Tournaments.First(x => x.Id == 1).Name, Is.EqualTo("Test"));

            data.RemoveRange(teams);
            data.RemoveRange(countries);
            data.RemoveRange(tournament);
            data.SaveChanges();
        }

        [Test]
        public void TestIfTournamentIsIn()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
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
            tournament = new List<Tournament>()
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
            data = new ApplicationDbContext(options);
            data.AddRange(teams);
            data.AddRange(countries);
            data.AddRange(tournament);
            data.SaveChanges();

            ITournamentService service = new TournamentService(data);
            var result = service.CheckIfTournamentIsIn("Bundesliga 18/19");
            var resultFalse = service.CheckIfTournamentIsIn("Toto Liga");
            Assert.That(result.Result.Id, Is.EqualTo(12));
            Assert.That(resultFalse.Result, Is.EqualTo(null));

            data.RemoveRange(teams);
            data.RemoveRange(countries);
            data.RemoveRange(tournament);
            data.SaveChanges();
        }

        [Test]
        public void TestTournamentDetails()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
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
            tournament = new List<Tournament>()
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
            data = new ApplicationDbContext(options);
            data.AddRange(teams);
            data.AddRange(countries);
            data.AddRange(tournament);
            data.SaveChanges();

            ITournamentService service = new TournamentService(data);
            var result = service.GetDetails(12);
            Assert.That(result.Result.Name, Is.EqualTo("Bundesliga 18/19"));

            data.RemoveRange(teams);
            data.RemoveRange(countries);
            data.RemoveRange(tournament);
            data.SaveChanges();
        }

        [Test]
        public void TestGetName()
        {
            countries = new List<Country>()
            {
                new Country(){Id = 1, Name= "Spain", ShortName = "SP"},
                new Country(){Id = 2, Name= "Belgium", ShortName="BE"},
                new Country(){Id = 3, Name= "Bulgaria", ShortName="BG"},
                new Country(){Id = 4, Name= "England", ShortName="EN"}
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
            tournament = new List<Tournament>()
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
            data = new ApplicationDbContext(options);
            data.AddRange(teams);
            data.AddRange(countries);
            data.AddRange(tournament);
            data.SaveChanges();

            ITournamentService service = new TournamentService(data);
            var result = service.GetName(14);
            Assert.That(result.Result, Is.EqualTo("Vtora Liga 18/19"));

            data.RemoveRange(teams);
            data.RemoveRange(countries);
            data.RemoveRange(tournament);
            data.SaveChanges();
        }
    }
}
