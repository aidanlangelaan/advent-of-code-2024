using AdventOfCode.Challenges;

namespace AdventOfCode.Tests;

public class Day11Tests
{
    private Day11 _day11;
    
    [SetUp]
    public void Setup()
    {
        var testInput = new[]
        {
            "put test values here",
        };
        
        _day11 = new Day11(testInput);
    }

    [Test]
    public void Example_Part1_ShouldReturn123()
    {
        // act
        var result = _day11.SolvePart1();

        // assert
        Assert.That(result, Is.EqualTo(123));
    }
    
    [Test]
    public void Example_Part2_ShouldReturn123()
    {
        // act
        var result = _day11.SolvePart2();

        // assert
        Assert.That(result, Is.EqualTo(123));
    }
}
