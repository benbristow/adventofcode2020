using System;
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
    }
}
