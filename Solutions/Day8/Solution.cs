namespace Solutions.Day8
{
    public static class Solution
    {
        public static int Day => 8;

        public static string SolvePart1(string[] rows)
        {
            var directions = rows[0].ToCharArray();
            var graph = ParseGraph(rows.Skip(2).ToArray());
            var steps = 0;
            var current = "AAA";
            var end = "ZZZ";

            while (current != end)
            {
                foreach (var d in directions)
                {
                    var options = graph[current];
                    current = d == 'L' ? options[0] : options[1];
                    steps++;

                    if (current == end)
                        break;
                }
            }
            
            return steps.ToString();
        }

        private static Dictionary<string, string[]> ParseGraph(string[] rows)
        {
            return rows
                .Select(row => row
                    .Replace("=", "")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace(",", "")
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .ToDictionary(row => row[0], row => row.Skip(1).ToArray());
        }

        public static string SolvePart2(string[] rows)
        {
            var directions = rows[0].ToCharArray();
            var graph = ParseGraph(rows.Skip(2).ToArray());
            var steps = 0;
            var current = graph.Keys.Where(k => k.EndsWith("A")).ToArray();

            while (!current.All(c => c.EndsWith("Z")))
            {
                foreach (var d in directions)
                {
                    for (var i = 0; i < current.Length; i++)
                    {
                        var options = graph[current[i]];
                        current[i] = d == 'L' ? options[0] : options[1];
                    }
                    
                    steps++;
                    
                    if (current.All(c => c.EndsWith("Z")))
                       break;
                }
            }
            
            return steps.ToString();
        }
    }
}