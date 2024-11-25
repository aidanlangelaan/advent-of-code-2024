using AdventOfCode.Challenges;

namespace AdventOfCode.Tests;

public class Day01Tests
{
    private Day01 _day01;
    
    [SetUp]
    public void Setup()
    {
        // var testInput = new[]
        // {
        //     "1abc2",
        //     "pqr3stu8vwx",
        //     "a1b2c3d4e5f",
        //     "treb7uchet"
        // };
        
        var testInput = new[]
        {
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen"
        };
        
        _day01 = new Day01(testInput);
    }

    // COMMENTED OUT BECAUSE OF NOT SUPPORTING DIFFERENT TEST SETS FOR PART 1 AND PART 2
    // [Test]
    // public void Example_Part1_ShouldReturn142()
    // {
    //     // act
    //     var result = _day01.SolvePart1();
    //
    //     // assert
    //     Assert.That(result, Is.EqualTo(142));
    // }
    
    [Test]
    public void Example_Part2_ShouldReturn281()
    {
        // act
        var result = _day01.SolvePart2();

        // assert
        Assert.That(result, Is.EqualTo(281));
    }
}