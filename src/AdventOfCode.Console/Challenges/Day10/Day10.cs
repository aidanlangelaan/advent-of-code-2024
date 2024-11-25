using System.ComponentModel;
using AdventOfCode.Core;

namespace AdventOfCode.Challenges;

[Description("Day 10")]
public class Day10 : Challenge<Day10>
{
    public Day10(string[] input) : base(input)
    {
    }

    public Day10() : base()
    {
    }

    public override int SolvePart1()
    {
        var grid = GetGrid();

        var startPoint = grid
            .SelectMany((row, i) => row.Select((cell, j) => new { i, j, cell }))
            .Where(x => x.cell == 'S')
            .Select(x => (x.i, x.j))
            .First();

        var path = GetPath(grid, startPoint);

        return path.Count / 2;
    }

    public override int SolvePart2()
    {
        var grid = GetGrid();

        var startPoint = grid
            .SelectMany((row, i) => row.Select((cell, j) => new { i, j, cell }))
            .Where(x => x.cell == 'S')
            .Select(x => (x.i, x.j))
            .First();

        var path = GetPath(grid, startPoint);

        var enclosed = 0;
        var rowsToCheck = path.Select(x => x.X).Distinct().Order();
        foreach (var row in rowsToCheck)
        {
            var columns = path.Where(x => x.X == row).Select(x => x.Y).Distinct().Order().ToList();

            var first = columns.First();
            var last = columns.Last();

            var columnsNotInPath = Enumerable.Range(first, last - first + 1)
                .Except(columns).ToList();

            enclosed += columnsNotInPath.Count(col => IsPointInPolygon(path, (row, col)));
        }

        return enclosed;
    }

    private List<(int X, int Y)> GetPath(List<List<char>> grid, (int X, int Y) startPoint)
    {
        var startDirection = GetNextDirection(grid, startPoint.X, startPoint.Y, -1, -1);
        
        var path = new List<(int X, int Y)>();
        var x = startDirection.X;
        var y = startDirection.Y;
        var xPrev = startPoint.X;
        var yPrev = startPoint.Y;
        path.Add((X: xPrev, Y: yPrev));
        path.Add((X: x, Y: y));
        
        while (true)
        {
            if (grid[x][y] == 'S')
            {
                break;
            }

            var nextDirection = GetNextDirection(grid, x, y, xPrev, yPrev);
            xPrev = x;
            yPrev = y;
            x = nextDirection.X;
            y = nextDirection.Y;

            path.Add((X: x, Y: y));
        }

        return path;
    }

    private List<List<char>> GetGrid()
    {
        var grid = _input.Select(t => t.Select(x => x).ToList()).ToList();

        // Fill with extra line around grid to prevent out of bounds
        for (var i = 0; i < grid.Count; i++)
        {
            grid[i] = grid[i].Prepend('.').Append('.').ToList();
        }

        grid.Insert(0, Enumerable.Repeat('.', grid[0].Count).ToList());
        grid.Add(Enumerable.Repeat('.', grid[0].Count).ToList());
        return grid;
    }

    private (int X, int Y) GetNextDirection(List<List<char>> grid, int x, int y, int xPrev, int yPrev)
    {
        var current = grid[x][y];
        
        if (current is 'S')
        {
            var left = grid[x][y - 1];
            var right = grid[x][y + 1];
            var top = grid[x - 1][y];
            var bottom = grid[x + 1][y];

            if (left is '-' or 'F' or 'L') return (x, y - 1);
            if (right is '-' or 'J' or '7') return (x, y + 1);
            if (top is '|' or 'F' or '7') return (x - 1, y);
            if (bottom is '|' or 'L' or 'J') return (x + 1, y);
        }
        if (current is '|')
        {
            // only up and down
            var top = grid[x - 1][y];
            if (x - 1 != xPrev && top is '|' or 'F' or '7' or 'S')
            {
                return (x - 1, y);
            }

            var bottom = grid[x + 1][y];
            if (x + 1 != xPrev && bottom is '|' or 'L' or 'J' or 'S')
            {
                return (x + 1, y);
            }
        }

        if (current is '-')
        {
            // only left and right
            var left = grid[x][y - 1];
            if (y - 1 != yPrev && left is '-' or 'F' or 'L' or 'S')
            {
                return (x, y - 1);
            }

            var right = grid[x][y + 1];
            if (y + 1 != yPrev && right is '-' or 'J' or '7' or 'S')
            {
                return (x, y + 1);
            }
        }

        if (current is 'L')
        {
            // only up and right
            var top = grid[x - 1][y];
            if (x - 1 != xPrev && top is '|' or 'F' or '7' or 'S')
            {
                return (x - 1, y);
            }

            var right = grid[x][y + 1];
            if (y + 1 != yPrev && right is '-' or 'J' or '7' or 'S')
            {
                return (x, y + 1);
            }
        }

        if (current is 'J')
        {
            // only up and left
            var top = grid[x - 1][y];
            if (x - 1 != xPrev && top is '|' or 'F' or '7' or 'S')
            {
                return (x - 1, y);
            }

            var left = grid[x][y - 1];
            if (y - 1 != yPrev && left is '-' or 'F' or 'L' or 'S')
            {
                return (x, y - 1);
            }
        }

        if (current is '7')
        {
            // only down and left
            var bottom = grid[x + 1][y];
            if (x + 1 != xPrev && bottom is '|' or 'L' or 'J' or 'S')
            {
                return (x + 1, y);
            }

            var left = grid[x][y - 1];
            if (y - 1 != yPrev && left is '-' or 'F' or 'L' or 'S')
            {
                return (x, y - 1);
            }
        }

        if (current is 'F')
        {
            // only down and right
            var bottom = grid[x + 1][y];
            if (x + 1 != xPrev && bottom is '|' or 'L' or 'J' or 'S')
            {
                return (x + 1, y);
            }

            var right = grid[x][y + 1];
            if (y + 1 != yPrev && right is '-' or 'J' or '7' or 'S')
            {
                return (x, y + 1);
            }
        }

        throw new Exception("No direction found");
    }

    private bool IsPointInPolygon(IReadOnlyList<(int X, int Y)> polygon, (int X, int Y) testPoint)
    {
        var result = false;
        var j = polygon.Count - 1;
        for (var i = 0; i < polygon.Count; i++)
        {
            if (polygon[i].Y < testPoint.Y && polygon[j].Y >= testPoint.Y ||
                polygon[j].Y < testPoint.Y && polygon[i].Y >= testPoint.Y)
            {
                if (polygon[i].X + (testPoint.Y - polygon[i].Y) /
                    (polygon[j].Y - polygon[i].Y) *
                    (polygon[j].X - polygon[i].X) < testPoint.X)
                {
                    result = !result;
                }
            }

            j = i;
        }

        return result;
    }
}