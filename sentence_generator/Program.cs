using System;
using System.Collections.Generic;

namespace sentence_generator
{
    class Program
    {
        private static string GetRandomElement(string[] a)
        {
            var r = new Random();
            var i = r.Next(a.Length);
            return a[i];
        }

        private static void Main(string[] args)
        {
            var subjects = new string[]
            {
                "A rabbit",
                "A granny",
                "A cat",
                "A snowman",
                "A snail",
                "A fea",
                "A magician",
                "A turtle"
            };

            var verbs = new string[]
            {
                "eats",
                "crushes",
                "destroys",
                "observes",
                "catches",
                "swallows",
                "cleans up",
                "fights with",
                "hooks on"
            };

            var complements = new string[]
            {
                "a tree",
                "a book",
                "the moon",
                "the sun",
                "a snake",
                "a map",
                "a giraffe",
                "the sky",
                "a swimming pool",
                "a cake",
                "a mouse",
                "a frog"
            };

            const int sentenceNumber = 100;
            var uniqueSentences = new List<string>();
            var occurrencesAvoided = 0;

            while (uniqueSentences.Count < sentenceNumber)
            {
                var subject = GetRandomElement(subjects);
                var verb = GetRandomElement(verbs);
                var complement = GetRandomElement(complements);

                var sentence = $"{subject} {verb} {complement}";

                if (!uniqueSentences.Contains(sentence))
                {
                    uniqueSentences.Add(sentence);
                }
                else
                {
                    occurrencesAvoided++;
                }
            }

            foreach (var sentence in uniqueSentences)
            {
                Console.WriteLine(sentence);
            }

            Console.WriteLine("Unique sentences: " + uniqueSentences.Count);
            Console.WriteLine("Occurrences avoided: " + occurrencesAvoided);
        }
    }
}