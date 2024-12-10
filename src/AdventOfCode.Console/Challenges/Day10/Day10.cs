using AdventOfCode.Core.Classes;

namespace AdventOfCode.Challenges;

public class Day10 : SolutionBase
{
    public override int Day => 10;

    public override object PartOne(string[] input)
    {
        var map = ParseInput(input);
        
        var startingPositions = GetStartingPositions(map);

        var trailScores = new Dictionary<(int x, int y), int>();
        
        var memo = new Dictionary<(int x, int y, int value), HashSet<(int x, int y)>>();
        foreach (var (x, y, _) in startingPositions)
        {
            var possibleTrails = FindPossibleTrails(map, x, y, 0, memo);
            if (possibleTrails.Count == 0) continue;
            
            trailScores.Add((x, y), possibleTrails.Count);
        }
        
        return trailScores.Sum(x => x.Value);
    }

    public override object PartTwo(string[] input)
    {
        var map = ParseInput(input);
        
        var startingPositions = GetStartingPositions(map);
        
        var trailRaitings = new Dictionary<(int x, int y), int>();

        var memo = new Dictionary<(int x, int y, int value), int>();
        foreach (var (x, y, _) in startingPositions)
        {
            var distinctTrails = CountDistinctTrails(map, x, y, 0, memo);
            if (distinctTrails == 0) continue;

            trailRaitings.Add((x, y), distinctTrails);
        }

        return trailRaitings.Sum(x => x.Value);
    }

    private static HashSet<(int x, int y)> FindPossibleTrails(int[][] map, int x, int y, int currentValue, Dictionary<(int x, int y, int value), HashSet<(int x, int y)>> memo)
    {
        if (x < 0 || x >= map[0].Length || y < 0 || y >= map.Length) return [];
        if (map[y][x] != currentValue) return [];
        
        if (memo.ContainsKey((x, y, currentValue)))
            return memo[(x, y, currentValue)];
        
        var reachable = new HashSet<(int x, int y)>();
        if (currentValue == 9)
        {
            reachable.Add((x, y));
            memo[(x, y, currentValue)] = reachable;
            return reachable;
        }
        
        var directions = new[] { (0, 1), (1, 0), (0, -1), (-1, 0) };

        foreach (var (dx, dy) in directions)
        {
            var neighborReachable = FindPossibleTrails(map, x + dx, y + dy, currentValue + 1, memo);
            reachable.UnionWith(neighborReachable);
        }

        memo[(x, y, currentValue)] = reachable;

        return reachable;
    }
    
    static int CountDistinctTrails(int[][] map, int x, int y, int currentValue, Dictionary<(int x, int y, int value), int> memo)
    {
        if (x < 0 || x >= map[0].Length || y < 0 || y >= map.Length) return 0;

        if (map[y][x] != currentValue) return 0;

        if (memo.ContainsKey((x, y, currentValue)))
            return memo[(x, y, currentValue)];

        if (currentValue == 9)
        {
            memo[(x, y, currentValue)] = 1;
            return 1;
        }

        var directions = new[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
        var trailCount = 0;

        foreach (var (dx, dy) in directions)
        {
            trailCount += CountDistinctTrails(map, x + dx, y + dy, currentValue + 1, memo);
        }

        memo[(x, y, currentValue)] = trailCount;

        return trailCount;
    }
    
    private static List<(int x, int y, int cell)> GetStartingPositions(int[][] map)
    {
        var startingPositions =
            map.Select((row, y) => row
                    .Select((cell, x) => (x, y, cell))
                    .Where(cell => cell.cell == 0))
                .SelectMany(x => x).ToList();
        return startingPositions;
    }
    
    private static int[][] ParseInput(string[] input)
    {
        return input.Select(line => line.Select(c => c - '0').ToArray()).ToArray();
    }
}