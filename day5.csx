using System.Reflection;
using System.Text.RegularExpressions;

string[] input = @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2".Split('\n', StringSplitOptions.RemoveEmptyEntries);
input = File.ReadAllLines("input5.txt");

void Part1()
{
    var parser = new Regex(@"(\d*),(\d*) -> (\d*),(\d*)");
    var valid = input
        .Select(line =>
        {
            var match = parser.Match(line);
            int x1 = int.Parse(match.Groups[1].Value);
            int y1 = int.Parse(match.Groups[2].Value);
            int x2 = int.Parse(match.Groups[3].Value);
            int y2 = int.Parse(match.Groups[4].Value);

            return (x1, y1, x2, y2);
        })
        .Where(line => line.x1 == line.x2 || line.y1 == line.y2)
        .Select(line =>
        {
            // always orient them left to right, top to bottom
            if (line.x1 == line.x2)
            {
                return (line.x1, y1: Math.Min(line.y1, line.y2), line.x2, y2: Math.Max(line.y1, line.y2));
            }
            else
            {
                return (x1: Math.Min(line.x1, line.x2), line.y1, x2: Math.Max(line.x1, line.x2), line.y2);
            }
        })
        .ToList();

    Dump(valid);

    var overlaps = new Dictionary<int, Dictionary<int, bool>>();
    void Add(int x, int y)
    {
        Console.WriteLine($"({x},{y})");
        if (!overlaps.TryGetValue(x, out var d))
        {
            overlaps[x] = d = new Dictionary<int, bool>();
        }
        d[y] = true;
    }

    for (var i = 0; i < valid.Count; ++i)
    {
        var line1 = valid[i];

        for (var j = i + 1; j < valid.Count; ++j)
        {
            var line2 = valid[j];

            if (line1.x1 == line1.x2)
            {
                // vertical line1 (y1-y2)
                if (line2.x1 == line2.x2)
                {
                    //vertical line2
                    if (line1.x1 == line2.x1)
                    {
                        if (!(line1.y2 < line2.y1 || line1.y1 > line2.y2))
                        {
                            var overlap = Math.Min(line1.y2, line2.y2) - Math.Max(line1.y1, line2.y1) + 1;
                            WriteLine($"Vertical overlap between {i} and {j} by {overlap}");
                            for (var y = Math.Max(line1.y1, line2.y1); y <= Math.Min(line1.y2, line2.y2); ++y)
                                Add(line1.x1, y);
                        }
                    }
                }
                else
                {
                    // horizontal line2 (x1-x2)
                    if (line2.x1 <= line1.x1 && line1.x1 <= line2.x2 &&
                        line1.y1 <= line2.y1 && line2.y1 <= line1.y2)
                    {
                        WriteLine($"Lines {i} and {j} intersect (1)");
                        Add(line1.y1, line2.x1);
                    }
                }
            }
            else
            {
                // horizontal line1
                if (line2.y1 == line2.y2)
                {
                    // horizontal line2
                    if (line1.y1 == line2.y1)
                    {
                        if (!(line1.x2 < line2.x1 || line1.x1 > line2.x2))
                        {
                            var overlap = Math.Min(line1.x2, line2.x2) - Math.Max(line1.x1, line2.x1) + 1;
                            WriteLine($"Horizontal overlap between {i} and {j} by {overlap}");
                            for (var x = Math.Max(line1.x1, line2.x1); x <= Math.Min(line1.x2, line2.x2); ++x)
                                Add(x, line1.y1);
                        }
                    }
                }
                else
                {
                    // vertical line2
                    // line1.y1 == line1.y2
                    // line2.x1 == line2.x2
                    if (line1.x1 <= line2.x1 && line2.x1 <= line2.x2 &&
                        line2.y1 <= line1.y1 && line1.y1 <= line2.y2)
                    {
                        WriteLine($"Lines {i} and {j} intersect (2)");
                        Add(line2.x1, line1.y1);
                    }
                }
            }
        }
    }

    WriteLine($"{overlaps.Sum(d => d.Value.Count)}");
}

