using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace pizza_v2
{
    internal class CustomPizza : Pizza
    {
        private static int _customTotal = 0;

        public CustomPizza() : base("Custom", 5, false, null)
        {
            _customTotal++;
            Name = "Custom n° " + _customTotal;
            Ingredients = new List<string>();

            while (true)
            {
                Console.Write($"Enter the wanted ingredient for custom pizza n°{_customTotal} (Enter to finish): ");
                var ingredient = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(ingredient))
                {
                    break;
                }

                if (Ingredients.Contains(ingredient))
                {
                    Console.WriteLine("ERROR: You already added this ingredient");
                }
                else
                {
                    Ingredients.Add(ingredient);
                    Console.WriteLine(string.Join(", ", Ingredients));
                }

                Console.WriteLine();
            }

            Price = 5 + Ingredients.Count * 1.5f;
        }
    }

    internal class Pizza
    {
        public string Name { get; protected set; }
        public float Price { get; protected set; }
        public bool Vegetarian { get; private set; }
        public List<string> Ingredients { get; protected set; }

        public Pizza(string name, float price, bool vegetarian, List<string> ingredients)
        {
            this.Name = name;
            this.Price = price;
            this.Vegetarian = vegetarian;
            this.Ingredients = ingredients;
        }

        public void Display()
        {
            var veggie = Vegetarian ? " (V)" : "";
            var nameDisplay = FirstLetterUpperFormat(Name);
            var ingredientsDisplay = Ingredients.Select(FirstLetterUpperFormat).ToList();

            Console.WriteLine($"{nameDisplay}{veggie} - {Price} €");
            Console.WriteLine(string.Join(", ", ingredientsDisplay));
            Console.WriteLine();
        }

        private static string FirstLetterUpperFormat(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            var lower = s.ToLower();
            var upper = s.ToUpper();

            var result = upper[0] + lower[1..];

            return result;
        }

        public bool ContainsIngredient(string ingredient)
        {
            return Ingredients.Where(i => i.ToLower().Contains(ingredient)).ToList().Count > 0;
        }
    }

    class Program
    {
        private static List<Pizza> GetPizzasFromCode()
        {
            var pizzas = new List<Pizza>()
            {
                new Pizza("4 cheeses", 11.5f, true,
                    new List<string> {"cantal", "mozzarella", "goat cheese", "emmental"}),
                new Pizza("Indian", 10.5f, false,
                    new List<string> {"curry", "mozzarella", "chicken", "pepperoni", "onions", "coriander"}),
                new Pizza("Mexican", 13f, false,
                    new List<string> {"beef", "mozzarella", "cornfield", "tomatoes", "onions", "coriander"}),
                new Pizza("Margarita", 8f, true, new List<string> {"tomato sauce", "mozzarella", "parsley"}),
                new Pizza("Calzone", 12f, false, new List<string> {"tomatoes", "ham", "parsley", "onions"}),
                new Pizza("Complete", 9.5f, false, new List<string> {"ham", "eggs", "cheese"}),
                // new CustomPizza(),
                // new CustomPizza()
            };
            return pizzas;
        }

        private static List<Pizza> GetPizzasFromFile(string filename)
        {
            string json;
            try
            {
                json = File.ReadAllText(filename);
            }
            catch
            {
                Console.WriteLine("Error while trying to read file: " + filename);
                return null;
            }

            List<Pizza> pizzas;
            try
            {
                pizzas = JsonConvert.DeserializeObject<List<Pizza>>(json);
            }
            catch
            {
                Console.WriteLine("Error: JSON format is not respected");
                return null;
            }

            return pizzas;
        }

        private static void generateJsonFile(List<Pizza> pizzas, string filename)
        {
            var json = JsonConvert.SerializeObject(pizzas);
            File.WriteAllText(filename, json);
        }

        private static List<Pizza> GetPizzasFromURL(string url)
        {
            var webclient = new WebClient();
            var json = webclient.DownloadString(url);
            List<Pizza> pizzas = null;
            try
            {
                pizzas = JsonConvert.DeserializeObject<List<Pizza>>(json);
            }
            catch
            {
                Console.WriteLine("Error: JSON format is not valid");
                return null;
            }

            return pizzas;
        }

        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            const string filename = "pizzas.json";

            // var pizzas = GetPizzasFromCode();
            // generateJsonFile(pizzas, filename);
            
            var pizzas = GetPizzasFromFile(filename);

            if (pizzas == null) return;
            foreach (var pizza in pizzas)
            {
                pizza.Display();
            }
        }
    }
}