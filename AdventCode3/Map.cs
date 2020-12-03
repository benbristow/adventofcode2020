using System;

namespace AdventCode3
{
    public class Map
    {
        private readonly string[] _lines;

        public Map(string[] lines)
        {
            _lines = lines;
        }

        public int NumberOfTrees(int right, int down)
        {
            var x = -Math.Abs(right);
            var y = -Math.Abs(down);
            var trees = 0;

            while (y < _lines.Length - 1)
            {
                x += right;
                y += down;

                var line = RenderLine(y, x);

                if (line[0] == '#')
                {
                    trees++;
                };
            }

            return trees;
        }

        private string RenderLine(int y, int x)
        {
            var line = _lines[y];

            if (x > line.Length)
            {
                x %= line.Length;
            }

            var leftPart = line[x..];
            var rightPart = line.Substring(0, x);

            return leftPart + rightPart;
        }
    }
}
