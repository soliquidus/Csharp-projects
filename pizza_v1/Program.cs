using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pizza_v1
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
        protected string Name;
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
        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var pizzaList = new List<Pizza>()
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
                new CustomPizza(),
                new CustomPizza()
            };

            // pizzaList = pizzaList.Where(p => p.Vegetarian).ToList();
            // pizzaList = pizzaList.Where(p => p.ContainsIngredient("tomato")).ToList();
            // pizzaList = pizzaList.OrderBy(p => p.Price).ToList();
            
            foreach (var pizza in pizzaList)
            {
                pizza.Display();
            }
        }
    }
}