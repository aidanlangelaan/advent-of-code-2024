using AdventOfCode.Challenges;

namespace AdventOfCode.Tests;

public class Day12Tests
{
    private Day12 _day12;
    
    [SetUp]
    public void Setup()
    {
        var testInput = new[]
        {
            "put test values here",
        };
        
        _day12 = new Day12(testInput);
    }

    [Test]
    public void Example_Part1_ShouldReturn123()
    {
        // act
        var result = _day12.SolvePart1();

        // assert
        Assert.That(result, Is.EqualTo(123));
    }
    
    [Test]
    public void Example_Part2_ShouldReturn123()
    {
        // act
        var result = _day12.SolvePart2();

        // assert
        Assert.That(result, Is.EqualTo(123));
    }
}
