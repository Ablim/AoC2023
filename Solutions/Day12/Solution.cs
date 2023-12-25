namespace Solutions.Day12
{
    public static class Solution
    {
        public static int Day => 12;

        public static string SolvePart1(string[] rows)
        {
            return rows
                .Select(row => row.Split(' '))
                .Select(row => (springs: row[0], broken: row[1].Split(',').Select(int.Parse).ToArray()))
                .Select(CountArrangements)
                .Sum()
                .ToString();
        }

        private static int CountArrangements((string springs, int[] broken) row)
        {
            return BuildCandidates(row).Distinct().Count(r => Matches(r, row.broken));
        }
        
        private static int CountArrangements2((string springs, int[] broken) row)
        {
            var candidates = BuildCandidates2(row);
            var totalCount = candidates.Length;
            var distinctCount = candidates.Distinct().Count(r => Matches(r, row.broken));
            Console.WriteLine($"Total: {totalCount}");
            Console.WriteLine($"Distinct: {distinctCount}");
            Console.WriteLine();
            return distinctCount;
        }
        
        private static string[] BuildCandidates((string springs, int[] broken) row)
        {
            var master = row.springs.ToCharArray();
            var candidates = new List<(char[] arrangement, int index)>();
            
            var seed = new char[master.Length];
            Array.Copy(master, seed, master.Length);
            candidates.Add((seed, - 1));

            foreach (var b in row.broken)
            {
                var moreCandidates = new List<(char[] arrangement, int index)>();
                
                foreach (var c in candidates)
                {
                    for (var i = c.index + 1; i < c.arrangement.Length; i++)
                    {
                        if (c.arrangement.HasRoomAt(i, b))
                        {
                            var candidate = new char[c.arrangement.Length];
                            Array.Copy(c.arrangement, candidate, c.arrangement.Length);
                            candidate.InsertAt(i, b);
                            moreCandidates.Add((candidate, i));
                        }
                    }
                }
                
                candidates = moreCandidates;
            }
            
            return candidates
                .Select(c => new string(c.arrangement).Replace('?', '.'))
                .ToArray();
        }
        
        private static string[] BuildCandidates2((string springs, int[] broken) row)
        {
            var candidates = new List<(string arrangement, int index)>();
            candidates.Add((row.springs, - 1));

            foreach (var b in row.broken)
            {
                var moreCandidates = new List<(string arrangement, int index)>();
                
                foreach (var c in candidates)
                {
                    for (var i = c.index + 1; i < c.arrangement.Length; i++)
                    {
                        if (c.arrangement.ToCharArray().HasRoomAt(i, b))
                        {
                            var candidate = c.arrangement.ToCharArray();
                            candidate.InsertAt(i, b);
                            moreCandidates.Add(( new string(candidate), i));
                        }
                    }
                }
                
                candidates = moreCandidates;
            }
            
            return candidates
                .Select(c => c.arrangement.Replace('?', '.'))
                .ToArray();
        }

        private static bool Matches(this string row, int[] sequence)
        {
            var parts = row.Split('.', StringSplitOptions.RemoveEmptyEntries);
            return parts.Length == sequence.Length;
        }

        private static bool HasRoomAt(this char[] source, int index, int length)
        {
            if (index + length > source.Length)
                return false;
            
            if (index + length < source.Length && source[index + length] == '#')
                return false;

            if (index > 0 && source[index - 1] == '#')
                return false;
            
            for (var i = index; i < index + length; i++)
            {
                if (source[i] == '.')
                    return false;
            }

            return true;
        }
        
        private static void InsertAt(this char[] source, int index, int length)
        {
            for (var i = index; i < index + length; i++)
            {
                if (source[i] == '?')
                    source[i] = '#';
            }
            
            if (index + length < source.Length && source[index + length] == '?')
                source[index + length] = '.';
            
            if (index > 0 && source[index - 1] == '?')
                source[index - 1] = '.';
        }

        private static (string springs, string broken) Multiply(string[] source)
        {
            var springs = new List<string>();
            var broken = new List<string>();
            
            for (var i = 0; i < 5; i++)
            {
                springs.Add(source[0]);
                broken.Add(source[1]);
            }
            
            return (string.Join('?', springs), string.Join(',', broken));
        }
        
        public static string SolvePart2(string[] rows)
        {
            return rows
                .Select(row => row.Split(' '))
                .Select(Multiply)
                .Select(row => (row.springs, row.broken.Split(',').Select(int.Parse).ToArray()))
                .Select(CountArrangements2)
                .Sum()
                .ToString();
        }
    }
}