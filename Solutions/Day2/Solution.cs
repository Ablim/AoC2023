namespace Solutions.Day2
{
    public static class Solution
    {
        public static int Day => 2;
        private const int MaxRed = 12;
        private const int MaxGreen = 13;
        private const int MaxBlue = 14;

        public static string SolvePart1(string[] rows)
        {
            var parts = rows
                .Select(row => row.Split(';'))
                .ToArray();
            var possibleGames = new List<int>();
            
            foreach (var row in parts)
            {
                var game = int.Parse(row[0].Split(':')[0].Split(' ')[1]);
                var possible = true;

                foreach (var set in row)
                {
                    if (!possible)
                        break;
                    
                    var words = set.Split(' ');

                    for (var i = 0; i < words.Length; i++)
                    {
                        if (words[i].StartsWith("red") && int.Parse(words[i - 1]) > MaxRed)
                        {
                            possible = false;
                            break;
                        }

                        if (words[i].StartsWith("green") && int.Parse(words[i - 1]) > MaxGreen)
                        {
                            possible = false;
                            break;
                        }

                        if (words[i].StartsWith("blue") && int.Parse(words[i - 1]) > MaxBlue)
                        {
                            possible = false;
                            break;
                        }
                    }
                }
                
                if (possible)
                    possibleGames.Add(game);
            }
            
            return possibleGames
                .Sum()
                .ToString();
        }

        public static string SolvePart2(string[] rows)
        {
            var parts = rows
                .Select(row => row.Split(';'))
                .ToArray();
            var powers = new List<int>();
            
            foreach (var row in parts)
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
                        {
                            minRed = int.Parse(words[i - 1]);
                        }

                        if (words[i].StartsWith("green") && int.Parse(words[i - 1]) > minGreen)
                        {
                            minGreen = int.Parse(words[i - 1]);
                        }

                        if (words[i].StartsWith("blue") && int.Parse(words[i - 1]) > minBlue)
                        {
                            minBlue = int.Parse(words[i - 1]);
                        }
                    }
                }
                
                powers.Add(minRed * minGreen * minBlue);
            }
            
            return powers
                .Sum()
                .ToString();
        }
    }
}