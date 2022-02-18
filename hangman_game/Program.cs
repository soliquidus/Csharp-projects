using System;
using System.Collections.Generic;
using System.IO;

namespace hangman_game
{
    class Program
    {
        private static void DisplayWord(string word, List<char> characters)
        {
            for (var i = 0; i < word.Length; i++)
            {
                var character = word[i];
                if (characters.Contains(character))
                {
                    Console.Write(character + " ");
                }
                else
                {
                    Console.Write("_ ");
                }
            }

            Console.WriteLine();
        }

        private static bool AllGuessed(string word, List<char> characters)
        {
            foreach (var character in characters)
            {
                word = word.Replace(character.ToString(), "");
            }

            if (word.Length == 0)
            {
                return true;
            }

            return false;
        }

        private static char AskCharacter(string message = "Enter a letter: ")
        {
            while (true)
            {
                Console.WriteLine(message);
                var res = Console.ReadLine();
                if (res.Length == 1)
                {
                    res = res.ToUpper();
                    return res[0];
                }

                Console.WriteLine("ERROR: You must enter one letter");
            }
        }

        private static void GuessWord(string word)
        {
            var guessedCharacters = new List<char>();
            var excludedCharacters = new List<char>();
            const int turns = 6;
            var turnsLeft = turns;

            Console.WriteLine(HangedAscii.Title);

            while (turnsLeft > 0)
            {
                Console.WriteLine(HangedAscii.Hanged[turns - turnsLeft]);
                Console.WriteLine();

                DisplayWord(word, guessedCharacters);
                Console.WriteLine();
                var character = AskCharacter();
                Console.Clear();

                if (word.Contains(character))
                {
                    Console.WriteLine("This letter is in the word");
                    guessedCharacters.Add(character);
                    if (AllGuessed(word, guessedCharacters))
                    {
                        break;
                    }
                }
                else
                {
                    if (!excludedCharacters.Contains(character))
                    {
                        turnsLeft--;
                        excludedCharacters.Add(character);
                    }

                    Console.WriteLine("Turns left: " + turnsLeft);
                }

                if (excludedCharacters.Count > 0)
                {
                    Console.WriteLine(
                        "Word doesn't contain the following letters: " + string.Join(", ", excludedCharacters));
                }

                Console.WriteLine();
            }

            Console.WriteLine(HangedAscii.Hanged[turns - turnsLeft]);

            if (turnsLeft == 0)
            {
                Console.WriteLine(HangedAscii.Loose);
                Console.WriteLine("The word was: " + word);
            }
            else
            {
                DisplayWord(word, guessedCharacters);
                Console.WriteLine();

                Console.WriteLine(HangedAscii.Win);
            }
        }

        private static string[] LoadWords(string fileName)
        {
            try
            {
                return File.ReadAllLines(fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while reading file: {fileName} ({e})");
                throw;
            }
        }

        private static bool Replay()
        {
            while (true)
            {
                var res = AskCharacter("Want to play again? (y/n)");
                switch (res)
                {
                    case 'y':
                    case 'Y':
                        return true;
                    case 'n':
                    case 'N':
                        return false;
                    default:
                        Console.WriteLine("Error: You have to answer with y or n");
                        continue;
                }
            }
        }

        private static void Main(string[] args)
        {
            var words = LoadWords("words.txt");
            if (words == null || words.Length == 0)
            {
                Console.WriteLine("No words were found");
            }
            else
            {
                while (true)
                {
                    var r = new Random();
                    var i = r.Next(words.Length);
                    var word = words[i].Trim().ToUpper();
                    GuessWord(word);
                    if (!Replay())
                    {
                        break;
                    }
                    Console.Clear();
                }

                Console.WriteLine("Thanks for playing and see you soon!");
            }
        }
    }
}