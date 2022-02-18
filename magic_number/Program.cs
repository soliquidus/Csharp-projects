using System;

namespace magic_number
{
    class Program
    {
        private static int AskNumber(int min, int max)
        {
            var userNumber = min - 1;

            while (userNumber < min || userNumber > max)
            {
                Console.Write($"Enter a number between {min} and {max}: ");
                var response = Console.ReadLine();

                try
                {
                    if (response != null) userNumber = int.Parse(response);
                    
                    if (userNumber < min || userNumber > max)
                    {
                        Console.WriteLine($"ERROR: number must be between {min} and {max}");
                    }
                }
                catch
                {
                    Console.WriteLine("ERROR: Enter a valid number");
                }
            }

            return userNumber;
        }

        private static void Main(string[] args)
        {
            var random = new Random();
            const int minNumber = 1;
            const int maxNumber = 10;
            var magicNumber = random.Next(minNumber, maxNumber+1);
            
            var number = magicNumber + 1; 
            // var credits = 4;

            // while (credits > 0)
            for (var credits = 4; credits > 0; credits--)
            {
                Console.WriteLine();
                Console.WriteLine("Credits left: " + credits);
                number = AskNumber(minNumber, maxNumber);

                if (number < magicNumber)
                {
                    Console.WriteLine("Magic number is bigger");
                }
                else if (number > magicNumber)
                {
                    Console.WriteLine("Magic number is less big");
                }
                else
                {
                    Console.WriteLine("Congrats, you found the magic number!");
                    break;
                }
                // credits--;
            }

            if (number != magicNumber)
            {
                Console.WriteLine("Sorry, magic number was " + magicNumber + "!");
            }
        }
    }
}