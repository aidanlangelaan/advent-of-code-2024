using AdventOfCode.Challenges;
using NUnit.Framework;

namespace AdventOfCode.Tests;

[TestFixture]
public class Day10Test
{
    private Day10 _day10;

    private readonly string[] input =
    [
        "89010123",
        "78121874",
        "87430965",
        "96549874",
        "45678903",
        "32019012",
        "01329801",
        "10456732",
    ];

    [SetUp]
    public void Setup()
    {
        _day10 = new Day10();
    }
    
    [Test]
    public void Example_Part1_ShouldReturn36()
    {
        // act
        var result = _day10.PartOne(input);
    
        // assert
        Assert.That(result, Is.EqualTo(36));
    }

    [Test]
    public void Example_Part2_ShouldReturn81()
    {
        // act
        var result = _day10.PartTwo(input);

        // assert
        Assert.That(result, Is.EqualTo(81));
    }
}

