namespace Solutions.Day3
{
    public static class Solution
    {
        public static int Day => 3;
        private static char[][] _map = Array.Empty<char[]>();
        private static readonly Dictionary<(int row, int col), int> Lookup = new();
        private static readonly Dictionary<int, List<Position>> ReverseLookup = new();
        
        public static string SolvePart1(string[] rows)
        {
            _map = rows
                .Select(row => row.ToCharArray())
                .ToArray();

            Parse();

            var adjacent = new List<int[]>();
            
            for (var row = 0; row < _map.Length; row++)
            {
                for (var col = 0; col < _map[0].Length; col++)
                {
                    var character = _map[row][col];

                    if (character != '.' && (character < 48 || character > 57))
                    {
                        adjacent.Add(GetAdjacent(row, col));
                    }
                }
            }

            return adjacent
                .SelectMany(x => x)
                .Sum()
                .ToString();
        }

        private static void Parse()
        {
            for (var row = 0; row < _map.Length; row++)
            {
                var numParts = new List<char>();
                var indices = new List<(int row, int col)>();
                
                for (var col = 0; col < _map[0].Length; col++)
                {
                    var character = _map[row][col];

                    if (character >= 48 && character <= 57)
                    {
                        numParts.Add(character);
                        indices.Add((row, col));
                    }
                    else if (numParts.Any())
                    {
                        AddToLookups(numParts, indices);
                        numParts.Clear();
                        indices.Clear();
                    }
                }
                
                if (numParts.Any())
                    AddToLookups(numParts, indices);
            }
        }

        private static void AddToLookups(List<char> numParts, List<(int row, int col)> indices)
        {
            var number = int.Parse(new string(numParts.ToArray()));

            foreach (var index in indices)
            {
                Lookup.Add(index, number);
            }

            if (ReverseLookup.ContainsKey(number))
            {
                ReverseLookup[number].Add(new Position
                {
                    Indices = indices.ToArray()
                });
            }
            else
            {
                ReverseLookup[number] = new List<Position>
                {
                    new()
                    {
                        Indices = indices.ToArray()
                    }
                };
            }
        }
        
        private static int[] GetAdjacent(int row, int col)
        {
            var result = new Dictionary<Position, int>();
            
            for (var i = row - 1; i <= row + 1; i++)
            {
                for (var j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < _map.Length && j >= 0 && j < _map[0].Length)
                    {
                        if (Lookup.ContainsKey((i, j)))
                        {
                            var number = Lookup[(i, j)];
                            var position = ReverseLookup[number].Single(p => p.Indices.Contains((i, j)));

                            result.TryAdd(position, number);
                        }
                    }
                }
            }

            return result.Values.ToArray();
        }
        
        private static int GetGearRatio(int row, int col)
        {
            var result = new Dictionary<Position, int>();
            
            for (var i = row - 1; i <= row + 1; i++)
            {
                for (var j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < _map.Length && j >= 0 && j < _map[0].Length)
                    {
                        if (Lookup.ContainsKey((i, j)))
                        {
                            var number = Lookup[(i, j)];
                            var position = ReverseLookup[number].Single(p => p.Indices.Contains((i, j)));

                            result.TryAdd(position, number);
                        }
                    }
                }
            }

            return result.Count == 2 ? result.Values.Aggregate((x, y) => x * y) : 0;
        }
        
        public static string SolvePart2(string[] rows)
        {
            if (!_map.Any())
                _map = rows
                    .Select(row => row.ToCharArray())
                    .ToArray();
            
            if (!Lookup.Any())
                Parse();
            
            var ratios = new List<int>();
            
            for (var row = 0; row < _map.Length; row++)
            {
                for (var col = 0; col < _map[0].Length; col++)
                {
                    var character = _map[row][col];

                    if (character == '*')
                    {
                        ratios.Add(GetGearRatio(row, col));
                    }
                }
            }

            return ratios
                .Sum()
                .ToString();
        }
    }
}