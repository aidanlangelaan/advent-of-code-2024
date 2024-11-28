namespace AdventOfCode.Core;

public interface ISolution
{
    int Day { get; }
    object PartOne(string[] input);
    object PartTwo(string[] input);
}