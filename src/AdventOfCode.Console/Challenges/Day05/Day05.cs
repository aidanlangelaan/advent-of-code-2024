using System.Runtime.Intrinsics.X86;
using AdventOfCode.Core.Classes;
using AdventOfCode.Core.Extensions;

namespace AdventOfCode.Challenges;

public class Day05 : SolutionBase
{
    public override int Day => 05;

    public override object PartOne(string[] input)
    {
        var orderRules = GetOrderRulesFromInput(input);

        var pages = GetPagesFromInput(input);

        var correctLines = ValidateLines(pages, orderRules);

        return correctLines
            .Select(x => x[x.Length / 2])
            .Sum();
    }

    public override object PartTwo(string[] input)
    {
        var orderRules = GetOrderRulesFromInput(input);

        var pages = GetPagesFromInput(input);

        var incorrectLines = ValidateLines(pages, orderRules, returnIncorrectLines: true);

        var correctedLines = FixIncorrectLines(incorrectLines, orderRules);

        return correctedLines
            .Select(x => x[x.Length / 2])
            .Sum();
    }

    private static List<int[]> FixIncorrectLines(List<int[]> incorrectLines, List<int[]> orderRules)
    {
        List<int[]> correctedLines = [];
        foreach (var line in incorrectLines)
        {
            var rules = orderRules
                .Where(x => line.Contains(x[0]) && line.Contains(x[1]))
                .OrderBy(x => x[0])
                .ToList();
            
            correctedLines.Add(line.OrderBy(x => rules.Count(rule => rule[1] == x)).ToArray());
        }

        return correctedLines;
    }

    private static List<int[]> ValidateLines(List<int[]> pages, List<int[]> orderRules,
        bool returnIncorrectLines = false)
    {
        List<int[]> correctLines = [];
        List<int[]> incorrectLines = [];
        foreach (var line in pages)
        {
            var rules = orderRules
                .Where(x => line.Contains(x[0]) && line.Contains(x[1]))
                .OrderBy(x => x[0])
                .ToList();

            var lineCorrect = true;
            foreach (var rule in rules)
            {
                var before = rule[0];
                var after = rule[1];

                if (Array.IndexOf(line, after) < Array.IndexOf(line, before))
                {
                    lineCorrect = false;
                    incorrectLines.Add(line);
                    break;
                }
            }

            if (lineCorrect) correctLines.Add(line);
        }

        return returnIncorrectLines ? incorrectLines : correctLines;
    }

    private static List<int[]> GetPagesFromInput(string[] input)
    {
        return input
            .SkipWhile(x => x != "")
            .Skip(1)
            .Select(x => x.Split(",")
                .Select(int.Parse)
                .ToArray())
            .ToList();
    }

    private static List<int[]> GetOrderRulesFromInput(string[] input)
    {
        return input
            .TakeWhile(x => x != "")
            .Select(x => x.Split("|")
                .Select(int.Parse)
                .ToArray())
            .ToList();
    }
}