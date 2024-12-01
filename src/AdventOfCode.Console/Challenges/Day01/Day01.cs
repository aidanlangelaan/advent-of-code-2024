using AdventOfCode.Core.Classes;

namespace AdventOfCode.Challenges;

public class Day01 : SolutionBase
{
    public override int Day => 01;

    public override object PartOne(string[] input)
    {
        var (listA, listB) = ParseLists(input);

        listA.Sort();
        listB.Sort();

        return listA.Zip(listB, (a, b) => Math.Abs(a - b)).Sum();
    }

    public override object PartTwo(string[] input)
    {
        var (listA, listB) = ParseLists(input);

        // Predetermine the counts of each number in listB
        var counts = listB.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

        // Use the predetermined counts rather than counting each time
        return listA.Sum(num => num * counts.GetValueOrDefault(num, 0));
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