using AdventOfCode.Challenges;

namespace AdventOfCode.Tests;

public class Day06Tests
{
    private Day06 _day06;
    
    [SetUp]
    public void Setup()
    {
        var testInput = new[]
        {
            "put test values here",
        };
        
        _day06 = new Day06(testInput);
    }

    [Test]
    public void Example_Part1_ShouldReturn0()
    {
        // act
        var result = _day06.SolvePart1();

        // assert
        Assert.That(result, Is.EqualTo(0));
    }
    
    [Test]
    public void Example_Part2_ShouldReturn0()
    {
        // act
        var result = _day06.SolvePart2();

        // assert
        Assert.That(result, Is.EqualTo(0));
    }
}
