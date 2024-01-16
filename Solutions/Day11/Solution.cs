namespace Solutions.Day11
{
    public static class Solution
    {
        public static int Day => 11;
        private static int _multiplier = 0;

        public static string SolvePart1(string[] rows, int multiplier = 2)
        {
            _multiplier = multiplier;
            var map = rows.ParseMap();
            var galaxies = map.ParseGalaxies();
            var emptyRows = map.ParseEmptyRows();
            var emptyColumns = map.ParseEmptyColumns();
            
            return Sum(galaxies, emptyRows, emptyColumns).ToString();
        }

        private static char[][] ParseMap(this string[] rows)
        {
            return rows.Select(row => row.ToCharArray()).ToArray();
        }

        private static List<Point> ParseGalaxies(this char[][] map)
        {
            var galaxies = new List<Point>();

            for (var row = 0; row < map.Length; row++)
            {
                for (var col = 0; col < map[0].Length; col++)
                {
                    if (map[row][col] == '#')
                        galaxies.Add(new Point(row, col));
                }
            }

            return galaxies;
        }

        private static HashSet<int> ParseEmptyRows(this char[][] map)
        {
            var rows = new HashSet<int>();

            for (var row = 0; row < map.Length; row++)
            {
                var empty = true;

                for (var col = 0; col < map[0].Length; col++)
                {
                    if (map[row][col] == '#')
                    {
                        empty = false;
                        break;
                    }
                }

                if (empty)
                    rows.Add(row);
            }

            return rows;
        }
        
        private static HashSet<int> ParseEmptyColumns(this char[][] map)
        {
            var columns = new HashSet<int>();

            for (var col = 0; col < map.Length; col++)
            {
                var empty = true;

                for (var row = 0; row < map[0].Length; row++)
                {
                    if (map[row][col] == '#')
                    {
                        empty = false;
                        break;
                    }
                }

                if (empty)
                    columns.Add(col);
            }

            return columns;
        }

        private static long Sum(List<Point> galaxies, HashSet<int> emptyRows, HashSet<int> emptyColumns)
        {
            var sum = 0L;
            
            for (var a = 0; a < galaxies.Count; a++)
            {
                for (var b = a + 1; b < galaxies.Count; b++)
                {
                    var galaxyA = galaxies[a];
                    var galaxyB = galaxies[b];

                    var doubleRows = Generate(Math.Min(galaxyA.Row, galaxyB.Row), Math.Max(galaxyA.Row, galaxyB.Row))
                        .Where(emptyRows.Contains)
                        .Count();
                    var doubleCols = Generate(Math.Min(galaxyA.Column, galaxyB.Column), Math.Max(galaxyA.Column, galaxyB.Column))
                        .Where(emptyColumns.Contains)
                        .Count();
                    
                    var deltaRow = Math.Abs(galaxyA.Row - galaxyB.Row) - doubleRows + doubleRows * _multiplier;
                    var deltaCol = Math.Abs(galaxyA.Column - galaxyB.Column) - doubleCols + doubleCols * _multiplier;
                    
                    sum += deltaRow + deltaCol;
                }
            }
            
            return sum;
        }

        private static List<int> Generate(int start, int end)
        {
            var result = new List<int>();

            for (var i = start; i <= end; i++)
            {
                result.Add(i);
            }

            return result;
        }

        public static string SolvePart2(string[] rows, int multiplier = 1000000)
        {
            _multiplier = multiplier;
            var map = rows.ParseMap();
            var galaxies = map.ParseGalaxies();
            var emptyRows = map.ParseEmptyRows();
            var emptyColumns = map.ParseEmptyColumns();
            
            return Sum(galaxies, emptyRows, emptyColumns).ToString();
        }
    }
}