namespace Solutions.Day5
{
    public static class Solution
    {
        public static int Day => 5;

        public static string SolvePart1(string[] rows)
        {
            var seeds = ParseSeeds(rows[0]);
            var maps = ParseMaps(rows);

            return seeds
                .Select(s => Find(s, maps["seed-to-soil"]))
                .Select(s => Find(s, maps["soil-to-fertilizer"]))
                .Select(s => Find(s, maps["fertilizer-to-water"]))
                .Select(s => Find(s, maps["water-to-light"]))
                .Select(s => Find(s, maps["light-to-temperature"]))
                .Select(s => Find(s, maps["temperature-to-humidity"]))
                .Select(s => Find(s, maps["humidity-to-location"]))
                .Min()
                .ToString();
        }

        private static Dictionary<string, List<(long dstStart, long srcStart, long length)>> ParseMaps(string[] rows)
        {
            var result = new Dictionary<string, List<(long, long, long)>>();
            var key = "";
            var values = new List<(long, long, long)>();
            
            foreach (var row in rows.Skip(2))
            {
                if (row.Contains("map:"))
                {
                    key = row.Split(' ')[0];
                    values = new List<(long, long, long)>();
                    continue;
                }

                if (string.IsNullOrEmpty(row))
                {
                    result.Add(key, values);
                    continue;
                }
                
                var numbers = row
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToArray();
                if (numbers.Any())
                {
                    values.Add((numbers[0], numbers[1], numbers[2]));
                }
            }
            
            result.Add(key, values);
            
            return result;
        }

        private static long[] ParseSeeds(string row)
        {
            return row
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(long.Parse)
                .ToArray();
        }
        
        private static List<(long start, long length)> ParseSeeds2(string row)
        {
            var numbers = row
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(long.Parse)
                .ToArray();

            var result = new List<(long start, long length)>();

            for (var i = 0; i < numbers.Length; i += 2)
            {
                result.Add((numbers[i], numbers[i + 1]));
            }

            return result;
        }

        private static long Find(long key, List<(long dstStart, long srcStart, long length)> intervals)
        {
            foreach (var interval in intervals)
            {
                if (key >= interval.srcStart && key < interval.srcStart + interval.length)
                {
                    var diff = key - interval.srcStart;
                    return interval.dstStart + diff;
                }
            }
            
            return key;
        }
        
        private static long Find2(long key, List<(long dstStart, long srcStart, long length)> intervals)
        {
            foreach (var interval in intervals)
            {
                if (key >= interval.dstStart && key < interval.dstStart + interval.length)
                {
                    var diff = key - interval.dstStart;
                    return interval.srcStart + diff;
                }
            }
            
            return key;
        }

        // Flip it!
        public static string SolvePart2(string[] rows)
        {
            var seeds = ParseSeeds2(rows[0]);
            var maps = ParseMaps(rows);

            var location = 0;

            while (true)
            {
                var h = Find2(location, maps["humidity-to-location"]);
                var t = Find2(h, maps["temperature-to-humidity"]);
                var l = Find2(t, maps["light-to-temperature"]);
                var w = Find2(l, maps["water-to-light"]);
                var f = Find2(w, maps["fertilizer-to-water"]);
                var s = Find2(f, maps["soil-to-fertilizer"]);
                var seed = Find2(s, maps["seed-to-soil"]);

                if (seeds.Any(target => seed >= target.start && seed < target.start + target.length))
                    return location.ToString();

                location++;
            }
            
            return "";
        }
    }
}