using AdventOfCode.Core.Attributes;
using AdventOfCode.Core.Classes;

namespace AdventOfCode.Challenges;

public class Day06 : SolutionBase
{
    public override int Day => 06;

    public override object PartOne(string[] input)
    {
        var grid = ParseGrid(input);
        var startPosition = grid.First(x => x.Value == '^').Key;
        grid[startPosition] = '.';

        var visited = GetVisitedPositions(startPosition, grid);

        return visited.Select(x => x.Coords).Distinct().Count();
    }

    [ExcludePartFromRun(2)]
    public override object PartTwo(string[] input)
    {
        var grid = ParseGrid(input);
        var startPosition = grid.First(x => x.Value == '^').Key;
        grid[startPosition] = '.';

        var initialPath = GetVisitedPositions(startPosition, grid);

        List<(int, int)> blockLoops = [];
        foreach (var position in initialPath.Select(x => x.Coords))
        {
            // skip the start position
            if (position == startPosition)
            {
                continue;
            }

            // place temporary block
            grid[position] = '#';

            // run the path again to see if we hit a loop
            var visitedWithBlock = GetVisitedPositions(startPosition, grid, loopDetection: true);
            
            if (visitedWithBlock[^1].Direction == "LOOP" && !blockLoops.Contains(position))
            {
                blockLoops.Add(position);
            }

            // reset temporary block
            grid[position] = '.';
        }

        return blockLoops.Count;
    }

    private static List<((int X, int Y) Coords, string Direction)> GetVisitedPositions((int X, int Y) position,
        Dictionary<(int X, int Y), char> grid, bool loopDetection = false)
    {
        var currentPosition = (Coords: (X: position.X, Y: position.Y), Direction: "UP");
        var visited = new List<((int X, int Y) Coords, string Direction)> { currentPosition };
        var directions = new Dictionary<string, (int X, int Y)>
        {
            { "UP", (X: 0, Y: -1) },
            { "DOWN", (X: 0, Y: 1) },
            { "LEFT", (X: -1, Y: 0) },
            { "RIGHT", (X: 1, Y: 0) },
        };

        while (true)
        {
            var newPosition = (Coords: (X: currentPosition.Coords.X + directions[currentPosition.Direction].X,
                Y: currentPosition.Coords.Y + directions[currentPosition.Direction].Y), currentPosition.Direction);

            if (!grid.ContainsKey(newPosition.Coords)) break;

            if (grid.TryGetValue(newPosition.Coords, out var value) && value == '.')
            {
                if (visited.Contains(newPosition) && loopDetection)
                {
                    // dirty hack to break out of the loop and let the caller know we hit a loop
                    visited.Add(((X: 0, Y: 0), "LOOP"));
                    break;
                }

                visited.Add(newPosition);
                currentPosition = newPosition;
            }
            else
            {
                // Change direction if we hit a wall or non-empty cell
                currentPosition.Direction = currentPosition.Direction switch
                {
                    "UP" => "RIGHT",
                    "RIGHT" => "DOWN",
                    "DOWN" => "LEFT",
                    "LEFT" => "UP",
                    _ => currentPosition.Direction
                };
            }
        }

        return visited;
    }

    private static Dictionary<(int X, int Y), char> ParseGrid(string[] input)
    {
        return input
            .SelectMany((row, y) => row.Select((value, x) => new { X = x, Y = y, value }))
            .ToDictionary(cell => (X: cell.X, Y: cell.Y), cell => cell.value);
    }
}