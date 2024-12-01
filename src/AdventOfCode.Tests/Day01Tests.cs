using AdventOfCode.Challenges;
using NUnit.Framework;

namespace AdventOfCode.Tests;

[TestFixture]
public class Day01Test
{
    private Day01 _day01;

    private readonly string[] input =
    [
        "3   4",
        "4   3",
        "2   5",
        "1   3",
        "3   9",
        "3   3",
    ];

    [SetUp]
    public void Setup()
    {
        _day01 = new Day01();
    }
    
    [Test]
    public void Example_Part1_ShouldReturn11()
    {
        // act
        var result = _day01.PartOne(input);
    
        // assert
        Assert.That(result, Is.EqualTo(11));
    }

    [Test]
    public void Example_Part2_ShouldReturn31()
    {
        // act
        var result = _day01.PartTwo(input);

        // assert
        Assert.That(result, Is.EqualTo(31));
    }
}

