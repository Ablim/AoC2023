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
            var all = BuildCandidates(row);
            var filtered = all
                .Where(a => a.Matches(row.springs))
                .Distinct()
                .ToArray();
            return filtered.Length;
        }
        
        private static string[] BuildCandidates((string springs, int[] broken) row)
        {
            var master = row.springs.ToCharArray();
            var candidates = new List<char[]>();
            
            var seed = new char[master.Length];
            Array.Copy(master, seed, master.Length);
            candidates.Add(seed);

            foreach (var b in row.broken)
            {
                var moreCandidates = new List<char[]>();
                
                foreach (var c in candidates)
                {
                    for (var i = 0; i < c.Length; i++)
                    {
                        if (c.HasRoomAt(i, b))
                        {
                            var candidate = new char[c.Length];
                            Array.Copy(c, candidate, c.Length);
                            candidate.InsertAt(i, b);
                            moreCandidates.Add(candidate);
                        }
                    }
                }
                
                candidates = moreCandidates;
            }
            
            return candidates.Select(c => new string(c)).ToArray();
        }

        private static bool Matches(this string row, string template)
        {
            if (row.Length != template.Length)
                return false;

            var rowAsArray = row.ToCharArray();
            var templateAsArray = template.ToCharArray();

            for (var i = 0; i < rowAsArray.Length; i++)
            {
                if (rowAsArray[i] == '?')
                    return false;
                
                if (!(rowAsArray[i] == templateAsArray[i] || templateAsArray[i] == '?'))
                    return false;
            }
            
            return true;
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
        
        public static string SolvePart2(string[] rows)
        {
            return "";
        }
    }
}