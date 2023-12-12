namespace Solutions.Day7
{
    public static class Solution
    {
        public static int Day => 7;

        public static string SolvePart1(string[] rows)
        {
            var cards = rows
                .Select(Parse)
                .Select(deck => (deck.deck, deck.bid, ParseType(deck.deck)))
                .ToList();
            cards.Sort(Compare);

            var sum = 0;

            for (var i = 0; i < cards.Count; i++)
            {
                sum += (i + 1) * cards[i].bid;
            }
            
            return sum.ToString();
        }

        private static (string deck, int bid) Parse(string row)
        {
            var parts = row.Split(' ');
            return (parts[0], int.Parse(parts[1]));
        }

        private static int Compare((string deck, int bid, DeckType type) left, (string deck, int bid, DeckType type) right)
        {
            if (left.type > right.type)
                return 1;
            if (left.type < right.type)
                return -1;

            var leftCards = left.deck.ToCharArray();
            var rightCards = right.deck.ToCharArray();

            for (var i = 0; i < 5; i++)
            {
                if (leftCards[i].NumVal() > rightCards[i].NumVal())
                    return 1;
                if (leftCards[i].NumVal() < rightCards[i].NumVal())
                    return -1;
            }
            
            return 0;
        }

        private static DeckType ParseType(string deck)
        {
            var cards = deck.ToCharArray();
            var map = new Dictionary<char, int>();

            foreach (var card in cards)
            {
                if (map.ContainsKey(card))
                    map[card]++;
                else
                    map.Add(card, 1);
            }

            if (map.Count == 1)
                return DeckType.FiveOfAKind;
            if (map.Count == 5)
                return DeckType.HighCard;
            if (map.Count == 2 && map.FirstOrDefault().Value is 1 or 4)
                return DeckType.FourOfAKind;
            if (map.Count == 2 && map.FirstOrDefault().Value is 2 or 3)
                return DeckType.FullHouse;
            if (map.Any(m => m.Value == 3))
                return DeckType.ThreeOfAKind;
            if (map.Count == 3)
                return DeckType.TwoPair;

            return DeckType.OnePair;
        }
        
        private static int Compare2((string deck, int bid, DeckType type) left, (string deck, int bid, DeckType type) right)
        {
            if (left.type > right.type)
                return 1;
            if (left.type < right.type)
                return -1;

            var leftCards = left.deck.ToCharArray();
            var rightCards = right.deck.ToCharArray();

            for (var i = 0; i < 5; i++)
            {
                if (leftCards[i].NumValJoker() > rightCards[i].NumValJoker())
                    return 1;
                if (leftCards[i].NumValJoker() < rightCards[i].NumValJoker())
                    return -1;
            }
            
            return 0;
        }
        
        private static DeckType ParseType2(string deck)
        {
            var cards = deck.ToCharArray();
            var map = new Dictionary<char, int>();

            foreach (var card in cards)
            {
                if (map.ContainsKey(card))
                    map[card]++;
                else
                    map.Add(card, 1);
            }

            if (!map.ContainsKey('J'))
            {
                if (map.Count is 1)
                    return DeckType.FiveOfAKind;
                if (map.Count is 5)
                    return DeckType.HighCard;
                if (map.Count is 2 && map.FirstOrDefault().Value is 1 or 4)
                    return DeckType.FourOfAKind;
                if (map.Count is 2 && map.FirstOrDefault().Value is 2 or 3)
                    return DeckType.FullHouse;
                if (map.Any(m => m.Value is 3))
                    return DeckType.ThreeOfAKind;
                if (map.Count is 3)
                    return DeckType.TwoPair;
                return DeckType.OnePair;
            }
            
            if (map.Count is 1 or 2)
                return DeckType.FiveOfAKind;
            if (map.Count is 3) // JJJ X Y, JJ XX Y, J XX YY, J X YYY
                if (map['J'] is 3 or 2 || (map['J'] is 1 && map.Any(m => m.Value is 3)))
                    return DeckType.FourOfAKind;
                else
                    return DeckType.FullHouse;
            if (map.Count is 4)
                if (map['J'] is 1 or 2)
                    return DeckType.ThreeOfAKind;

            return DeckType.OnePair;
        }
        
        public static string SolvePart2(string[] rows)
        {
            var cards = rows
                .Select(Parse)
                .Select(deck => (deck.deck, deck.bid, ParseType2(deck.deck)))
                .ToList();
            cards.Sort(Compare2);

            var sum = 0;

            for (var i = 0; i < cards.Count; i++)
            {
                sum += (i + 1) * cards[i].bid;
            }
            
            return sum.ToString();
        }
    }

    public enum DeckType
    {
        HighCard = 0,
        OnePair = 1,
        TwoPair = 2,
        ThreeOfAKind = 3,
        FullHouse = 4,
        FourOfAKind = 5,
        FiveOfAKind = 6
    }

    public static class Extensions
    {
        public static int NumVal(this char source) =>
            source switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'J' => 11,
                'T' => 10,
                _ => int.Parse(source.ToString())
            };
        
        public static int NumValJoker(this char source) =>
            source switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'J' => 1,
                'T' => 10,
                _ => int.Parse(source.ToString())
            };
    }
}