﻿namespace My_Transfermarkt_Core.Models.StadiumModels
{
    public class StadiumViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Built { get; set; } = null!;
        public int Capacity { get; set; }
        public string Country { get; set; } = null!;
    }
}