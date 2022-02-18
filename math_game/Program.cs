using System;

namespace math_game
{
    class Program
    {
        private enum EOperator
        {
            Addition = 1,
            Multiplication = 2,
            Subtraction = 3,
            Division = 4
        }

        private static bool AskQuestion(int min, int max)
        {
            var rand = new Random();

            var responseInt = 0;
            while (true)
            {
                var a = rand.Next(min, max + 1);
                var b = rand.Next(min, max + 1);
                var o = (EOperator) rand.Next(1, 5);
                int awaitedResult;

                switch (o)
                {
                    case EOperator.Addition:
                        Console.WriteLine($"{a} + {b} = ");
                        awaitedResult = a + b;
                        break;
                    case EOperator.Multiplication:
                        Console.WriteLine($"{a} x {b} = ");
                        awaitedResult = a * b;
                        break;
                    case EOperator.Subtraction:
                        Console.WriteLine($"{a} - {b} = ");
                        awaitedResult = a - b;
                        break;
                    case EOperator.Division:
                        Console.WriteLine($"{a} : {b} = ");
                        awaitedResult = a / b;
                        break;
                    default:
                        Console.WriteLine("ERROR: Unknown operator");
                        return false;
                }

                var response = Console.ReadLine();
                try
                {
                    responseInt = int.Parse(response);
                    if (responseInt == awaitedResult)
                    {
                        return true;
                    }

                    return false;
                }
                catch
                {
                    Console.WriteLine("ERROR: You must enter a number");
                }
            }
        }

        private static void Main(string[] args)
        {
            const int minNumber = 1;
            const int maxNumber = 10;
            const int questionsNumber = 5;

            var points = 0;

            for (var i = 0; i < questionsNumber; i++)
            {
                Console.WriteLine($"Question {i + 1}/{questionsNumber}");
                var goodAnswer = AskQuestion(minNumber, maxNumber);
                if (goodAnswer)
                {
                    Console.WriteLine("Good answer!");
                    points++;
                }
                else
                {
                    Console.WriteLine("Wrong answer!");
                }

                Console.WriteLine();
            }

            Console.WriteLine($"Your score: {points}/{questionsNumber}");

            const int averageScore = questionsNumber / 2;

            switch (points)
            {
                case questionsNumber:
                    Console.WriteLine("Wonderful!");
                    break;
                case 0:
                    Console.WriteLine("You need to study some math I think!");
                    break;
                default:
                {
                    Console.WriteLine(points > averageScore ? "Not bad!" : "You can do better!");
                    break;
                }
            }
        }
    }
}