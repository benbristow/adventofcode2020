using System;
using System.Linq;
using System.Numerics;
using AdventCode.Shared;
using AdventCode3;

namespace AdventCode
{
    public static class AdventCode3
    {
        public static void Main(string[] args)
        {
            var input = AdventCodeHelpers.ReadInputFile();
            var map = new Map(input);

            Console.WriteLine(Part1(map));
            Console.WriteLine(Part2(map));
        }

        private static int Part1(Map map) => map.NumberOfTrees(3, 1);

        private static BigInteger Part2(Map map)
        {
            var values = new[]
            {
                map.NumberOfTrees(1, 1),
                map.NumberOfTrees(3, 1),
                map.NumberOfTrees(5, 1),
                map.NumberOfTrees(7, 1),
                map.NumberOfTrees(1, 2)
            };

            return values.Aggregate<int, BigInteger>(1, (current, x) => current * x);
        }
    }   
}
