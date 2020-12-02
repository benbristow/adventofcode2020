using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventCode2
{
    class Program
    {
        private struct Puzzle
        {
            public int Selector1;
            public int Selector2;
            public char Character; 
            public string Value;
        }

        static void Main(string[] args)
        {
            var passwords = ReadFile().Split(Environment.NewLine).Select(ParsePuzzle).ToList();

            Console.WriteLine(passwords.Where(IsValidPasswordPart1).Count());
            Console.WriteLine(passwords.Where(IsValidPasswordPart2).Count());
        }

        private static bool IsValidPasswordPart1(Puzzle puzzle)
        {
            var occurrences = puzzle.Value.ToCharArray().Count(x => x == puzzle.Character);

            return occurrences >= puzzle.Selector1 && occurrences <= puzzle.Selector2;
        }

        private static bool IsValidPasswordPart2(Puzzle puzzle) =>
            puzzle.Value[puzzle.Selector1 - 1] == puzzle.Character ^
            puzzle.Value[puzzle.Selector2 - 1] == puzzle.Character;

        private static Puzzle ParsePuzzle(string input)
        {
            var match = Regex.Match(input, @"(\d+)\-(\d+) ([a-z]): ([a-z]+)");

            return new Puzzle()
            {
                Selector1 = int.Parse(match.Groups[1].Value),
                Selector2 = int.Parse(match.Groups[2].Value),
                Character = match.Groups[3].Value[0],
                Value = match.Groups[4].Value
            };
        }

        private static string ReadFile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("input.txt"));

            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }
    }
}
