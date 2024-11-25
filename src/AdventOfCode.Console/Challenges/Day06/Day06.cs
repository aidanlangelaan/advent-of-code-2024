using System.ComponentModel;
using AdventOfCode.Core;

namespace AdventOfCode.Challenges;

[Description("Day 06")]
public class Day06 : Challenge<Day06>
{
    public Day06(string[] input) : base(input)
    {
    }

    public Day06() : base()
    {
    }

    public override int SolvePart1()
    {
        var races = GetRaces(1).ToArray();

        var winCount = ProcessRaces(races);

        var product = winCount[0];
        for (var i = 1; i < races.Length; i++)
        {
            product *= winCount[i];
        }

        Console.WriteLine(product);
        return 0;
    }

    public override int SolvePart2()
    {
        var races = GetRaces(2).ToArray();
        var winCount = ProcessRaces(races);
        
        Console.WriteLine(winCount[0]);
        return 0;
    }

    private List<long> ProcessRaces((long Time, long RecordDistance)[] races)
    {
        List<long> winCount = new();
        foreach (var race in races)
        {
            // get min win
            long minSpeed = 0;
            for (var i = 1; i < race.Time; i++)
            {
                var speed = (long)Math.Pow(i, 1);
                var distance = (race.Time - i) * speed;

                if (distance > race.RecordDistance)
                {
                    minSpeed = speed;
                    break;
                }
            }

            var maxSpeed = race.Time - minSpeed;

            winCount.Add(maxSpeed - minSpeed + 1);
        }

        return winCount;
    }

    private IEnumerable<(long Time, long RecordDistance)> GetRaces(int part)
    {
        var times = new List<long>();
        var distances = new List<long>();
        if (part == 1)
        {
            times = _input[0].Split(":")[1].Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(long.Parse).ToList();
            distances = _input[1].Split(":")[1].Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(long.Parse)
                .ToList();
        }
        else
        {
            var time = _input[0].Split(":")[1].Split(" ").Where(x => !string.IsNullOrEmpty(x))
                .Aggregate("", (current, next) => $"{current}{next}");
            times.Add(long.Parse(time));
            
            var distance = _input[1].Split(":")[1].Split(" ").Where(x => !string.IsNullOrEmpty(x))
                .Aggregate("", (current, next) => $"{current}{next}");
            distances.Add(long.Parse(distance));
        }

        return times.Select((t, i) => (Time: t, RecordDistance: distances[i])).ToList();
    }
}