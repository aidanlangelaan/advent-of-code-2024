using System.Diagnostics;
using System.Reflection;
using AdventOfCode.Core;
using AdventOfCode.Core.Attributes;
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
            var solutionType = solution.GetType();
            if (solutionType.GetCustomAttribute<ExcludeDayFromRunAttribute>() != null)
            {
                Console.WriteLine($"Day {solution.Day}: Skipped.");
                continue;
            }

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
        var solutionType = solution.GetType();
        var (inputPart1, inputPart2) = ((SolutionBase)solution).GetInputs();
        Console.WriteLine($"Day {solution.Day}:");

        if (part is 0 or 1)
        {
            var partOneMethod = solutionType.GetMethod(nameof(solution.PartOne));
            if (partOneMethod?.GetCustomAttribute<ExcludePartFromRunAttribute>()?.Part == 1)
            {
                Console.WriteLine("  Part One: Skipped.");
            }
            else
            {
                var stopwatch = Stopwatch.StartNew();
                var partOneResult = solution.PartOne(inputPart1);
                stopwatch.Stop();
                Console.WriteLine($"  Part One: {partOneResult} (Execution Time: {stopwatch.ElapsedMilliseconds} ms)");
            }
        }

        if (part is 0 or 2)
        {
            var partTwoMethod = solutionType.GetMethod(nameof(solution.PartTwo));
            if (partTwoMethod?.GetCustomAttribute<ExcludePartFromRunAttribute>()?.Part == 2)
            {
                Console.WriteLine("  Part Two: Skipped.");
            }
            else
            {
                var stopwatch = Stopwatch.StartNew();
                var partTwoResult = solution.PartTwo(inputPart2 ?? inputPart1);
                stopwatch.Stop();
                Console.WriteLine($"  Part Two: {partTwoResult} (Execution Time: {stopwatch.ElapsedMilliseconds} ms)");
            }
        }
    }
}