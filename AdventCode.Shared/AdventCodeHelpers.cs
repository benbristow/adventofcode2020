using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdventCode.Shared
{
    public static class AdventCodeHelpers
    {
        public static string[] ReadInputFile()
        {
            var assembly = Assembly.GetCallingAssembly();
            var resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("input.txt"));

            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream);

            return reader.ReadToEnd().Split(Environment.NewLine);
        }
        
        public static IEnumerable<Tuple<T, T>> GetCombinations<T>(IEnumerable<T> input)
            where T : IComparable<T>
        {
            var comparables = input as T[] ?? input.ToArray();
            return (from item in comparables
                from item2 in comparables
                where item.CompareTo(item2) > 0
                select new Tuple<T, T>(item, item2)).ToList();
        }
    }
}
