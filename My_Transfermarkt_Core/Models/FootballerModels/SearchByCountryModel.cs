﻿using System.ComponentModel.DataAnnotations;

namespace My_Transfermarkt_Core.Models.FootballerModels
{
    public class SearchByCountryModel
    {
        [Required]
        public string Country { get; set; } = null!;
    }
}
