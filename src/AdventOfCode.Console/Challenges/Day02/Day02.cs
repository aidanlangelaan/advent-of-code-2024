using AdventOfCode.Core.Classes;

namespace AdventOfCode.Challenges;

public class Day02 : SolutionBase
{
    public override int Day => 02;

    public override object PartOne(string[] input)
    {
        var reports = input.Select(x => x.Split(" ").Select(int.Parse).ToArray()).ToArray();

        var safeReportCount = 0;
        foreach (var report in reports)
        {
            var increasing = report[0] < report[^1];
            if (ValidateReport(report, !increasing)) safeReportCount++;
        }

        return safeReportCount;
    }

    public override object PartTwo(string[] input)
    {
        var reports = input.Select(x => x.Split(" ").Select(int.Parse).ToArray()).ToArray();

        var safeReportCount = 0;
        foreach (var report in reports)
        {
            var increasing = report[0] < report[^2];
            if (ValidateReport(report, !increasing, enableProblemDampener: true))
            {
                safeReportCount++;
            }
        }

        return safeReportCount;
    }

    private static bool ValidateReport(int[] report, bool reverse, bool enableProblemDampener = false)
    {
        if (reverse) Array.Reverse(report);

        if (IsValid(report))
        {
            return true;
        }

        if (enableProblemDampener)
        {
            for (var i = 0; i < report.Length; i++)
            {
                var modifiedReport = report.Where((_, index) => index != i).ToArray();
                if (IsValid(modifiedReport))
                {
                    return true;
                }
            }   
        }

        return false;
    }

    private static bool IsValid(int[] report)
    {
        var currentValue = report[0];
        for (var i = 1; i < report.Length; i++)
        {
            var nextValue = report[i];
            if (nextValue <= currentValue || nextValue - currentValue > 3)
            {
                return false;
            }

            currentValue = nextValue;
        }

        return true;
    }
}