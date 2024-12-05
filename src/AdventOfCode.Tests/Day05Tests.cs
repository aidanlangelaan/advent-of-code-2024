using AdventOfCode.Challenges;
using NUnit.Framework;

namespace AdventOfCode.Tests;

[TestFixture]
public class Day05Test
{
    private Day05 _day05;

    private readonly string[] input =
    [
        "47|53",
        "97|13",
        "97|61",
        "97|47",
        "75|29",
        "61|13",
        "75|53",
        "29|13",
        "97|29",
        "53|29",
        "61|53",
        "97|53",
        "61|29",
        "47|13",
        "75|47",
        "97|75",
        "47|61",
        "75|61",
        "47|29",
        "75|13",
        "53|13",
        "",
        "75,47,61,53,29",
        "97,61,53,29,13",
        "75,29,13",
        "75,97,47,61,53",
        "61,13,29",
        "97,13,75,29,47",
    ];

    [SetUp]
    public void Setup()
    {
        _day05 = new Day05();
    }

    [Test]
    public void Example_Part1_ShouldReturn143()
    {
        // act
        var result = _day05.PartOne(input);

        // assert
        Assert.That(result, Is.EqualTo(143));
    }

    [Test]
    public void Example_Part2_ShouldReturn123()
    {
        // act
        var result = _day05.PartTwo(input);

        // assert
        Assert.That(result, Is.EqualTo(123));
    }
}