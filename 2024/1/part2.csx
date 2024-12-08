using System.IO;

var lines = File.ReadAllLines("input1.txt");

var left = new List<long>();
var right = new List<long>();
foreach (var line in lines)
{
    var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    left.Add(long.Parse(parts[0]));
    right.Add(long.Parse(parts[1]));
}

var leftGroups = left.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
var rightGroups = right.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
var similarity = leftGroups.Select(kvp =>
{
    if (rightGroups.TryGetValue(kvp.Key, out var rightCount))
    {
        Console.WriteLine($"{kvp.Key} {kvp.Value} {rightCount}");
        return rightCount * kvp.Value * kvp.Key;
    }

    return 0;
}).Sum();

Console.WriteLine(similarity);
