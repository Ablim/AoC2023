namespace Solutions.Day21
{
    public static class Solution
    {
        public static int Day => 21;
        
        public static string SolvePart1(string[] rows, int steps = 64)
        {
            var map = ParseMap(rows);
            return map.Walk(steps).ToString();
        }

        private static char[][] ParseMap(string[] rows)
        {
            return rows.Select(r => r.ToCharArray()).ToArray();
        }

        private static int Walk(this char[][] map, int steps)
        {
            // Memoize
            var lookup = new HashSet<string>();
            var visited = new HashSet<(int row, int col)>();
            DistinctVisits(lookup, map, map.FindStart(), steps, visited);
            return visited.Count;
        }
        
        private static int Walk2(this char[][] map, int steps)
        {
            // Memoize
            // Recursion => Stack overflow
            var lookup = new HashSet<string>();
            var visited = new HashSet<(int row, int col)>();
            DistinctVisits(lookup, map, map.FindStart(), steps, visited);
            return visited.Count;
        }

        private static void DistinctVisits(HashSet<string> lookup, char[][] map, (int row, int col) position, 
            int steps, HashSet<(int row, int col)> visited)
        {
            if (steps == 0)
            {
                visited.Add(position);
                return;
            }

            var key = $"{position.row}_{position.col}_{steps}";

            if (lookup.Contains(key))
                return;

            if (map.CanGoUp(position))
                DistinctVisits(lookup, map, (position.row - 1, position.col), steps - 1, visited);
            if (map.CanGoRight(position))
                DistinctVisits(lookup, map, (position.row, position.col + 1), steps - 1, visited);
            if (map.CanGoDown(position))
                DistinctVisits(lookup, map, (position.row + 1, position.col), steps - 1, visited);
            if (map.CanGoLeft(position))
                DistinctVisits(lookup, map, (position.row, position.col - 1), steps - 1, visited);

            lookup.Add(key);
        }

        private static (int row, int col) FindStart(this char[][] map)
        {
            for (var row = 0; row < map.Length; row++)
            {
                for (var col = 0; col < map[0].Length; col++)
                {
                    if (map[row][col] == 'S')
                        return (row, col);
                }
            }

            return (0, 0);
        }

        private static bool CanGoUp(this char[][] map, (int row, int col) position) => 
            position.row - 1 >= 0 
            && map[position.row - 1][position.col] != '#';
            
        private static bool CanGoDown(this char[][] map, (int row, int col) position) => 
            position.row + 1 < map.Length 
            && map[position.row + 1][position.col] != '#';
        
        private static bool CanGoRight(this char[][] map, (int row, int col) position) => 
            position.col + 1 < map[0].Length
            && map[position.row][position.col + 1] != '#';
        
        private static bool CanGoLeft(this char[][] map, (int row, int col) position) => 
            position.col - 1 >= 0
            && map[position.row][position.col - 1] != '#';
        
        public static string SolvePart2(string[] rows, int steps = 26501365)
        {
            return "";
            var map = ParseMap(rows);
            return map.Walk2(steps).ToString();
        }
    }
}