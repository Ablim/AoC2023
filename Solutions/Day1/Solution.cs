namespace Solutions.Day1
{
    public static class Solution
    {
        public static int Day => 1;

        public static string SolvePart1(string[] rows)
        {
            return rows
                .Select(row => $"{FirstNum(row)}{LastNum(row)}")
                .Select(int.Parse)
                .Sum()
                .ToString();
        }

        public static string SolvePart2(string[] rows)
        {
            return rows
                .Select(Transform)
                .Select(row => $"{FirstNum(row)}{LastNum(row)}")
                .Select(int.Parse)
                .Sum()
                .ToString();
        }

        private static int FirstNum(string row)
        {
            var first = row
                .ToCharArray()
                .FirstOrDefault(c => int.TryParse(c.ToString(), out _))
                .ToString(); 
            return int.Parse(first);
        }

        private static int LastNum(string row) =>
            FirstNum(new string(row.ToCharArray().Reverse().ToArray()));

        private static string Transform(string row)
        {
            var newRow = row.ToCharArray();
            var replacements = new Dictionary<int, char>();

            foreach (var number in Numbers)
            {
                var temp = row;

                while (true)
                {
                    var index = temp.IndexOf(number, StringComparison.OrdinalIgnoreCase);
                    if (index == -1)
                        break;
                    
                    replacements.Add(index, Map(number));
                    var temp2 = temp.ToCharArray();
                    temp2[index] = ' ';
                    temp = new string(temp2);
                }
            }

            foreach (var replacement in replacements)
            {
                newRow[replacement.Key] = replacement.Value;
            }
            
            return new string(newRow);
        }

        private static string[] Numbers => new[]
        {
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine",
        };

        private static char Map(string number) => number switch
        {
            "one" => '1',
            "two" => '2',
            "three" => '3',
            "four" => '4',
            "five" => '5',
            "six" => '6',
            "seven" => '7',
            "eight" => '8',
            "nine" => '9',
            _ => '0'
        };
    }
}