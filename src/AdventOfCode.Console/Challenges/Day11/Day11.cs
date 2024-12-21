using System.Numerics;
using AdventOfCode.Core.Classes;

namespace AdventOfCode.Challenges;

public class Day11 : SolutionBase
{
    private static readonly Dictionary<(BigInteger, int), BigInteger> Cache = new();
    
    public override int Day => 11;

    public override object PartOne(string[] input)
    {
        var stones = input[0].Split(" ").Select(BigInteger.Parse).ToArray();

        return SumBigIntegers(stones, 25);
    }

    public override object PartTwo(string[] input)
    {
        var stones = input[0].Split(" ").Select(BigInteger.Parse).ToArray();

        return SumBigIntegers(stones, 75);
    }
    
    private static BigInteger Count(BigInteger stone, int cycles)
    {
        if (Cache.TryGetValue((stone, cycles), out var cachedResult))
        {
            return cachedResult;
        }

        if (cycles == 0)
        {
            return 1;
        }

        BigInteger result;

        if (stone == 0)
        {
            result = Count(1, cycles - 1);
        }
        else
        {
            var stoneString = stone.ToString();
            var length = stoneString.Length;

            if (length % 2 == 0)
            {
                var mid = length / 2;
                var first = BigInteger.Parse(stoneString[..mid]);
                var second = BigInteger.Parse(stoneString[mid..]);

                result = Count(first, cycles - 1) + Count(second, cycles - 1);
            }
            else
            {
                result = Count(stone * 2024, cycles - 1);
            }
        }

        Cache[(stone, cycles)] = result;

        return result;
    }

    private static BigInteger SumBigIntegers(BigInteger[] stones, int steps)
    {
        BigInteger total = 0;

        foreach (var stone in stones)
        {
            total += Count(stone, steps);
        }

        return total;
    }
}