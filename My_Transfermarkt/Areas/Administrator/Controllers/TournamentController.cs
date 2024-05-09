using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Transfermarkt_Core.Contracts;
using My_Transfermarkt_Core.Models.MatchModels;
using My_Transfermarkt_Core.Models.TournamentModels;

namespace My_Transfermarkt.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TournamentController : BaseController
    {
        private readonly ITournamentService tournamentService;
        private readonly ITeamService teamService;
        private readonly IGroupService groupService;
        private readonly IMatchService matchService;

        public TournamentController(
            ITournamentService _service,
            ITeamService _team,
            IGroupService _group,
            IMatchService _matchService)
        {
            tournamentService = _service;
            teamService = _team;
            groupService = _group; 
            matchService = _matchService;
        }

        public IActionResult AddNewTournament()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddGroupStageTournament()
        {
            AddNewGroupStageTournament model = new AddNewGroupStageTournament();
            model.StartDate = DateTime.Now;
            model.EndtDate = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddGroupStageTournament(AddNewGroupStageTournament model)
        {
            if (groupService.NumberOfGroupsAreCorrect(model) == false)
            {
                ViewBag.Comment = "Groups must be between 2 and 25 ";
                return View(model);
            }

            if (groupService.TeamsInGroupAreCorrect(model.TeamsNumberInGroup) == false)
            {
                ViewBag.Comment = "Teams per Group must be between 2 and 12 ";
                return View(model);
            }

            if (tournamentService.TotalTeamsAreCorrect(model.NumberOfTeams) == false)
            {
                ViewBag.Comment = "Teams in tournament must be between 4 and 100 ";
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                
                return View(model);
            }

            if (tournamentService.AreDatesCorrect(model) == false)
            {
                ViewBag.Comment = "Dates are not correctly set";
                return View(model);
            }

            var isTournamentAlreadyIn = await tournamentService.CheckIfTournamentIsIn(model.Name);
            if (isTournamentAlreadyIn != null)
            {
                ViewBag.Comment = "Tournament is Already Created";
                return View(model);
            }

            await tournamentService.AddNewTournamentAsync(model);


            return RedirectToAction(nameof(AllTournaments));
        }


        [HttpGet]
        public IActionResult AddNewSingleTournament()
        {
            AddNewSingleGroupTournamentModel model = new AddNewSingleGroupTournamentModel();
            model.StartDate = DateTime.Now;
            model.EndtDate = DateTime.Now;
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddNewSingleTournament(AddNewSingleGroupTournamentModel model)
        {
            if (tournamentService.TotalTeamsAreCorrect(model.NumberOfTeams) == false)
            {
                ViewBag.Comment = "Teams in tournament must be between 4 and 100 ";
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Comment = "Name Is not Valid";
                return View(model);
            }

            if (tournamentService.AreDatesCorrect(model) == false)
            {
                ViewBag.Comment = "Dates are not correctly set";
                return View(model);
            }

            var isTournamentAlreadyIn = await tournamentService.CheckIfTournamentIsIn(model.Name);
            if (isTournamentAlreadyIn != null)
            {
                ViewBag.Comment = "Tournament is Already Created";
                return View(model);
            }

            await tournamentService.AddNewTournamentAsync(model);


            return RedirectToAction(nameof(AllTournaments));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var findTournament = await tournamentService.FindTournament(id);
            if (findTournament == null)
            {
                return View("Error404", new { area = "" });
            }
            EditTournamentModel model = new EditTournamentModel()
            {
                Name = findTournament.Name,
                Id = findTournament.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTournamentModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Comment = "Name Is not Valid";
                return View(model);
            }


            var isTournamentAlreadyIn = await tournamentService.CheckIfTournamentIsIn(model.Name);
            if (isTournamentAlreadyIn != null)
            {
                var find = await tournamentService.FindTournament(model.Id);
                model.Name = find.Name;
                ViewBag.Comment = "Tournament is Already Created";
                return View(model);
            }

            await tournamentService.SaveChangesAsync(model);
            return RedirectToAction(nameof(AllTournaments));
        }

        public async Task<IActionResult> AllTournaments()
        {
            var result = await tournamentService.GetAllTournaments();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> AddTeams(int Id)
        {
            AddTeamsToTournament model = new AddTeamsToTournament()
            {
                Id = Id,
                Teams = await teamService.GetAllTeamsAvailable()
            };
            ViewBag.Tournament = await tournamentService.GetName(Id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeams(int Id, AddTeamsToTournament model)
        {
            var tournament = await tournamentService.FindTournament(Id);
            model.Id = tournament.Id;

            if (tournament == null)
            {
                return View("Error404", new { area = "" });
            }
            int[] teams = new int[model.SelectedTeams.Count()];
            for (int i = 0; i < teams.Length; i++)
            {
                var team = await teamService.FindTeam(model.SelectedTeams[i]);
                var isTeamIn = await tournamentService.IsTeamInTournament(Id, team.Id);
                if (isTeamIn == true)
                {
                    continue;
                }
                if (team == null)
                {
                    return View("Error404", new { area = "" });
                }
                teams[i] = model.SelectedTeams[i];
                await tournamentService.AddTeamToTournament(Id, model.SelectedTeams[i]);
            }

            return RedirectToAction("CurrentTeams", model);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveTeams(int Id)
        {
            AddTeamsToTournament model = new AddTeamsToTournament
            {
                Id = Id,
                Teams = await teamService.CurrentTeamsInTournament(Id)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTeams(int Id, AddTeamsToTournament model)
        {
            var tournament = await tournamentService.FindTournament(Id);
            if (tournament == null)
            {
                return View("Error404", new { area = "" });
            }
            int[] teams = new int[model.SelectedTeams.Count()];
            for (int i = 0; i < teams.Length; i++)
            {
                if (await matchService.IsTeamAssignedToMatch(model.SelectedTeams[i], tournament.Id) == true)
                {
                    continue;
                }

                if (await tournamentService.IsItGroupStageTournament(Id) == true)
                {
                    
                    await tournamentService.RemoveFromGroup(Id, model.SelectedTeams[i]);
                }
                await tournamentService.RemoveFromTournament(Id, model.SelectedTeams[i]);
                
            }
            return RedirectToAction("CurrentTeams", model);
        }

        public async Task<IActionResult> CurrentTeams(AddTeamsToTournament model)
        {
            var tournament = await tournamentService.FindTournament(model.Id);

            if (tournament == null)
            {
                return View("Error404", new { area = "" });
            }

            var result = await tournamentService.GetDetails(model.Id);
            return View(result);

        }

        public async Task<IActionResult> Matches (int Id)
        {
            if (TempData["Id"] != null)
            {
                Id = (int)TempData["Id"];
                TempData.Remove("Id");
            }
            var tournament = await tournamentService.FindTournament(Id);

            if (tournament == null)
            {
                return View("Error404", new { area = "" });
            }
            var result = await tournamentService.FindMatchesInTournament(Id);
            ViewBag.Tournament = tournament.Name;
            ViewBag.Id = tournament.Id;
            return View("ResultMatches", result);
        }

        public IActionResult ResultMatches(List<ShowMatchModel> result)
        {
            return View(result); 
        }

    }
}
