using System;
using System.Collections.Generic;
using System.Linq;
using AdventCode.Shared;

namespace AdventCode6
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var input = AdventCodeHelpers.ReadInputFile();
            var groups = ParseInput(input).ToArray();
            
            Console.WriteLine(groups.Select(GetQuestionCount).Sum());
            Console.WriteLine(groups.Select(GetQuestionCountWhereEveryoneAnswered).Sum());
        }

        private static int GetQuestionCount(IEnumerable<string> group) =>
            group.SelectMany(x => x.ToCharArray())
                .Distinct()
                .Count();

        private static int GetQuestionCountWhereEveryoneAnswered(IEnumerable<string> group)
        {
            var charGroups = group
                .Select(x => x.ToCharArray())
                .ToList();

            return charGroups
                .SelectMany(x => x)
                .Distinct()
                .Count(x => charGroups.All(y => y.Contains(x)));
        }
        
        // NB: Must be a better way of doing this in LINQ or something...
        private static IEnumerable<string[]> ParseInput(IEnumerable<string> input)
        {
            var groups = new List<string[]>();
            var currentList = new List<string>();
            
            using var enumerator = input.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!string.IsNullOrEmpty(enumerator.Current))
                {
                    currentList.Add(enumerator.Current);
                }
                else
                {
                    groups.Add(currentList.ToArray());
                    currentList = new List<string>();
                }
            }
            
            groups.Add(currentList.ToArray());

            return groups;
        }
    }
}