void Part1b()
{
    var parser = new Regex(@"(\d*),(\d*) -> (\d*),(\d*)");
    var valid = input
        .Select(line =>
        {
            var match = parser.Match(line);
            int x1 = int.Parse(match.Groups[1].Value);
            int y1 = int.Parse(match.Groups[2].Value);
            int x2 = int.Parse(match.Groups[3].Value);
            int y2 = int.Parse(match.Groups[4].Value);

            return (x1, y1, x2, y2);
        })
        .Where(line => line.x1 == line.x2 || line.y1 == line.y2)
        .Select(line =>
        {
            // always orient them left to right, top to bottom
            if (line.x1 == line.x2)
            {
                return (line.x1, y1: Math.Min(line.y1, line.y2), line.x2, y2: Math.Max(line.y1, line.y2));
            }
            else
            {
                return (x1: Math.Min(line.x1, line.x2), line.y1, x2: Math.Max(line.x1, line.x2), line.y2);
            }
        })
        .ToList();

    var maxx = valid.Select(line => Math.Max(line.x1, line.x2)).Max();
    var maxy = valid.Select(line => Math.Max(line.y1, line.y2)).Max();
    WriteLine($"Grid {maxx}x{maxy}");
    var grid = new int[maxx + 1, maxy + 1];
    foreach (var line in valid)
    {
        if (line.x1 == line.x2)
        {
            for (int y = line.y1; y <= line.y2; ++y)
                grid[line.x1, y]++;
        }
        else
        {
            for (int x = line.x1; x <= line.x2; ++x)
                grid[x, line.y1]++;
        }
    }

    int count = 0;
    for (var i = 0; i < maxx; i++)
        for (var j = 0; j < maxy; ++j)
            if (grid[i, j] > 1)
                count++;
    WriteLine(count);
}


void Part2()
{
    var parser = new Regex(@"(\d*),(\d*) -> (\d*),(\d*)");
    var valid = input
        .Select(line =>
        {
            var match = parser.Match(line);
            int x1 = int.Parse(match.Groups[1].Value);
            int y1 = int.Parse(match.Groups[2].Value);
            int x2 = int.Parse(match.Groups[3].Value);
            int y2 = int.Parse(match.Groups[4].Value);

            return (x1, y1, x2, y2);
        })
        .ToList();

    var maxx = valid.Select(line => Math.Max(line.x1, line.x2)).Max();
    var maxy = valid.Select(line => Math.Max(line.y1, line.y2)).Max();
    WriteLine($"Grid {maxx}x{maxy}");
    var grid = new int[maxx + 1, maxy + 1];
    foreach (var line in valid)
    {
        if (line.x1 == line.x2)
        {
            for (int y = Math.Min(line.y1, line.y2); y <= Math.Max(line.y1, line.y2); ++y)
                grid[line.x1, y]++;
        }
        else if (line.y1 == line.y2)
        {
            for (int x = Math.Min(line.x1, line.x2); x <= Math.Max(line.x1, line.x2); ++x)
                grid[x, line.y1]++;
        }
        else
        {
            var length = Math.Abs(line.x2 - line.x1);
            var dx = line.x2 > line.x1 ? 1 : -1;
            var dy = line.y2 > line.y1 ? 1 : -1;

            var x = line.x1;
            var y = line.y1;
            WriteLine($"({line.x1},{line.y1}) -> ({line.x2},{line.y2}) ({dx},{dy}), {length}");
            for (var i = 0; i <= length; ++i, x += dx, y += dy)
            {
                grid[x, y]++;
            }
        }
    }

    int count = 0;
    for (var i = 0; i <= maxx; i++)
    {
        for (var j = 0; j <= maxy; ++j)
        {
            if (grid[i, j] > 1)
            {
                count++;
            }
            // Write(grid[i, j] == 0 ? "." : grid[i, j].ToString());
        }
        // WriteLine();
    }
    WriteLine(count);

}
// Part1();
// Part1b();

Part2();


IEnumerable<T> Dump<T>(IEnumerable<T> enumerable)
{
    WriteLine("*** DUMP ***");
    int i = 0;
    foreach (var element in enumerable)
    {
        Write($"{i}: ");
        if (element is null) WriteLine("null");
        else
            WriteLine(@$"{{ {string.Join(", ", element
                .GetType()
                .GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Select(prop => $"{prop.Name}: {prop.GetValue(element)}"))} }}");
        i++;
    }
    WriteLine("*** END DUMP ***");
    return enumerable;
}
