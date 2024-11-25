using AdventOfCode.Challenges;

namespace AdventOfCode.Tests;

public class Day02Tests
{
    private Day02 _day02;
    
    [SetUp]
    public void Setup()
    {
        var testInput = new[]
        {
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green",
        };
        
        _day02 = new Day02(testInput);
    }

    [Test]
    public void Example_Part1_ShouldReturn8()
    {
        // act
        var result = _day02.SolvePart1();

        // assert
        Assert.That(result, Is.EqualTo(8));
    }
    
    [Test]
    public void Example_Part2_ShouldReturn2286()
    {
        // act
        var result = _day02.SolvePart2();

        // assert
        // 
        Assert.That(result, Is.EqualTo(2286));
    }
}
