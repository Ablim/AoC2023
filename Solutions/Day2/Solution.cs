namespace Solutions.Day2
{
    public static class Solution
    {
        public static int Day => 2;
        private const int MaxRed = 12;
        private const int MaxGreen = 13;
        private const int MaxBlue = 14;

        public static string SolvePart1(string[] rows) =>
            rows.Select(row => row.Split(';'))
                .Where(row => row.All(SetIsPossible))
                .Select(row => int.Parse(row[0].Split(':')[0].Split(' ')[1]))
                .Sum()
                .ToString();

        private static bool SetIsPossible(string set)
        {
            var words = set.Split(' ');

            for (var i = 0; i < words.Length; i++)
            {
                if (words[i].StartsWith("red") && int.Parse(words[i - 1]) > MaxRed)
                    return false;

                if (words[i].StartsWith("green") && int.Parse(words[i - 1]) > MaxGreen)
                    return false;

                if (words[i].StartsWith("blue") && int.Parse(words[i - 1]) > MaxBlue)
                    return false;
            }

            return true;
        }

        public static string SolvePart2(string[] rows) =>
            rows.Select(row => row.Split(';'))
                .ToArray()
                .Select(GetPower)
                .Sum()
                .ToString();

        private static int GetPower(string[] row)
        {
            var minRed = 0;
            var minGreen = 0;
            var minBlue = 0;

            foreach (var set in row)
            {
                var words = set.Split(' ');

                for (var i = 0; i < words.Length; i++)
                {
                    if (words[i].StartsWith("red") && int.Parse(words[i - 1]) > minRed)
                        minRed = int.Parse(words[i - 1]);

                    if (words[i].StartsWith("green") && int.Parse(words[i - 1]) > minGreen)
                        minGreen = int.Parse(words[i - 1]);

                    if (words[i].StartsWith("blue") && int.Parse(words[i - 1]) > minBlue)
                        minBlue = int.Parse(words[i - 1]);
                }
            }

            return minRed * minGreen * minBlue;
        }
    }
}