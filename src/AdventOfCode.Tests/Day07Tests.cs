using AdventOfCode.Challenges;

namespace AdventOfCode.Tests;

public class Day07Tests
{
    private Day07 _day07;
    
    [SetUp]
    public void Setup()
    {
        var testInput = new[]
        {
            "put test values here",
        };
        
        _day07 = new Day07(testInput);
    }

    [Test]
    public void Example_Part1_ShouldReturn123()
    {
        // act
        var result = _day07.SolvePart1();

        // assert
        Assert.That(result, Is.EqualTo(123));
    }
    
    [Test]
    public void Example_Part2_ShouldReturn123()
    {
        // act
        var result = _day07.SolvePart2();

        // assert
        Assert.That(result, Is.EqualTo(123));
    }
}
