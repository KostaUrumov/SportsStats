﻿using SportsStats_Infastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStats_Infastructure.DataModels
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: DataConstraints.Team.MaxName,
            ErrorMessage = "The {0} must be between {1} and {2}",
            MinimumLength = DataConstraints.Team.MinName)]
        public string Name { get; set; } = null!;

        [Required]
        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; } = null!;

        [Required]
        public byte[] Logo { get; set; } = null!;


        public int? StadiumId { get; set; }

        [ForeignKey(nameof(StadiumId))]
        public Stadium Stadium { get; set; } = null!;

        [InverseProperty(nameof(Match.HomeTeam))]
        public virtual ICollection<Match> HomeGames { get; set; } = new List<Match>();

        [InverseProperty(nameof(Match.AwayTeam))]
        public virtual ICollection<Match> AwayGames { get; set; } = new List<Match>();

        public ICollection<TeamsFootballers> TeamFootballers { get; set; } = new List<TeamsFootballers>();
        public ICollection<Match> Matches { get; set; } = new List<Match>();

    }
}
