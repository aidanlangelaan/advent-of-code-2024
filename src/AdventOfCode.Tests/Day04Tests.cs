using AdventOfCode.Challenges;

namespace AdventOfCode.Tests;

public class Day04Tests
{
    private Day04 _day04;
    
    [SetUp]
    public void Setup()
    {
        var testInput = new[]
        {
            "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53",
            "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19",
            "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1",
            "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83",
            "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36",
            "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"
        };
        
        _day04 = new Day04(testInput);
    }

    [Test]
    public void Example_Part1_ShouldReturn13()
    {
        // act
        var result = _day04.SolvePart1();

        // assert
        Assert.That(result, Is.EqualTo(13));
    }
    
    [Test]
    public void Example_Part2_ShouldReturn30()
    {
        // act
        var result = _day04.SolvePart2();

        // assert
        Assert.That(result, Is.EqualTo(30));
    }
}
