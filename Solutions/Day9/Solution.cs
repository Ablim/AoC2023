namespace Solutions.Day9
{
    public static class Solution
    {
        public static int Day => 9;

        public static string SolvePart1(string[] rows)
        {
            return rows
                .Select(Predict)
                .Sum()
                .ToString();
        }

        private static int Predict(string row)
        {
            var original = row
                .Split(' ')
                .Select(int.Parse)
                .ToArray();
            var all = new List<int[]>
            {
                original
            };

            do
            {
                var next = all.Last().Diffs().ToArray();
                all.Add(next);
            } while (all.Last().Any(x => x != 0));

            var prediction = 0;
            
            for (var i = all.Count - 1; i >= 0; i--)
            {
                if (all[i].Any())
                    prediction += all[i].Last();
            }
            
            return prediction;
        }

        private static int[] Diffs(this int[] source)
        {
            var target = new List<int>();

            for (var i = 0; i < source.Length - 1; i++)
            {
                var left = source[i];
                var right = source[i + 1];
                target.Add(right - left);
            }
            
            return target.ToArray();
        }
        
        public static string SolvePart2(string[] rows)
        {
            return "";
        }
    }
}