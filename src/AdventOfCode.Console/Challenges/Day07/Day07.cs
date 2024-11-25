using System.ComponentModel;
using AdventOfCode.Core;

namespace AdventOfCode.Challenges;

[Description("Day 07")]
public class Day07 : Challenge<Day07>
{
    public Day07(string[] input) : base(input)
    {
    }

    public Day07() : base()
    {
    }

    public override int SolvePart1()
    {
        var groupedHands = GetGroupedHands();

        DetermineRanks(groupedHands);

        Console.WriteLine(groupedHands.Values.SelectMany(x => x).Sum(x => x.WinAmount));
        return 0;
    }

    public override int SolvePart2()
    {
        var groupedHands = GetGroupedHands(true);

        DetermineRanks(groupedHands);

        Console.WriteLine(groupedHands.Values.SelectMany(x => x).Sum(x => x.WinAmount));
        return 0;
    }

    private Dictionary<HandType, List<Hand>> GetGroupedHands(bool useJoker = false)
    {
        // parse all hands
        var hands = _input
            .Select(line => line.Trim().Split(" "))
            .Select(set => new Hand(set[0], long.Parse(set[1]), useJoker))
            .OrderByDescending(x => x.HandType)
            .ToList();

        // group hands with the same card type
        return hands
            .GroupBy(h => h.HandType)
            .ToDictionary(group => group.Key, group => group.ToList());
    }
    
    private void DetermineRanks(Dictionary<HandType, List<Hand>> groupedHands)
    {
        var rank = 1;
        foreach (var handsToCompare in groupedHands.Select(groupedHand => groupedHand.Value))
        {
            // if there is only one hand of this type no need to compare
            if (handsToCompare.Count == 1)
            {
                handsToCompare[0].Rank = rank++;
                continue;
            }

            // sort and assign rank
            handsToCompare.Sort();
            foreach (var hand in handsToCompare)
            {
                hand.Rank = rank++;
            }
        }
    }
    
    private class Hand(string cards, long bid, bool useJoker = false) : IComparable<Hand>
    {
        private string Cards { get; } = cards;

        private int[] CardValues => Cards.Select(GetCardValue).ToArray();

        private long Bid { get; } = bid;

        public int Rank { get; set; }
        
        public long WinAmount => Bid * Rank;

        public HandType HandType => DetermineHandType();

        private HandType DetermineHandType()
        {
            var labelCounts = Cards.
                GroupBy(c => c)
                .Select(g => new { g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            var jokerCount = 0;
            if (useJoker && cards.Contains('J'))
            {
                jokerCount = labelCounts.First(x => x.Key == 'J').Count;
                labelCounts.Remove(labelCounts.First(x => x.Key == 'J'));
            }

            // Check for Five of a Kind
            if (jokerCount == 5 || labelCounts[0].Count + jokerCount == 5)
            {
                return HandType.FiveOfAKind;
            }

            // Check for Four of a Kind
            if (labelCounts[0].Count + jokerCount == 4)
            {
                return HandType.FourOfAKind;
            }

            // Check for Full House
            if (labelCounts[0].Count + jokerCount == 3 
                && labelCounts[1].Count == 2)
            {
                return HandType.FullHouse;
            }

            // Check for Three of a Kind
            if (labelCounts[0].Count + jokerCount == 3)
            {
                return HandType.ThreeOfAKind;
            }

            // Check for Two Pair
            if (labelCounts[0].Count + jokerCount == 2 
                && labelCounts[1].Count == 2)
            {
                return HandType.TwoPair;
            }

            // Check for One Pair
            if (labelCounts[0].Count + jokerCount == 2)
            {
                return HandType.OnePair;
            }

            // High Card
            return HandType.HighCard;
        }

        private int GetCardValue(char card)
        {
            if (char.IsNumber(card)) return int.Parse(card.ToString());

            if (useJoker)
            {
                return card switch
                {
                    'A' => 13,
                    'K' => 12,
                    'Q' => 11,
                    'J' => 1,
                    'T' => 10,
                    _ => throw new ArgumentOutOfRangeException(nameof(card), card, null)
                };
            }
            
            return card switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'J' => 11,
                'T' => 10,
                _ => throw new ArgumentOutOfRangeException(nameof(card), card, null)
            };
        }
        
        public int CompareTo(Hand? otherHand)
        {
            if (otherHand == null) return 1;
            for (var i = 0; i < CardValues.Length; i++)
            {
                if (otherHand.CardValues[i] > CardValues[i]) return -1;
                if (otherHand.CardValues[i] < CardValues[i]) return 1;
            }
            
            return 0;
        }
    }

    private enum HandType
    {
        FiveOfAKind = 1,
        FourOfAKind = 2,
        FullHouse = 3,
        ThreeOfAKind = 4,
        TwoPair = 5,
        OnePair = 6,
        HighCard = 7
    }
}