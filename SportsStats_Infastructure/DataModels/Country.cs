﻿using SportsStats_Infastructure;
using System.ComponentModel.DataAnnotations;

namespace SportsStats_Infastructure.DataModels
{
    public class Country
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: DataConstraints.Country.MaxName,
            ErrorMessage = "The {0} must be between {1} and {2}",
            MinimumLength = DataConstraints.Country.MinName)]
        [RegularExpression(DataConstraints.Country.Name)]
        public string Name { get; set; } = null!;

        [Required]
        [RegularExpression(DataConstraints.Country.ShortName)]
        public string ShortName { get; set; } = null!;
    }
}