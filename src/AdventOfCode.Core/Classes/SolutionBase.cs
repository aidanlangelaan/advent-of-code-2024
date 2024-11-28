namespace AdventOfCode.Core.Classes;

public abstract class SolutionBase : ISolution
{
    public abstract int Day { get; }
    public abstract object PartOne(string[] input);
    public abstract object PartTwo(string[] input);

    private string[] ReadInput(string fileName)
    {
        var filePath = Path.Combine("Challenges", $"Day{Day:D2}", fileName);
        return File.Exists(filePath) ? File.ReadAllLines(filePath) : [];
    }

    public (string[] InputPart1, string[]? InputPart2) GetInputs()
    {
        var inputPart1 = ReadInput("Input1.txt");
        var inputPart2 = ReadInput("Input2.txt");
        return (inputPart1, inputPart2.Length > 0 ? inputPart2 : null);
    }
}