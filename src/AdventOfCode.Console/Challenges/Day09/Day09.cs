using System.ComponentModel;
using AdventOfCode.Core;

namespace AdventOfCode.Challenges;

[Description("Day 09")]
public class Day09 : Challenge<Day09>
{
    public Day09(string[] input) : base(input)
    {
    }

    public Day09() : base()
    {
    }

    public override int SolvePart1()
    {
        var extrapolatedValues = ProcessInput();
        return extrapolatedValues.Sum();
    }

    public override int SolvePart2()
    {
        var extrapolatedValues = ProcessInput(true);
        return extrapolatedValues.Sum();
    }

    private List<int> ProcessInput(bool reverse = false)
    {
        var extrapolatedValues = new List<int>();
        foreach (var line in _input)
        {
            var history = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var sequences = new List<List<int>>() { history };

            var current = sequences.Last();
            while (current.Any(x => x != 0))
            {
                var sequence = new List<int>();
                for (var i = 1; i < current.Count; i++)
                {
                    sequence.Add(current[i] - current[i - 1]);
                }

                sequences.Add(sequence);
                current = sequence;
            }

            sequences.Reverse();
            var extrapolatedValue = 0;
            for (var i = 1; i < sequences.Count; i++)
            {
                extrapolatedValue = reverse
                    ? sequences[i].First() - extrapolatedValue
                    : sequences[i].Last() + extrapolatedValue;
            }

            extrapolatedValues.Add(extrapolatedValue);
        }

        return extrapolatedValues;
    }
}