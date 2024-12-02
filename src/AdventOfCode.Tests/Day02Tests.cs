using AdventOfCode.Challenges;
using NUnit.Framework;

namespace AdventOfCode.Tests;

[TestFixture]
public class Day02Test
{
    private Day02 _day02;

    private readonly string[] input =
    [
        "7 6 4 2 1",
        "1 2 7 8 9",
        "9 7 6 2 1",
        "1 3 2 4 5",
        "8 6 4 4 1",
        "1 3 6 7 9",
    ];

    [SetUp]
    public void Setup()
    {
        _day02 = new Day02();
    }
    
    [Test]
    public void Example_Part1_ShouldReturn2()
    {
        // act
        var result = _day02.PartOne(input);
    
        // assert
        Assert.That(result, Is.EqualTo(2));
    }

    [Test]
    public void Example_Part2_ShouldReturn4()
    {
        // act
        var result = _day02.PartTwo(input);

        // assert
        Assert.That(result, Is.EqualTo(4));
    }
}

