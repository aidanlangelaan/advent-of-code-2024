using System.Diagnostics;
using System.Reflection;
using AdventOfCode.Core;
using AdventOfCode.Core.Classes;

namespace AdventOfCode;

public abstract class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter 'all' to run all days, 'dX' for day X, or 'dXpY' for day X part Y:");
        var input = Console.ReadLine();
        
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine($"No valid day entered, exiting.");
            return;
        }

        var solutions = GetSolutions();
        
        if (input.Equals("all", StringComparison.OrdinalIgnoreCase))
        {
            RunAllSolutions(solutions);
        }
        else if (input.StartsWith("d", StringComparison.OrdinalIgnoreCase))
        {
            RunSingleSolution(solutions, input);
        }
        else
        {
            Console.WriteLine("Invalid input, exiting.");
        }
    }

    private static List<ISolution> GetSolutions() =>
        Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(ISolution).IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false })
            .Select(t => Activator.CreateInstance(t) as ISolution)
            .Where(instance => instance != null)
            // ReSharper disable once NullableWarningSuppressionIsUsed
            .Select(instance => instance!)
            .OrderBy(s => s.Day)
            .ToList();

    private static void RunAllSolutions(IEnumerable<ISolution> solutions)
    {
        foreach (var solution in solutions)
        {
            ExecuteSolutionWithMetrics(solution);
        }
    }

    private static void RunSingleSolution(IEnumerable<ISolution> solutions, string input)
    {
        var parts = input[1..].Split('p');
        if (!int.TryParse(parts[0], out var day))
        {
            Console.WriteLine("Invalid day format.");
            return;
        }

        var solution = solutions.FirstOrDefault(s => s.Day == day);
        if (solution == null)
        {
            Console.WriteLine($"Solution for Day {day} not found.");
            return;
        }

        if (parts.Length == 1)
        {
            ExecuteSolutionWithMetrics(solution);
        }
        else if (parts.Length == 2 && int.TryParse(parts[1], out var part))
        {
            ExecuteSolutionWithMetrics(solution, part);
        }
        else
        {
            Console.WriteLine("Invalid part format.");
        }
    }
    
    private static void ExecuteSolutionWithMetrics(ISolution solution, int part = 0)
    {
        var (inputPart1, inputPart2) = ((SolutionBase)solution).GetInputs();
        Console.WriteLine($"Day {solution.Day}:");

        var stopwatch = Stopwatch.StartNew();

        if (part is 0 or 1)
        {
            stopwatch.Restart();
            var partOneResult = solution.PartOne(inputPart1);
            stopwatch.Stop();
            Console.WriteLine($"  Part One: {partOneResult} (Execution Time: {stopwatch.ElapsedMilliseconds} ms)");
        }

        if (part is 0 or 2)
        {
            stopwatch.Restart();
            var partTwoResult = solution.PartTwo(inputPart2 ?? inputPart1);
            stopwatch.Stop();
            Console.WriteLine($"  Part Two: {partTwoResult} (Execution Time: {stopwatch.ElapsedMilliseconds} ms)");
        }
    }
}