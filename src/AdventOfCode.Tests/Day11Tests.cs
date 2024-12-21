using System.Numerics;
using AdventOfCode.Challenges;
using NUnit.Framework;

namespace AdventOfCode.Tests;

[TestFixture]
public class Day11Test
{
    private Day11 _day11;

    private readonly string[] input =
    [
        "125 17"
    ];

    [SetUp]
    public void Setup()
    {
        _day11 = new Day11();
    }
    
    [Test]
    public void Example_Part1_ShouldReturn55312()
    {
        // act
        var result = _day11.PartOne(input);
    
        // assert
        Assert.That(result, Is.EqualTo(55312));
    }

    [Test]
    public void Example_Part2_ShouldReturn65601038650482()
    {
        // act
        var result = _day11.PartTwo(input);

        // assert
        Assert.That(result, Is.EqualTo((BigInteger)65601038650482));
    }
}

