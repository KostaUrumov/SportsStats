﻿namespace SportsStats_Core.Models.FootballerModels
{
    public class ShowFootballerModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int InternationalCaps { get; set; }
        public string BirthDay { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Foot { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string? CurrentTeam { get; set; }
        public string CurrentValue { get; set; } = null!;
        public string HighestValue { get; set; } = null!;
        public byte[] Photo { get; set; } = null!;
        public string? StartContract { get; set; }
        public string? EndContract { get; set; }
        public bool IsRetired { get; set; }
    }
}
