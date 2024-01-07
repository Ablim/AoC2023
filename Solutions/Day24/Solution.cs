namespace Solutions.Day24
{
    public static class Solution
    {
        public static int Day => 24;
        
        public static string SolvePart1(string[] rows, long min = 200000000000000, long max = 400000000000000)
        {
            var lines = rows.Parse();
            var intersections = new List<Point>();

            for (var a = 0; a < lines.Count; a++)
            {
                for (var b = a + 1; b < lines.Count; b++)
                {
                    var lineA = lines[a];
                    var lineB = lines[b];
                    var intersection = Intersects2D(min, max, lineA.position, lineA.velocity, lineB.position, lineB.velocity);
                    if (intersection != null)
                        intersections.Add(intersection.Value);
                }
            }
            
            return intersections.Count.ToString();
        }

        private static List<(Point position, Vector velocity)> Parse(this string[] rows)
        {
            return rows
                .Select(row => row.Split('@'))
                .Select(row => (
                    point: row[0]
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(double.Parse)
                        .ToArray(),
                    vector: row[1]
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(double.Parse)
                        .ToArray()))
                .Select(row => (
                    new Point(row.point[0], row.point[1], row.point[2]),
                    new Vector(row.vector[0], row.vector[1], row.vector[2])))
                .ToList();
        }

        private static Point? Intersects2D(long min, long max, Point pA, Vector vA, Point pB, Vector vB)
        {
            var kA = vA.Y / vA.X;
            var mA = pA.Y - kA * pA.X;
            var kB = vB.Y / vB.X;
            var mB = pB.Y - kB * pB.X;
            
            var x = (mB - mA) / (kA - kB);
            var y = kA * x + mA;

            var intersection = new Point(x, y, 0);
            return intersection.IsWithinBounds(min, max) 
                   && intersection.IsInFuture2D(pA, vA) 
                   && intersection.IsInFuture2D(pB, vB) 
                ? intersection 
                : null;
        }

        private static bool IsInFuture2D(this Point point, Point position, Vector direction)
        {
            var currentDistance = Math.Sqrt(Math.Pow(point.X - position.X, 2) + Math.Pow(point.Y - position.Y, 2));
            var nextPosition = new Point(position.X + direction.X, position.Y + direction.Y, 0);
            var nextDistance = Math.Sqrt(Math.Pow(point.X - nextPosition.X, 2) + Math.Pow(point.Y - nextPosition.Y, 2));
            return nextDistance < currentDistance;
        }

        private static bool IsWithinBounds(this Point point, long min, long max) =>
            point.X >= min && point.X <= max && point.Y >= min && point.Y <= max;
        
        public static string SolvePart2(string[] rows)
        {
            return "";
        }
    }
}