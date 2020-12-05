using System;
using System.Collections.Generic;
using System.Linq;
using AdventCode.Shared;

namespace AdventCode5
{
    public static class Program
    {
        public static void Main()
        {
            var input = AdventCodeHelpers.ReadInputFile();

            var seats = input.Select(GetSeatId).OrderBy(x => x).ToList();
            
            Console.WriteLine(Part1(seats));
            Console.WriteLine(Part2(seats));
        }

        private static int Part1(IEnumerable<int> list)
        {
            return list.Max();
        }

        private static int Part2(IReadOnlyList<int> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var a = list[i];
                var b = list[i + 1];

                if (b - a != 1)
                {
                    return a + 1;
                }
            }
            
            throw new ArgumentException("No seats available", nameof(list));
        }

        private static int GetSeatId(string input)
        {
            var rows = Enumerable.Range(0, 128).ToList();
            var columns = Enumerable.Range(0, 8).ToList();

            foreach (var x in input.ToCharArray())
            {
                switch (x)
                {
                    case 'F':
                    {
                        rows.RemoveTopHalf();
                        break;
                    }
                    case 'B':
                    {
                        rows.RemoveBottomHalf();
                        break;
                    }
                    case 'R':
                        columns.RemoveBottomHalf();
                        break;
                    case 'L':
                        columns.RemoveTopHalf();
                        break;
                }
            }

            return rows[0] * 8 + columns[0];
        }
    }
}