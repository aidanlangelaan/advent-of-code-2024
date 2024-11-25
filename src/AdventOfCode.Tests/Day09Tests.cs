using AdventOfCode.Challenges;

namespace AdventOfCode.Tests;

public class Day09Tests
{
    private Day09 _day09;
    
    [SetUp]
    public void Setup()
    {
        var testInput = new[]
        {
            "put test values here",
        };
        
        _day09 = new Day09(testInput);
    }

    [Test]
    public void Example_Part1_ShouldReturn123()
    {
        // act
        var result = _day09.SolvePart1();

        // assert
        Assert.That(result, Is.EqualTo(123));
    }
    
    [Test]
    public void Example_Part2_ShouldReturn123()
    {
        // act
        var result = _day09.SolvePart2();

        // assert
        Assert.That(result, Is.EqualTo(123));
    }
}
