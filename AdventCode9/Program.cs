using System;
using System.Collections.Generic;
using System.Linq;
using AdventCode.Shared;

namespace AdventCode9
{
    public static class Program
    {
        private const int Preamble = 25;
        
        public static void Main()
        {
            var input = AdventCodeHelpers.ReadInputFile().Select(long.Parse).ToList();

            var part1Answer = Part1(input);
            Console.WriteLine(part1Answer);
            
            Console.WriteLine(Part2(input, part1Answer));
        }

        private static long Part1(IReadOnlyList<long> input)
        {
            for (var i = Preamble; i < input.Count; i++)
            {
                var current = input[i];
                var previousNumbers = input.Skip(i - Preamble).Take(Preamble).ToList();
                var combinations = AdventCodeHelpers.GetCombinations(previousNumbers);

                if (combinations.All(x => x.Item1 + x.Item2 != current))
                {
                    return current;
                }
            }
            
            throw new Exception("Invalid input");
        }

        private static long Part2(IReadOnlyList<long> input, long part1Answer)
        {
            for (var i = Preamble; i < input.Count; i++)
            {
                var numbers = new List<long>();
                var y = i + 1;
                long sum = 0;

                while (y < input.Count && sum < part1Answer)
                {
                    var current = input[y];
                    numbers.Add(current);

                    sum = numbers.Sum();

                    if (numbers.Count > 1 && sum == part1Answer)
                    {
                        return numbers.Min() + numbers.Max();
                    }

                    y++;
                }
            }
            
            throw new Exception("Invalid input");
        }
    }
}