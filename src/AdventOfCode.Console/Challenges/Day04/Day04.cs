using System.Collections;
using AdventOfCode.Core.Classes;

namespace AdventOfCode.Challenges;

public class Day04 : SolutionBase
{
    public override int Day => 04;

    public override object PartOne(string[] input)
    {
        var grid = input
            .SelectMany((row, y) => row.Select((value, x) => new { x, y, value }))
            .ToDictionary(cell => (cell.x, cell.y), cell => cell.value);
        
        var startPositions = grid
            .Where(x => x.Value == 'X')
            .Select(x => x.Key)
            .ToList();
        
        var occurrences = 0;
        foreach (var startPosition in startPositions)
        {
            var directions = GetDirections(grid, startPosition);

            foreach ((int X, int Y) direction in directions)
            {
                occurrences += FindOccurrenceInDirection(startPosition, direction, grid);
            }
        }
        
        return occurrences;
    }

    public override object PartTwo(string[] input)
    {
        var grid = input
            .SelectMany((row, y) => row.Select((value, x) => new { X = x, Y = y, value }))
            .ToDictionary(cell => (X: cell.X, Y: cell.Y), cell => cell.value);
        
        var startPositions = grid
            .Where(x => x.Value == 'A')
            .Select(x => x.Key)
            .ToList();
        
        var occurrences = 0;
        foreach (var startPosition in startPositions)
        {
            var isXmas = ValidateXmasCross(startPosition, grid);
            if (isXmas) occurrences++;
        }

        return occurrences;
    }

    private static bool ValidateXmasCross((int X, int Y) startPosition, Dictionary<(int X, int Y), char> grid)
    {
        grid.TryGetValue((startPosition.X, startPosition.Y), out var current);
        grid.TryGetValue((startPosition.X - 1, startPosition.Y - 1), out var upLeft);
        grid.TryGetValue((startPosition.X + 1, startPosition.Y - 1), out var upRight);
        grid.TryGetValue((startPosition.X - 1, startPosition.Y + 1), out var downLeft);
        grid.TryGetValue((startPosition.X + 1, startPosition.Y + 1), out var downRight);

        var part1 = string.Join("", [upLeft, current, downRight]);
        var part2 = string.Join("", [upRight, current, downLeft]);

        return part1 is "MAS" or "SAM" && part2 is "MAS" or "SAM";
    }

    private static int FindOccurrenceInDirection((int x, int y) startPosition, (int X, int Y) direction, Dictionary<(int x, int y), char> grid)
    {
        var current = (X: startPosition.x + direction.X, Y: startPosition.y + direction.Y, Char: 'M');
        while (true)
        {
            var nextLetter = current.Char == 'M' ? 'A' : 'S';
            var x = current.X + direction.X;
            var y = current.Y + direction.Y;
                    
            if (!grid.TryGetValue((x, y), out var value) || value != nextLetter)
            {
                break;
            }
                    
            if (nextLetter == 'S')
            {
                return 1;
            }
                    
            current = (X: x, Y: y, Char: nextLetter);
        }

        return 0;
    }

    private IEnumerable GetDirections(Dictionary<(int x, int y),char> grid, (int x, int y) startPosition)
    {
        var directions = new List<(int x, int y)>
        {
            (0, -1), // up
            (0, 1), // down
            (-1, 0), // left
            (1, 0), // right
            (-1, -1), // up-left
            (1, -1), // up-right
            (-1, 1), // down-left
            (1, 1), // down-right
        };
        
        var possibleDirections = new List<(int x, int y)>();
        foreach (var direction in directions)
        {
            var x = startPosition.x + direction.x;
            var y = startPosition.y + direction.y;
            
            if (grid.TryGetValue((x, y), out var value) && value == 'M')
            {
                possibleDirections.Add(direction);
            }
        }

        return possibleDirections;
    }
}

