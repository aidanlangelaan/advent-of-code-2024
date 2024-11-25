using System.ComponentModel;
using AdventOfCode.Core;

namespace AdventOfCode.Challenges;

[Description("Day 02")]
public class Day02 : Challenge<Day02>
{
    public Day02(string[] input) : base(input)
    {
    }

    public Day02() : base()
    {
    }

    public override int SolvePart1()
    {
        // only 12 red cubes, 13 green cubes, and 14 blue cubes
        Dictionary<string, int> cubes = new()
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        var possibleGameIds = new List<int>();
        foreach (var line in _input)
        {
            var parts = line.Split(":");
            var gameId = Convert.ToInt32(parts[0].Split(" ").Last());

            if (CheckIfPossible(parts, cubes))
            {
                possibleGameIds.Add(gameId);
            }
        }
        
        return possibleGameIds.Sum();
    }

    private bool CheckIfPossible(IReadOnlyList<string> parts, IReadOnlyDictionary<string, int> cubes)
    {
        var game = parts[1].Split(",");
        foreach (var take in game)
        {
            var sets = take.Trim().Split(";");
            foreach (var set in sets)
            {
                var takeParts = set.Trim().Split(" ");
                if (cubes[takeParts[1]] < Convert.ToInt32(takeParts[0]))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public override int SolvePart2()
    {
        var counts = new List<int>(); 
        foreach (var line in _input)
        {
            var parts = line.Split(":");
            counts.Add(GetMinimalCubeCount(parts));
        }
        
        return counts.Sum();
    }
    
    private int GetMinimalCubeCount(IReadOnlyList<string> parts)
    {
        Dictionary<string, int> cubes = new()
        {
            { "red", 0 },
            { "green", 0 },
            { "blue", 0 }
        };
        
        var game = parts[1].Split(",");
        foreach (var take in game)
        {
            var sets = take.Trim().Split(";");
            foreach (var set in sets)
            {
                var takeParts = set.Trim().Split(" ");
                var count = Convert.ToInt32(takeParts[0]);
                
                if (cubes[takeParts[1]] < count)
                {
                    cubes[takeParts[1]] = count;
                }
            }
        }

        return cubes["red"] * cubes["green"] * cubes["blue"];
    }
}