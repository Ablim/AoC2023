namespace Solutions.Day13
{
    public static class Solution
    {
        public static int Day => 13;

        public static string SolvePart1(string[] rows)
        {
            var maps = rows.ParseMaps();
            var verticalFolds = maps.Select(FoldVertically);
            var leftColumns = verticalFolds
                .Where(fold => fold != null)
                .Select(fold => fold!.Value.right)
                .Sum();
            
            var horizontalFolds = maps.Select(FoldHorizontally);
            var topRows = horizontalFolds
                .Where(fold => fold != null)
                .Select(fold => fold!.Value.bottom)
                .Sum();
            
            return (leftColumns + 100 * topRows).ToString();
        }

        private static List<char[][]> ParseMaps(this string[] rows)
        {
            var result = new List<char[][]>();
            var temp = new List<string>();

            foreach (var row in rows)
            {
                if (string.IsNullOrEmpty(row))
                {
                    result.Add(temp.Select(t => t.ToCharArray()).ToArray());
                    temp.Clear();
                    continue;
                }
                
                temp.Add(row);
            }
            
            result.Add(temp.Select(t => t.ToCharArray()).ToArray());
            return result;
        }
        
        private static (int left, int right)? FoldVertically(this char[][] map)
        {
            for (var column = 0; column < map[0].Length - 1; column++)
            {
                // If we can fold all rows around this column, we have found the vertical fold location
                var foldBetween = (left: column, right: column + 1);
                var foldable = true;
                
                for (var row = 0; row < map.Length; row++)
                {
                    var left = column;
                    var right = column + 1;

                    while (left >= 0 && right < map[0].Length)
                    {
                        if (map[row][left] != map[row][right])
                        {
                            foldable = false;
                            break;
                        }

                        left--;
                        right++;
                    }

                    if (!foldable)
                        break;
                }

                if (foldable)
                    return foldBetween;
            }

            return null;
        }
        
        private static (int top, int bottom)? FoldHorizontally(this char[][] map)
        {
            for (var row = 0; row < map.Length - 1; row++)
            {
                // If we can fold all rows around this column, we have found the vertical fold location
                var foldBetween = (top: row, bottom: row + 1);
                var foldable = true;

                for (var column = 0; column < map[0].Length; column++)
                {
                    var top = row;
                    var bottom = row + 1;
                    
                    while (top >= 0 && bottom < map.Length)
                    {
                        if (map[top][column] != map[bottom][column])
                        {
                            foldable = false;
                            break;
                        }
                    
                        top--;
                        bottom++;
                    }
                    
                    if (!foldable)
                        break;
                }

                if (foldable)
                    return foldBetween;
            }

            return null;
        }
        
        public static string SolvePart2(string[] rows)
        {
            return "";
        }
    }
}