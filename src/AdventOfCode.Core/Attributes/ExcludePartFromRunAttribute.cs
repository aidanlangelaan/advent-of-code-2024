namespace AdventOfCode.Core.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class ExcludePartFromRunAttribute : Attribute
{
    public int Part { get; }

    public ExcludePartFromRunAttribute(int part)
    {
        if (part != 1 && part != 2)
        {
            throw new ArgumentException("Part must be 1 or 2.");
        }
        Part = part;
    }
}