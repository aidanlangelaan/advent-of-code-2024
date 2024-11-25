using System.ComponentModel;
using AdventOfCode.Core;

namespace AdventOfCode.Challenges;

[Description("Day 12")]
public class Day12 : Challenge<Day12>
{
    public Day12(string[] input) : base(input)
    {
    }

    public Day12() : base()
    {
    }


    private static Dictionary<string, long> memo = new();

    public override int SolvePart1()
    {
        var arrangementCount = new List<int>();
        foreach (var line in _input)
        {
            var parts = line.Split(" ");
            var springs = parts[0];
            var damagedGroups = parts[1].Split(',').Select(int.Parse).ToList();

            var possibleArrangementCount = GetPossibleArrangementCount(springs, damagedGroups);
            arrangementCount.Add(possibleArrangementCount);
        }

        return arrangementCount.Sum();
    }

    public override int SolvePart2()
    {
        var arrangementCount = new List<int>();
        foreach (var line in _input)
        {
            var parts = line.Split(" ");
            var springs = Unfold(parts[0], '?');
            var damagedGroups = Unfold(parts[1], ',').Split(',').Select(int.Parse).ToList();

            var possibleArrangementCount = GetPossibleArrangementCount(springs, damagedGroups);
            arrangementCount.Add(possibleArrangementCount);
        }

        return arrangementCount.Sum();
    }

    private string Unfold(string input, char separator)
    {
        var unfoldedInput = string.Empty;
        for (var i = 0; i < 5; i++)
        {
            unfoldedInput += input;
            if (i < 4)
            {
                unfoldedInput += separator;
            }
        }

        return unfoldedInput;
    }

    private int GetPossibleArrangementCount(string springs, List<int> damagedGroups)
    {
        var unknowns = springs.Count(c => c == '?');
        var possibleOptions = (int)Math.Pow(2, unknowns);
        if (memo.TryGetValue(springs, out var value)) return (int)value;

        var possibleArrangementCount = 0;
        for (var i = 0; i < possibleOptions; i++)
        {
            var option = GetOption(springs, i);
            if (IsOptionValid(option, damagedGroups)) possibleArrangementCount++;
        }

        memo.Add(springs, possibleArrangementCount);

        return possibleArrangementCount;
    }

    private string GetOption(string springs, int option)
    {
        var result = string.Empty;
        foreach (var spring in springs)
        {
            if (spring == '?')
            {
                result += option % 2 == 1 ? '#' : '.';
                option >>= 1;
            }
            else
            {
                result += spring;
            }
        }

        return result;
    }

    private bool IsOptionValid(string option, List<int> damagedGroups)
    {
        var groups = option
            .Split('.')
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList()
            .Select(x => x.Length);

        return groups.SequenceEqual(damagedGroups);
    }
}