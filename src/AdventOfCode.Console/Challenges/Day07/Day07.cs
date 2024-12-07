using AdventOfCode.Core.Classes;

namespace AdventOfCode.Challenges;

public class Day07 : SolutionBase
{
    public override int Day => 07;

    public override object PartOne(string[] input)
    {
        var equations = ParseInput(input);

        // Find and sum valid equations (using + and *)
        var validEquations = FindValidEquations(equations, enableConcatenation: false);

        return validEquations.Sum(x => x.Target);
    }

    public override object PartTwo(string[] input)
    {
        var equations = ParseInput(input);

        // Find and sum valid equations (using +, *, and ||)
        var validEquations = FindValidEquations(equations, enableConcatenation: true);

        return validEquations.Sum(x => x.Target);
    }

    private static List<(long Target, List<int> Numbers)> FindValidEquations(
        List<(long Target, List<int> Numbers)> equations,
        bool enableConcatenation)
    {
        var validEquations = new List<(long Target, List<int> Numbers)>();

        foreach (var (target, numbers) in equations)
        {
            if (HasSolution(target, numbers, enableConcatenation))
                validEquations.Add((target, numbers));
        }

        return validEquations;
    }

    private static bool HasSolution(long target, List<int> numbers, bool enableConcatenation)
    {
        var memo = new Dictionary<(int index, long currentValue), bool>();

        return TryOperators(1, numbers[0]);

        bool TryOperators(int index, long currentValue)
        {
            if (index == numbers.Count)
                return currentValue == target;

            if (memo.TryGetValue((index, currentValue), out bool result))
                return result;

            // Try addition
            if (TryOperators(index + 1, currentValue + numbers[index]))
                return memo[(index, currentValue)] = true;

            // Try multiplication
            if (TryOperators(index + 1, currentValue * numbers[index]))
                return memo[(index, currentValue)] = true;

            // Try concatenation (only if enabled)
            if (enableConcatenation)
            {
                long concatenatedValue = long.Parse($"{currentValue}{numbers[index]}");
                if (TryOperators(index + 1, concatenatedValue))
                    return memo[(index, currentValue)] = true;
            }

            return memo[(index, currentValue)] = false;
        }
    }

    private static List<(long Target, List<int> Numbers)> ParseInput(string[] input)
    {
        var equations = new List<(long Target, List<int> Numbers)>();

        foreach (var line in input)
        {
            var parts = line.Split(':');
            var target = long.Parse(parts[0]);
            var numbers = parts[1].Trim().Split(' ').Select(int.Parse).ToList();
            equations.Add((target, numbers));
        }

        return equations;
    }
}