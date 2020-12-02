using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdventCode1
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = ReadFile();
            var numbers = file.Split(Environment.NewLine).Select(int.Parse).ToArray();

            Console.WriteLine(Part1(numbers));
            Console.WriteLine(Part2(numbers));
        }

        private static int Part1(int[] numbers)
        {
            var combinations = (from item in numbers
                from item2 in numbers
                where item < item2
                select new Tuple<int, int>(item, item2)).ToList();

            var combination = combinations.Single(x => x.Item1 + x.Item2 == 2020);

            return combination.Item1 * combination.Item2;
        }

        private static int Part2(int[] numbers)
        {
            var combinations = (
                from item in numbers
                from item2 in numbers
                from item3 in numbers
                where item < item2 && item2 < item3
                select new Tuple<int, int, int>(item, item2, item3)).ToList();

            var combination = combinations.Single(x => x.Item1 + x.Item2 + x.Item3 == 2020);

            return combination.Item1 * combination.Item2 * combination.Item3;
        }

        private static string ReadFile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("numbers.txt"));

            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream);
            
            return reader.ReadToEnd();
        }
    }
}
