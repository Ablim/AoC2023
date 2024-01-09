namespace Solutions.Day10
{
    public static class Solution
    {
        public static int Day => 10;

        public static string SolvePart1(string[] rows)
        {
            var map = rows.Parse();
            var start = map.FindStart();
            var path = map.Explore(start);
            return (path.Count % 2 == 0 ? path.Count / 2 : path.Count / 2 + 1).ToString();
        }

        private static char[][] Parse(this string[] rows)
        {
            return rows
                .Select(row => row.ToCharArray())
                .ToArray();
        }

        private static Position FindStart(this char[][] map)
        {
            for (var row = 0; row < map.Length; row++)
            {
                for (var col = 0; col < map[0].Length; col++)
                {
                    if (map[row][col] == 'S')
                        return new Position(row, col);
                }
            }
            
            return new Position(0, 0);
        }

        private static List<Position> Explore(this char[][] map, Position start)
        {
            var initial = new List<(Position position, string direction)>
            {
                (new(start.Row - 1, start.Column), "U"),
                (new(start.Row, start.Column + 1), "R"),
                (new(start.Row + 1, start.Column), "D"), 
                (new(start.Row, start.Column - 1), "L")
            };
            
            while (initial.Any())
            {
                var loop = new List<Position>
                {
                    start
                };

                var next = initial.FirstOrDefault();
                initial.Remove(next);
                
                while (true)
                {
                    if (!next.position.IsInside(map))
                        break;

                    var pipe = map[next.position.Row][next.position.Column];
                    
                    if (pipe == 'S')
                        return loop;

                    if (next.direction == "U" && pipe is '|' or '7' or 'F')
                    {
                        loop.Add(next.position);
                        next = MapNext(next, pipe);
                    }
                    else if (next.direction == "R" && pipe is '-' or 'J' or '7')
                    {
                        loop.Add(next.position);
                        next = MapNext(next, pipe);
                    }
                    else if (next.direction == "D" && pipe is '|' or 'J' or 'L')
                    {
                        loop.Add(next.position);
                        next = MapNext(next, pipe);
                    }
                    else if (next.direction == "L" && pipe is '-' or 'F' or 'L')
                    {
                        loop.Add(next.position);
                        next = MapNext(next, pipe);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return new List<Position>();
        }
        
        private static bool IsInside(this Position position, char[][] map)
        {
            return position.Row >= 0
                   && position.Row < map.Length
                   && position.Column >= 0
                   && position.Column < map[0].Length;
        }

        private static (Position position, string direction) MapNext((Position position, string direction) current, char pipe)
        {
            return (current.direction, pipe) switch
            {
                ("U", '|') => (new Position(current.position.Row - 1, current.position.Column), "U"),
                ("U", '7') => (new Position(current.position.Row, current.position.Column - 1), "L"),
                ("U", 'F') => (new Position(current.position.Row, current.position.Column + 1), "R"),
                ("R", '-') => (new Position(current.position.Row, current.position.Column + 1), "R"),
                ("R", 'J') => (new Position(current.position.Row - 1, current.position.Column), "U"),
                ("R", '7') => (new Position(current.position.Row + 1, current.position.Column), "D"),
                ("D", '|') => (new Position(current.position.Row + 1, current.position.Column), "D"),
                ("D", 'J') => (new Position(current.position.Row, current.position.Column - 1), "L"),
                ("D", 'L') => (new Position(current.position.Row, current.position.Column + 1), "R"),
                
                ("L", '-') => (new Position(current.position.Row, current.position.Column - 1), "L"),
                ("L", 'F') => (new Position(current.position.Row + 1, current.position.Column), "D"),
                ("L", 'L') => (new Position(current.position.Row - 1, current.position.Column), "U"),
                _ => throw new InvalidOperationException()
            };
        }
        
        public static string SolvePart2(string[] rows)
        {
            return "";
        }
    }
}