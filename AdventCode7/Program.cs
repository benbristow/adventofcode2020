using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventCode.Shared;

namespace AdventCode7
{
    public class Bag
    {
        public Bag(string color)
        {
            Color = color;
        }
        
        public string Color { get; }
        
        public List<Bag> SupportedBags { get; } = new List<Bag>();

        public void AddDependentBags(IEnumerable<Bag> bags)
        {
            SupportedBags.AddRange(bags.Where(x => !SupportedBags.Contains(x)));
        }
    }
    
    public static class Program
    {
        public static void Main()
        {
            var bags = ParseInput(AdventCodeHelpers.ReadInputFile()).ToList();

            Console.WriteLine(Part1(bags));
            Console.WriteLine(Part2(bags));
        }

        private static int Part1(IEnumerable<Bag> bags)
        {
            static bool ContainsBag(Bag currentBag, Bag startBag, string colorToFind, bool deep = false)
            {
                if (deep && currentBag.Color == colorToFind)
                {
                    return true;
                }
                
                return currentBag.SupportedBags.Any(x => ContainsBag(x, startBag, colorToFind, true)); 
            }

            return bags.Count(x => ContainsBag(x, x, "shiny gold"));
        }

        private static int Part2(IEnumerable<Bag> bags)
        {
            throw new NotImplementedException("Need to get round to this one!");
        }

        // NB: Can this be done in one big regex??
        private static IEnumerable<Bag> ParseInput(IEnumerable<string> input)
        {
            var bags = new List<Bag>();
            
            Bag FindOrCreateBag(string color)
            {
                var bag = bags.SingleOrDefault(x => x.Color == color);
                
                if (bag == null)
                {
                    bag = new Bag(color);
                    bags.Add(bag);
                }

                return bag;
            }

            foreach (var x in input)
            {
                var currentBagMatch = Regex.Match(x, @"(.+) bags contain");
                var dependencyMatches = Regex.Matches(x, @"(\d) (.+?) bag");

                var bag = FindOrCreateBag(currentBagMatch.Groups[1].Value);
                bag.AddDependentBags(dependencyMatches.Select(y => FindOrCreateBag(y.Groups[2].Value)));
            }
            
            return bags;
        }
    }
}