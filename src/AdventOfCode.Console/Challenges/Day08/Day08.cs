using System.ComponentModel;
using System.Text.RegularExpressions;
using AdventOfCode.Core;

namespace AdventOfCode.Challenges;

[Description("Day 08")]
public class Day08 : Challenge<Day08>
{
    public Day08(string[] input) : base(input)
    {
    }

    public Day08() : base()
    {
    }

    public override int SolvePart1()
    {
        // parse directions
        var directions = _input[0].ToArray();

        // parse nodes
        var nodes = ParseNodes();

        // loop through directions and count how many steps are needed to go from name AAA to name ZZZ
        var steps = 0;
        var found = false;
        var current = nodes["AAA"];
        while (!found)
        {
            foreach (var direction in directions)
            {
                steps++;
                current = nodes[direction == 'L' ? current.Left : current.Right];

                if (current.Name == "ZZZ")
                {
                    found = true;
                    break;
                }
            }
        }

        return steps;
    }

    public override int SolvePart2()
    {
        // parse directions
        var directions = _input[0].ToArray();
        
        // parse nodes
        var nodes = ParseNodes();
        
        // get all indexes of nodes where name ends in A
        var startNodes = nodes
            .Where(x => x.Key.EndsWith('A'))
            .Select(x => x.Value)
            .ToArray();
        
        var nodeSteps = new List<int>();
        foreach (var startNode in startNodes)
        {
            var steps = 0;
            var found = false;
            var current = startNode;
            while (!found)
            {
                foreach (var direction in directions)
                {
                    steps++;
                    current = nodes[direction == 'L' ? current.Left : current.Right];

                    if (current.Name.EndsWith('Z'))
                    {
                        found = true;
                        break;
                    }
                }
            }
            
            nodeSteps.Add(steps);
        }
        
        // get minimal LCM
        var lcm = LeastCommonMultiple(nodeSteps[0], nodeSteps[1]);
        for (var i = 2; i < nodeSteps.Count; i++)
        {
            lcm = LeastCommonMultiple(lcm, nodeSteps[i]);
        }
        
        Console.WriteLine(lcm);
        return 0;
    }

    private Dictionary<string, Node> ParseNodes()
    {
        var nodes = new List<Node>();
        for (var i = 2; i < _input.Length; i++)
        {
            var values = Regex.Replace(_input[i], @"[\s()=,]", ",")
                .Split(",")
                .Where(x => !string.IsNullOrEmpty(x)).ToArray();
            nodes.Add(new Node(values[0], values[1], values[2]));
        }
        return nodes.ToDictionary(x => x.Name);
    }
    
    private long GreatestCommonDivisor(long a, long b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        
        return a;
    }
 
    private long LeastCommonMultiple(long a, long b)
    {
        return Math.Abs(a * b) / GreatestCommonDivisor(a, b);
    }

    private class Node(string name, string left, string right)
    {
        public string Name { get; } = name;

        public string Left { get; } = left;

        public string Right { get; } = right;
    }
}