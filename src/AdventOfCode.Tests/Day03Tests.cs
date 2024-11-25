using AdventOfCode.Challenges;

namespace AdventOfCode.Tests;

public class Day03Tests
{
    private Day03 _day03;
    
    [SetUp]
    public void Setup()
    {
        var testInput = new[]
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        };
        
        _day03 = new Day03(testInput);
    }

    [Test]
    public void Example_Part1_ShouldReturn4361()
    {
        // act
        var result = _day03.SolvePart1();

        // assert
        Assert.That(result, Is.EqualTo(4361));
    }
    
    [Test]
    public void Example_Part2_ShouldReturn467835()
    {
        // act
        var result = _day03.SolvePart2();

        // assert
        Assert.That(result, Is.EqualTo(467835));
    }
}
