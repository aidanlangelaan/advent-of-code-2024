using AdventOfCode.Core.Classes;

namespace AdventOfCode.Challenges;

public class Day08 : SolutionBase
{
    public override int Day => 08;

    public override object PartOne(string[] input)
    {
        var antennas = ParseInput(input);
        
        var antiNodes = DetermineAntiNodes(input, antennas);

        return antiNodes.Count;
    }

    public override object PartTwo(string[] input)
    {
        var antennas = ParseInput(input);
        
        var antiNodes = DetermineAntiNodes(input, antennas, resonantHarmonics: true);

        return antiNodes.Count;
    }
    
    private static HashSet<(int X, int Y)> DetermineAntiNodes(string[] input, List<(int X, int Y, char Frequency)> antennas, bool resonantHarmonics = false)
    {
        HashSet<(int X, int Y)> antiNodes = [];
        foreach (var antenna in antennas)
        {
            foreach (var otherAntenna in antennas.Where(x => x.Frequency == antenna.Frequency))
            {
                if (antenna == otherAntenna) continue;

                var distanceX = antenna.X - otherAntenna.X;
                var distanceY = antenna.Y - otherAntenna.Y;
                distanceX = antenna.X > otherAntenna.X ? -distanceX : Math.Abs(distanceX);
                distanceY = antenna.Y > otherAntenna.Y ? -distanceY : Math.Abs(distanceY);

                if (resonantHarmonics)
                {
                    var findIndex = 1;
                    while (true)
                    {
                        var antiNode = (X: antenna.X + distanceX * findIndex, Y: antenna.Y + distanceY * findIndex);

                        // don't include nodes outside the map
                        if (antiNode.X < 0 || antiNode.X >= input[0].Length || antiNode.Y < 0 ||
                            antiNode.Y >= input.Length) break;
                
                        antiNodes.Add(antiNode);

                        findIndex++;
                    }
                }
                else
                {
                    var antiNode = (X: otherAntenna.X + distanceX, Y: otherAntenna.Y + distanceY);

                    // don't include nodes outside the map
                    if (antiNode.X < 0 || antiNode.X >= input.Length || antiNode.Y < 0 ||
                        antiNode.Y >= input[0].Length) continue;
                
                    antiNodes.Add(antiNode);                    
                }
            }
        }

        return antiNodes;
    }
    
    private List<(int X, int Y, char Frequency)> ParseInput(string[] input)
    {
        List<(int X, int Y, char Frequency)> antennas = new();
        
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == '.') continue;

                antennas.Add((x, y, input[y][x]));
            }
        }

        return antennas;
    }
}