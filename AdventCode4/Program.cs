using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventCode.Shared;

namespace AdventCode4
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var input = AdventCodeHelpers.ReadInputFile();
            var passports = ParseInput(input);
            
            Console.WriteLine(Part1(passports));
            Console.WriteLine(Part2(passports));
        }

        private static int Part1(IEnumerable<Dictionary<string, string>> passports) =>
            passports.Count(HasAllRequiredFields);

        private static int Part2(IEnumerable<Dictionary<string, string>> passports)
        {
            return passports
                .Where(HasAllRequiredFields)
                .Count(x =>
                {
                    // Birth year
                    var birthYear = int.Parse(x["byr"]);
                    if (birthYear < 1920 || birthYear > 2002)
                    {
                        return false;
                    }

                    // Issue year
                    var issueYear = int.Parse(x["iyr"]);
                    if (issueYear < 2010 || issueYear > 2020)
                    {
                        return false;
                    }

                    // Expiration Year
                    var expirationYear = int.Parse(x["eyr"]);
                    if (expirationYear < 2020 || expirationYear > 2030)
                    {
                        return false;
                    }

                    // Height
                    var heightMatch = Regex.Match(x["hgt"], @"^(\d+)(in|cm)$");
                    if (!heightMatch.Success)
                    {
                        return false;
                    }

                    var heightValue = int.Parse(heightMatch.Groups[1].Value);
                    var heightUnits = heightMatch.Groups[2].Value;
                    switch (heightUnits)
                    {
                        case "cm" when (heightValue < 150 || heightValue > 193):
                        case "in" when (heightValue < 59 || heightValue > 76):
                            return false;
                    }
                    
                    // Hair color
                    var hairColorMatch = Regex.Match(x["hcl"], @"^\#[a-f0-9]{6}$");
                    if (!hairColorMatch.Success)
                    {
                        return false;
                    }
                    
                    // Eye color
                    var validEyeColors = new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
                    if (!validEyeColors.Contains(x["ecl"]))
                    {
                        return false;
                    }
                     
                    // Passport ID
                    var passportIdMatch = Regex.Match(x["pid"], @"^\d{9}$");
                    return passportIdMatch.Success;
                });
        }
        
        // NB: Must be a better way of doing this in LINQ or something...
        private static IEnumerable<Dictionary<string, string>> ParseInput(IEnumerable<string> input)
        {
            var passportStrings = new List<string>();
            var currentPassport = "";

            using var enumerator = input.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!string.IsNullOrEmpty(enumerator.Current))
                {
                    currentPassport += enumerator.Current + " ";
                }
                else
                {
                    passportStrings.Add(currentPassport.TrimEnd());
                    currentPassport = string.Empty;
                }
            }
            
            if (currentPassport != string.Empty)
            {
                // Add final password if we're at the end...
                passportStrings.Add(currentPassport.TrimEnd());
            }

            return passportStrings.Select(ParsePassportString).ToList();
        }

        private static Dictionary<string, string> ParsePassportString(string passportString)
        {
            var matches = Regex.Matches(passportString, @"([a-z]+)\:(\S+)\s?");

            var dictionary = new Dictionary<string, string>();
            foreach (Match x in matches)
            {
                dictionary.Add(x.Groups[1].Value, x.Groups[2].Value);
            }

            return dictionary;
        }

        private static bool HasAllRequiredFields(Dictionary<string, string> passport)
        {
            var requiredFields = new[] {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};

            return requiredFields.All(x => passport.Keys.Contains(x));
        }
    }
}