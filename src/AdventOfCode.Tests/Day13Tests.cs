using AdventOfCode.Challenges;

namespace AdventOfCode.Tests;

public class Day13Tests
{
    private Day13 _day13;
    
    [SetUp]
    public void Setup()
    {
        var testInput = new[]
        {
            "put test values here",
        };
        
        _day13 = new Day13(testInput);
    }

    [Test]
    public void Example_Part1_ShouldReturn123()
    {
        // act
        var result = _day13.SolvePart1();

        // assert
        Assert.That(result, Is.EqualTo(123));
    }
    
    [Test]
    public void Example_Part2_ShouldReturn123()
    {
        // act
        var result = _day13.SolvePart2();

        // assert
        Assert.That(result, Is.EqualTo(123));
    }
}
