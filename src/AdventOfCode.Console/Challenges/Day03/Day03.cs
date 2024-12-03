using System.Text.RegularExpressions;
using AdventOfCode.Core.Classes;

namespace AdventOfCode.Challenges;

public class Day03 : SolutionBase
{
    public override int Day => 03;

    public override object PartOne(string[] input)
    {
        const string pattern = @"mul\(([0-9]+)\,([0-9]+)\)";
        
        var coruptedMem = string.Join("", input);
        
        var total = 0;
        foreach (Match match in Regex.Matches(coruptedMem, pattern, RegexOptions.IgnoreCase))
        {
            total += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        }

        return total;
    }

    public override object PartTwo(string[] input)
    {
        const string pattern = @"(mul\(([0-9]+)\,([0-9]+)\))|(do\(\))|(don't\(\))";
        
        var coruptedMem = string.Join("", input);
        
        var total = 0;
        var stop = false;
        
        foreach (Match match in Regex.Matches(coruptedMem, pattern, RegexOptions.IgnoreCase))
        {
            if (match.Value.Contains("mul") && !stop)
            {
                total += int.Parse(match.Groups[2].Value) * int.Parse(match.Groups[3].Value);
            }
            else if (match.Value == "do()")
            {
                stop = false;
            }
            else if (match.Value == "don't()")
            {
                stop = true;
            }
        }

        return total;
    }
}

