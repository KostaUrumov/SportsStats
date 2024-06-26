﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStats_Infastructure.DataModels
{
    public class GroupsTournament
    {

        public int TournamenId { get; set; }

        [ForeignKey(nameof(TournamenId))]
        public Tournament Tournament { get; set; } = null!;

        public int GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        public Group Group { get; set; } = null!;
    }
}
