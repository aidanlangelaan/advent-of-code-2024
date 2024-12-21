using AdventOfCode.Core.Classes;

namespace AdventOfCode.Challenges;

public class Day12 : SolutionBase
{
    public override int Day => 12;

    public override object PartOne(string[] input)
    {
        var grid = ParseInput(input);
        var regions = DiscoverRegions(grid);

        // Total cost: Sum of (area * perimeter) for all regions
        return regions.Sum(region => region.Count * CalculateRegionPerimeter(grid, region));
    }

    public override object PartTwo(string[] input)
    {
        var grid = ParseInput(input);
        var regions = DiscoverRegions(grid);

        // Total cost: Sum of (area * sides) for all regions
        return regions.Sum(region => region.Count * CalculateRegionSides(region));
    }

    private static char[][] ParseInput(string[] input)
    {
        return input.Select(line => line.ToCharArray()).ToArray();
    }

    private static List<HashSet<(int row, int column)>> DiscoverRegions(char[][] grid)
    {
        var rows = grid.Length;
        var cols = grid[0].Length;

        var regions = new List<HashSet<(int row, int column)>>();
        var seen = new HashSet<(int row, int column)>();

        for (var row = 0; row < rows; row++)
        {
            for (var column = 0; column < cols; column++)
            {
                if (seen.Contains((row, column))) continue;

                var region = DiscoverRegion(grid, row, column, rows, cols, seen);
                regions.Add(region);
            }
        }

        return regions;
    }

    private static HashSet<(int row, int column)> DiscoverRegion(char[][] grid, int startRow, int startColumn, int rows,
        int cols, HashSet<(int row, int column)> seen)
    {
        var region = new HashSet<(int row, int column)> { (startRow, startColumn) };
        var queue = new Queue<(int row, int column)>();
        queue.Enqueue((startRow, startColumn));
        seen.Add((startRow, startColumn));

        var crop = grid[startRow][startColumn];

        while (queue.Count > 0)
        {
            var (currentRow, currentColumn) = queue.Dequeue();
            foreach (var (neighborRow, neighborColumn) in new[]
                     {
                         (currentRow - 1, currentColumn),
                         (currentRow + 1, currentColumn),
                         (currentRow, currentColumn - 1),
                         (currentRow, currentColumn + 1)
                     })
            {
                // Invalid neighbors
                if (neighborRow < 0 || neighborColumn < 0 || neighborRow >= rows || neighborColumn >= cols) continue;

                // Neighbors of a different crop
                if (grid[neighborRow][neighborColumn] != crop) continue;

                // Already visited neighbors
                if (seen.Contains((neighborRow, neighborColumn))) continue;

                region.Add((neighborRow, neighborColumn));
                queue.Enqueue((neighborRow, neighborColumn));
                seen.Add((neighborRow, neighborColumn));
            }
        }

        return region;
    }

    private static int CalculateRegionPerimeter(char[][] grid, HashSet<(int row, int column)> region)
    {
        var perimeter = 0;

        foreach (var (row, column) in region)
        {
            // Check all neighbors of the current cell
            foreach (var (neighborRow, neighborColumn) in new[]
                     {
                         (row - 1, column),
                         (row + 1, column),
                         (row, column - 1),
                         (row, column + 1)
                     })
            {
                // If the neighbor is outside the grid or belongs to a different region, it contributes to the perimeter
                if (neighborRow < 0 || neighborColumn < 0 || neighborRow >= grid.Length ||
                    neighborColumn >= grid[0].Length ||
                    !region.Contains((neighborRow, neighborColumn)))
                {
                    perimeter++;
                }
            }
        }

        return perimeter;
    }

    private static int CalculateRegionSides(HashSet<(int row, int column)> region)
    {
        var cornerCandidates = new HashSet<(double row, double column)>();

        foreach (var (row, column) in region)
        {
            cornerCandidates.Add((row - 0.5, column - 0.5));
            cornerCandidates.Add((row + 0.5, column - 0.5));
            cornerCandidates.Add((row + 0.5, column + 0.5));
            cornerCandidates.Add((row - 0.5, column + 0.5));
        }

        var corners = 0;
        
        foreach (var (cornerRow, cornerColumn) in cornerCandidates)
        {
            var config = new[]
            {
                region.Contains(((int)(cornerRow - 0.5), (int)(cornerColumn - 0.5))),
                region.Contains(((int)(cornerRow + 0.5), (int)(cornerColumn - 0.5))),
                region.Contains(((int)(cornerRow + 0.5), (int)(cornerColumn + 0.5))),
                region.Contains(((int)(cornerRow - 0.5), (int)(cornerColumn + 0.5))),
            };

            var number = config.Count(x => x);
            if (number == 1)
            {
                corners += 1;
            }
            else if (number == 2)
            {
                if ((config[0] && config[2]) || (config[1] && config[3]))
                {
                    corners += 2;
                }
            }
            else if (number == 3)
            {
                corners += 1;
            }
        }

        return corners;
    }
}