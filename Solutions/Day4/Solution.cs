namespace Solutions.Day4
{
    public static class Solution
    {
        public static int Day => 4;

        public static string SolvePart1(string[] rows)
        {
            return rows
                .Select(Parse)
                .Select(pair => pair.winners.Join(pair.yours, w => w, y => y, (w, _) => w))
                .Select(joins => joins.Count())
                .Select(count => count == 0 ? 0 : (int)Math.Pow(2, count - 1))
                .Sum()
                .ToString();
        }

        private static (int[] winners, int[] yours) Parse(string row)
        {
            var parts = row.Split('|');
            var winners = parts[0]
                .Split(':')[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            var yours = parts[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            
            return (winners, yours);
        }
        
        private static (int card, List<int> winners, List<int> yours) Parse2(string row)
        {
            var parts = row.Split('|');
            var card = parts[0]
                .Split(':')[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)[1];
            var cardValue = int.Parse(card);
            var winners = parts[0]
                .Split(':')[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            var yours = parts[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            
            return (cardValue, winners, yours);
        }

        public static string SolvePart2(string[] rows)
        {
            var originalSet = rows
                .Select(Parse2)
                .ToDictionary(x => x.card, y => (y.winners, y.yours));
            var finalSet = new Dictionary<int, int>();

            foreach (var card in originalSet)
            {
                if (finalSet.ContainsKey(card.Key))
                    finalSet[card.Key]++;
                else
                    finalSet.Add(card.Key, 1);
                
                var matches = card.Value.winners
                    .Join(card.Value.yours, w => w, y => y, (w, _) => w)
                    .Count();

                for (var i = card.Key + 1; i <= card.Key + matches; i++)
                {
                    if (finalSet.ContainsKey(i))
                        finalSet[i] += finalSet[card.Key];
                    else
                        finalSet.Add(i, finalSet[card.Key]);
                }
            }
            
            return finalSet.Values.Sum().ToString();
        }
    }
}