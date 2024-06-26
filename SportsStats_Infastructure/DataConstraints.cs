﻿namespace SportsStats_Infastructure
{
    public class DataConstraints
    {
        public class Footballer
        {
            public const int MinName = 2;
            public const int MaxName = 45;

            public const double MinValue = 5000;

            public const int MinCaps = 0;

            public const string ValueRestriction = "[0-9]*[.]?[,]?[0-9]*";
        }

        public class Agent
        {
            public const int MinName = 2;
            public const int MaxName = 45;
        }

        public class Team
        {
            public const int MinName = 2;
            public const int MaxName = 100;
        }

        public class Stadium
        {
            public const int MinName = 2;
            public const int MaxName = 75;
        }

        public class Country
        {
            public const int MinName = 2;
            public const int MaxName = 75;

            public const string Name = @"^[A-Z]{1}[a-z]* *[A-Z]*[a-z]* *[A-Z]*[a-z]* *[A-Z]* *[a-z]* *[A-Z]*[a-z]* *[A-Z]*[a-z]* *[A-Z]* *[a-z]*";
            public const string ShortName = @"[A-Z]{2}";

        }

        public class User
        {
            public const int MinName = 2;
            public const int MaxName = 45;

            public const int MinUserName = 6;
            public const int MaxUserName = 60;

            public const string ExampleName = @"^([A-Z]|[А-Я]){1}([a-z]|[а-я])*";
            public const string ExamplePhone = @"[0]{1}[0-9]{9}";
        }

        public class Tournament
        {
            public const int MinTournamentName = 2;
            public const int MaxTournamentName = 100;
        }

        public class Referee
        {
            public const int MinNameSymbols = 2;
            public const int MaxNameSymbols = 100;

            public const double MinRating = 0.1;
            public const double Maxrating = 10.0;
        }

        public class Match
        {
            public const int MinScore = 0;
            public const int MaxScore = 500;
        }

        public class Group
        {
            public const int MinNumTeams = 2;
            public const int MaxNumTeams = 12;

            public const int MinNumOfRounds = 1;
            public const int MaxNumberOfRounds = 50;
        }

        public class GroupStageTournament
        {
            public const int MinGroupsNumber = 2;
            public const int MaxGroupsNumber = 25;

            public const int MinNumberOfTeams = 4;
            public const int MaxNumberOfTeams = 100;

        }
    }
}
