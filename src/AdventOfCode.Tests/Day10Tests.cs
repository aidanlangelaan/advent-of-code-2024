using AdventOfCode.Challenges;

namespace AdventOfCode.Tests;

public class Day10Tests
{
    private Day10 _day10;
    
    [SetUp]
    public void Setup()
    {
        var testInput = new[]
        {
            "put test values here",
        };
        
        _day10 = new Day10(testInput);
    }

    [Test]
    public void Example_Part1_ShouldReturn123()
    {
        // act
        var result = _day10.SolvePart1();

        // assert
        Assert.That(result, Is.EqualTo(123));
    }
    
    [Test]
    public void Example_Part2_ShouldReturn123()
    {
        // act
        var result = _day10.SolvePart2();

        // assert
        Assert.That(result, Is.EqualTo(123));
    }
}
