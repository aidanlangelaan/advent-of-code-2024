using AdventOfCode.Challenges;
using NUnit.Framework;

namespace AdventOfCode.Tests;

[TestFixture]
public class Day07Test
{
    private Day07 _day07;

    private readonly string[] input =
    [
        "190: 10 19",
        "3267: 81 40 27",
        "83: 17 5",
        "156: 15 6",
        "7290: 6 8 6 15",
        "161011: 16 10 13",
        "192: 17 8 14",
        "21037: 9 7 18 13",
        "292: 11 6 16 20",
    ];

    [SetUp]
    public void Setup()
    {
        _day07 = new Day07();
    }
    
    [Test]
    public void Example_Part1_ShouldReturn3749()
    {
        // act
        var result = _day07.PartOne(input);
    
        // assert
        Assert.That(result, Is.EqualTo(3749));
    }

    [Test]
    public void Example_Part2_ShouldReturn11387()
    {
        // act
        var result = _day07.PartTwo(input);

        // assert
        Assert.That(result, Is.EqualTo(11387));
    }
}

