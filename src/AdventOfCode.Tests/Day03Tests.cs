using AdventOfCode.Challenges;
using NUnit.Framework;

namespace AdventOfCode.Tests;

[TestFixture]
public class Day03Test
{
    private Day03 _day03;

    private readonly string[] input1 =
    [
        "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"
    ];
    
    private readonly string[] input2 =
    [
        "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"
    ];

    [SetUp]
    public void Setup()
    {
        _day03 = new Day03();
    }
    
    [Test]
    public void Example_Part1_ShouldReturn161()
    {
        // act
        var result = _day03.PartOne(input1);
    
        // assert
        Assert.That(result, Is.EqualTo(161));
    }

    [Test]
    public void Example_Part2_ShouldReturn48()
    {
        // act
        var result = _day03.PartTwo(input2);

        // assert
        Assert.That(result, Is.EqualTo(48));
    }
}

