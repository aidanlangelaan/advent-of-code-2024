using System.ComponentModel;
using AdventOfCode.Core;

namespace AdventOfCode.Challenges;

[Description("Day 13")]
public class Day13 : Challenge<Day13>
{
    public Day13(string[] input) : base(input)
    {
    }

    public Day13() : base()
    {
    }

    public override int SolvePart1()
    {
        var patterns = GetPatterns();

        var totalReflections = new List<int>();
        foreach (var pattern in patterns)
        {
            var verticalReflection = GetReflectionScore(pattern);
            if (verticalReflection > 0)
            {
                totalReflections.Add(verticalReflection);
                continue;
            }

            var patternColumns = Enumerable.Range(0, pattern[0].Length)
                .Select(colIndex => new string(pattern.Select(row => row[colIndex]).ToArray()))
                .ToList();
            
            var horizontalReflection = GetReflectionScore(patternColumns, true);
            if (horizontalReflection > 0)
            {
                totalReflections.Add(horizontalReflection);
            }
        }

        return totalReflections.Sum();
    }

    private List<List<string>> GetPatterns()
    {
        var patterns = new List<List<string>>();
        var pattern = new List<string>();
        foreach (var line in _input)
        {
            if (string.IsNullOrEmpty(line))
            {
                patterns.Add(pattern);
                pattern = new List<string>();
                continue;
            }
            pattern.Add(line);
        }
        
        patterns.Add(pattern);
        return patterns;
    }

    private int GetReflectionScore(List<string> pattern, bool fixSmudge, bool isHorizontal = false)
    {
        for (var j = 0; j < pattern.Count - 1; j++)
        {
            var line = pattern[j];
            var nextLine = pattern[j + 1];
            if (line == nextLine)
            {
                var isEqual = true;
                
                var previousLines = pattern.Take(j).Reverse().ToList();
                for (var k = 0; k < previousLines.Count; k++)
                {
                    var futureLineIndex = j + 2 + k;
                    if (futureLineIndex >= pattern.Count)
                        break;

                    var previousLine = previousLines[k];
                    var futureLine = pattern[futureLineIndex];
                    if (previousLine != futureLine)
                    {
                        if (fixSmudge && previousLine.Zip(futureLine, (c1, c2) => c1 != c2 ? 1 : 0).Sum() > 2)
                        {
                            pattern
                                
                            previousLine = futureLine;
                        }
                        
                        isEqual = false;
                        break;
                    }
                }

                if (isEqual)
                {
                    if (isHorizontal)
                    {
                        return j + 1;
                    }

                    return (j + 1) * 100;
                }
            }
        }

        return 0;
    }

    public override int SolvePart2()
    {
        throw new NotImplementedException();
    }
}