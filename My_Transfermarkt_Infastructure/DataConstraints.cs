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

            public const string Name = @"^[A-Z]{1}[a-z]* * [a-z]* *[A-Z]*[a-z]* *[A-Z]*[a-z]* *[A-Z]*[a-z]* *[A-Z]*[a-z]*";
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
    }
}
