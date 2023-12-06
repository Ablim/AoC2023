namespace Solutions.Day6
{
    public static class Solution
    {
        public static int Day => 6;

        public static string SolvePart1(string[] rows)
        {
            var availableTimes = rows[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(int.Parse);
            var distanceRecords = rows[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(int.Parse);
            var races = availableTimes.Zip(distanceRecords).ToArray();

            return races
                .Select(Race)
                .Select(x => x.Count())
                .Aggregate((x, y) => x * y)
                .ToString();
        }

        private static IEnumerable<int> Race((int availableTime, int distanceRecord) race)
        {
            for (var speed = 0; speed < race.availableTime; speed++)
            {
                var distance = speed * (race.availableTime - speed);

                if (distance > race.distanceRecord)
                    yield return distance;
            }
        }
        
        private static IEnumerable<long> Race((long availableTime, long distanceRecord) race)
        {
            for (var speed = 0; speed < race.availableTime; speed++)
            {
                var distance = speed * (race.availableTime - speed);

                if (distance > race.distanceRecord)
                    yield return distance;
            }
        }
        
        public static string SolvePart2(string[] rows)
        {
            var availableTime = rows[0]
                .Split(':')[1]
                .Replace(" ", "");
            var availableTime2 = long.Parse(availableTime);
            var distanceRecord = rows[1]
                .Split(':')[1]
                .Replace(" ", "");
            var distanceRecord2 = long.Parse(distanceRecord);

            return Race((availableTime2, distanceRecord2))
                .Count()
                .ToString();
        }
    }
}