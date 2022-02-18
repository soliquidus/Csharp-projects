using System;

namespace Formation
{
    static class utils
    {
        public static int AskNumberNotNullPositif(string question)
        {
            return AskNumberBetween(question, 1, int.MaxValue, "ERROR: Number must be positive and not null");
        }

        public static int AskNumberBetween(string question, int min, int max, string errorMessage = null)
        {
            while (true)
            {
                var number = AskNumber(question);
                if (number >= min && number <= max)
                {
                    return number;
                }

                Console.WriteLine(errorMessage ?? $"ERROR: Number must be between {min} and {max}");
                Console.WriteLine();
            }
        }

        private static int AskNumber(string question)
        {
            while (true)
            {
                Console.WriteLine(question);
                var response = Console.ReadLine();
                try
                {
                    var responseInt = int.Parse(response);
                    return responseInt;
                }
                catch
                {
                    Console.WriteLine("ERROR: You need to enter a number");
                    Console.WriteLine();
                }
            }
        }
    }
}