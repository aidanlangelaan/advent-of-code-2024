using AdventOfCode.Core.Classes;

namespace AdventOfCode.Challenges;

public class Day01 : SolutionBase
{
    public override int Day => 01;

    public override object PartOne(string[] input)
    {
        var (listA, listB) = ParseLists(input);

        var diffs = new List<int>();
        while (listA.Count > 0)
        {
            var a = listA.Min();
            var b = listB.Min();

            diffs.Add(Math.Abs(a - b));

            listA.Remove(a);
            listB.Remove(b);
        }

        return diffs.Sum();
    }

    public override object PartTwo(string[] input)
    {
        var (listA, listB) = ParseLists(input);
        return listA.Sum(num => num * listB.Count(x => x == num));
    }

    private static (List<int> listA, List<int> listB) ParseLists(string[] input)
    {
        var listA = new List<int>();
        var listB = new List<int>();
        foreach (var line in input)
        {
            var nums = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            listA.Add(nums[0]);
            listB.Add(nums[1]);
        }

        return (listA, listB);
    }
}