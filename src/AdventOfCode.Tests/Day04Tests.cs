using AdventOfCode.Challenges;
using NUnit.Framework;

namespace AdventOfCode.Tests;

[TestFixture]
public class Day04Test
{
    private Day04 _day04;

    private readonly string[] input =
    [
        "MMMSXXMASM",
        "MSAMXMSMSA",
        "AMXSXMAAMM",
        "MSAMASMSMX",
        "XMASAMXAMM",
        "XXAMMXXAMA",
        "SMSMSASXSS",
        "SAXAMASAAA",
        "MAMMMXMMMM",
        "MXMXAXMASX",
    ];

    [SetUp]
    public void Setup()
    {
        _day04 = new Day04();
    }
    
    [Test]
    public void Example_Part1_ShouldReturn18()
    {
        // act
        var result = _day04.PartOne(input);
    
        // assert
        Assert.That(result, Is.EqualTo(18));
    }

    [Test]
    public void Example_Part2_ShouldReturn9()
    {
        // act
        var result = _day04.PartTwo(input);

        // assert
        Assert.That(result, Is.EqualTo(9));
    }
}

