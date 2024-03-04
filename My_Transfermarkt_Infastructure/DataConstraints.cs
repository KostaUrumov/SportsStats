using System.Text.RegularExpressions;

namespace My_Transfermarkt_Infastructure
{
    public class DataConstraints
    {
        public class Footballer
        {
            public const int MinName = 2;
            public const int MaxName = 45;

            public const double MinValue = 5000;

            public const int MinCaps = 0;
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

        }

        public class User
        {
            public const int MinName = 2;
            public const int MaxName = 45;

            public const int MinUserName = 6;
            public const int MaxUserName = 60;
        }
    }
}
