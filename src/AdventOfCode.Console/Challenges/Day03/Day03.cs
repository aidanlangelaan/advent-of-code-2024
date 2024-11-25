using System.ComponentModel;
using AdventOfCode.Core;

namespace AdventOfCode.Challenges;

[Description("Day 03")]
public class Day03 : Challenge<Day03>
{
    public Day03(string[] input) : base(input)
    {
    }

    public Day03() : base()
    {
    }
    
    public override int SolvePart1()
    {
        var symbols = FindSymbols();
        
        // for every symbol, get the adjacent numbers
        var foundParts = new List<int>();
        foreach (var symbol in symbols)
        {
            var adjacentNumbers = GetAdjacentNumbers(symbol);
            if (adjacentNumbers.Count == 0)
            {
                continue;
            }

            adjacentNumbers.Select(x => Convert.ToInt32(x))
                .ToList()
                .ForEach(x => foundParts.Add(x));
        }

        return foundParts.Sum();
    }

    public override int SolvePart2()
    {
        var symbols = FindSymbols('*');

        // for every symbol, check get its adjacent numbers
        var ratios = new List<int>();
        foreach (var symbol in symbols)
        {
            var adjacentNumbers = GetAdjacentNumbers(symbol);
            if (adjacentNumbers.Count == 0)
            {
                continue;
            }

            var numbers = adjacentNumbers.Select(x => Convert.ToInt32(x))
                .ToList();

            if (numbers.Count == 2)
            {
                ratios.Add(numbers[0] * numbers[1]);
            }
        }

        return ratios.Sum();
    }

    private List<(int x, int y, char symbol)> FindSymbols(char? filter = null)
    {
        // get all symbols
        var symbols = new List<(int x, int y, char symbol)>();
        foreach (var s in _input)
        {
            for (var i = 0; i < s.Length; i++)
            {
                if (!char.IsNumber(s[i]) && s[i] != '.' && (filter == null || s[i] == filter))
                {
                    symbols.Add((i, Array.IndexOf(_input, s), s[i]));
                }
            }
        }

        return symbols;
    }

    private List<string> GetAdjacentNumbers((int x, int y, char symbol) symbol)
    {
        var adjacentNumbers = new List<string>();

        // check top left
        if (symbol is { y: > 0, x: > 0 })
        {
            var topLeft = _input[symbol.y - 1][symbol.x - 1];
            if (char.IsNumber(topLeft))
            {
                var foundNumber = GetNumber(_input[symbol.y - 1], symbol.x - 1);
                if (!adjacentNumbers.Contains(foundNumber))
                {
                    adjacentNumbers.Add(foundNumber);
                }
            }
        }

        // check top
        if (symbol.y > 0)
        {
            var top = _input[symbol.y - 1][symbol.x];
            if (char.IsNumber(top))
            {
                var foundNumber = GetNumber(_input[symbol.y - 1], symbol.x);
                if (!adjacentNumbers.Contains(foundNumber))
                {
                    adjacentNumbers.Add(foundNumber);
                }
            }
        }

        // check top right
        if (symbol.y > 0 && symbol.x < _input[symbol.y - 1].Length - 1)
        {
            var topRight = _input[symbol.y - 1][symbol.x + 1];
            if (char.IsNumber(topRight))
            {
                var foundNumber = GetNumber(_input[symbol.y - 1], symbol.x + 1);
                if (!adjacentNumbers.Contains(foundNumber))
                {
                    adjacentNumbers.Add(foundNumber);
                }
            }
        }

        // check left
        if (symbol.x > 0)
        {
            var left = _input[symbol.y][symbol.x - 1];
            if (char.IsNumber(left))
            {
                var foundNumber = GetNumber(_input[symbol.y], symbol.x - 1);
                if (!adjacentNumbers.Contains(foundNumber))
                {
                    adjacentNumbers.Add(foundNumber);
                }
            }
        }

        // check right
        if (symbol.x < _input[symbol.y].Length - 1)
        {
            var right = _input[symbol.y][symbol.x + 1];
            if (char.IsNumber(right))
            {
                var foundNumber = GetNumber(_input[symbol.y], symbol.x + 1);
                if (!adjacentNumbers.Contains(foundNumber))
                {
                    adjacentNumbers.Add(foundNumber);
                }
            }
        }

        // check bottom left
        if (symbol.y < _input.Length - 1 && symbol.x > 0)
        {
            var bottomLeft = _input[symbol.y + 1][symbol.x - 1];
            if (char.IsNumber(bottomLeft))
            {
                var foundNumber = GetNumber(_input[symbol.y + 1], symbol.x - 1);
                if (!adjacentNumbers.Contains(foundNumber))
                {
                    adjacentNumbers.Add(foundNumber);
                }
            }
        }

        // check bottom
        if (symbol.y < _input.Length - 1)
        {
            var bottom = _input[symbol.y + 1][symbol.x];
            if (char.IsNumber(bottom))
            {
                var foundNumber = GetNumber(_input[symbol.y + 1], symbol.x);
                if (!adjacentNumbers.Contains(foundNumber))
                {
                    adjacentNumbers.Add(foundNumber);
                }
            }
        }

        // check bottom right
        if (symbol.y < _input.Length - 1 && symbol.x < _input[symbol.y + 1].Length - 1)
        {
            var bottomRight = _input[symbol.y + 1][symbol.x + 1];
            if (char.IsNumber(bottomRight))
            {
                var foundNumber = GetNumber(_input[symbol.y + 1], symbol.x + 1);
                if (!adjacentNumbers.Contains(foundNumber))
                {
                    adjacentNumbers.Add(foundNumber);
                }
            }
        }

        return adjacentNumbers;
    }

    private string GetNumber(string line, int x)
    {
        Dictionary<int, char> numbers = new();
        
        numbers.Add(x, line[x]);
        
        // check left
        for (var i = x - 1; i >= 0; i--)
        {
            if (char.IsNumber(line[i]))
            {
                numbers.Add(i, line[i]);
            }
            else
            {
                break;
            }
        }

        // check right
        for (var i = x + 1; i < line.Length; i++)
        {
            if (char.IsNumber(line[i]))
            {
                numbers.Add(i, line[i]);
            }
            else
            {
                break;
            }
        }

        return numbers.OrderBy(i => i.Key)
            .Select(n => n.Value)
            .Aggregate("", (current, next) => $"{current}{next}");
    }
}
