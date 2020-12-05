using System.Collections.Generic;

namespace AdventCode5
{
    public static class ListExtensions
    {
        public static void RemoveTopHalf<T>(this List<T> list)
        {
            var start = list.Count / 2;    
            list.RemoveRange(start, list.Count - start);
        }

        public static void RemoveBottomHalf<T>(this List<T> list)
        {
            list.RemoveRange(0, list.Count / 2);
        }
    }
}