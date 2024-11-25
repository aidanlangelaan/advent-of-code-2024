using System.Diagnostics;

namespace AdventOfCode.Core;

public class Solver<TDay> where TDay : Challenge<TDay>
{
    private readonly bool _displayPerformance;
    private readonly TDay _day;
    private Stopwatch timer;

    public Solver(bool displayPerformance = false)
    {
        _displayPerformance = displayPerformance;
        
        _day = Activator.CreateInstance<TDay>();
        if (_day == null)
        {
            throw new Exception("Instance not found");
        }

        timer = new Stopwatch();
    }

    public void SolveDayPart1()
    {
        timer.Reset();
        timer.Start();
        
        var result = _day.SolvePart1();
        
        timer.Stop();

        OutputResult(result, 1);
    }

    public void SolveDayPart2()
    {
        timer.Reset();
        timer.Start();
        
        var result = _day.SolvePart2();
        
        timer.Stop();

        OutputResult(result, 2);
    }
    
    private void OutputResult(int result, int part)
    {
        var consoleOutput = $"part {part} result: {result}";
        if (_displayPerformance)
        {
            consoleOutput += $" ({timer.ElapsedMilliseconds}ms)";
        }

        Console.WriteLine(consoleOutput);
    }
}