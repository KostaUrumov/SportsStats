﻿namespace SportsStats_Core.Models.TeamModels
{
    public class ShowTeamModelView
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Stadium { get; set; } = null!;
        public byte[]? Picture { get; set; }

    }
}
