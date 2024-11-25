using System.ComponentModel;

namespace AdventOfCode.Core;

public abstract class Challenge<TDay>
{
    protected string[] _input;

    protected Challenge(string[] Input)
    {
        PrintDescription();
        _input = Input;
    }

    protected Challenge()
    {
        PrintDescription();
        _input = File.ReadAllLines($"Challenges\\{typeof(TDay).Name}\\Input.txt");
    }

    public abstract int SolvePart1();
    public abstract int SolvePart2();
    
    private static void PrintDescription()
    {
        var descriptions = (DescriptionAttribute[])typeof(TDay).GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (descriptions.Length != 0)
        {
            Console.WriteLine($"\r\n- {descriptions.First().Description} -");
        }
    }
}