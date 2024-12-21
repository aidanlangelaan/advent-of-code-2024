using AdventOfCode.Challenges;
using NUnit.Framework;

namespace AdventOfCode.Tests;

[TestFixture]
public class Day12Test
{
    private Day12 _day12;

    private readonly string[] input =
    [
        "RRRRIICCFF",
        "RRRRIICCCF",
        "VVRRRCCFFF",
        "VVRCCCJFFF",
        "VVVVCJJCFE",
        "VVIVCCJJEE",
        "VVIIICJJEE",
        "MIIIIIJJEE",
        "MIIISIJEEE",
        "MMMISSJEEE",
    ];

    [SetUp]
    public void Setup()
    {
        _day12 = new Day12();
    }
    
    [Test]
    public void Example_Part1_ShouldReturn1930()
    {
        // act
        var result = _day12.PartOne(input);
    
        // assert
        Assert.That(result, Is.EqualTo(1930));
    }

    [Test]
    public void Example_Part2_ShouldReturn1206()
    {
        // act
        var result = _day12.PartTwo(input);

        // assert
        Assert.That(result, Is.EqualTo(1206));
    }
}

