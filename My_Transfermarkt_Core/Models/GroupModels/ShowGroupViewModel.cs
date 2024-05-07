﻿using My_Transfermarkt_Core.Models.TeamModels;

namespace My_Transfermarkt_Core.Models.GroupModels
{
    public class ShowGroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string TournamentName { get; set; } = null!;
        public ICollection<ShowTeamModelView>? Teams { get; set; } = new List<ShowTeamModelView>();
        
    }
}