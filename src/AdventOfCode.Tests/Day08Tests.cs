using AdventOfCode.Challenges;
using NUnit.Framework;

namespace AdventOfCode.Tests;

[TestFixture]
public class Day08Test
{
    private Day08 _day08;

    private readonly string[] input =
    [
        "............",
        "........0...",
        ".....0......",
        ".......0....",
        "....0.......",
        "......A.....",
        "............",
        "............",
        "........A...",
        ".........A..",
        "............",
        "............",
    ];

    [SetUp]
    public void Setup()
    {
        _day08 = new Day08();
    }

    [Test]
    public void Example_Part1_ShouldReturn14()
    {
        // act
        var result = _day08.PartOne(input);

        // assert
        Assert.That(result, Is.EqualTo(14));
    }

    [Test]
    public void Example_Part2_ShouldReturn34()
    {
        // act
        var result = _day08.PartTwo(input);

        // assert
        Assert.That(result, Is.EqualTo(34));
    }
}