using System;
using Formation;

namespace password_generator
{
    class Program
    {
        private static void Main(string[] args)
        {
            const int passwordNumbers = 10;

            var passwordLength = utils.AskNumberNotNullPositif("Password length: ");
            Console.WriteLine();
            var alphabetChoice = utils.AskNumberBetween("What kind of password do you want?\n" +
                                                        "1 - Only lower case letters\n" +
                                                        "2 - Lower and upper case letters\n" +
                                                        "3 - Letters and numbers\n" +
                                                        "4 - Letters, numbers and special characters\n" +
                                                        "Your choice: ", 1, 4);

            const string toLower = "abcdefghijklmnopqrzstuvwxyz";
            const string numbers = "123456789";
            const string specialCharacters = "#&*@";
            var toUpper = toLower.ToUpper();

            var rand = new Random();

            var alphabet = alphabetChoice switch
            {
                1 => toLower,
                2 => toLower + toUpper,
                3 => toLower + toUpper + numbers,
                _ => toLower + toUpper + numbers + specialCharacters
            };

            var alphabetLength = alphabet.Length;
            int j;
            
            for (j = 1; j <= passwordNumbers; j++)
            {
                var password = "";
                for (var i = 0; i < passwordLength; i++)
                {
                    var index = rand.Next(0, alphabetLength);
                    password += alphabet[index];
                }

                Console.WriteLine($"Password n°{j}: {password}");
            }
        }
    }
}