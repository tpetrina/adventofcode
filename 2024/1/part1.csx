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

left.Sort();
right.Sort();
var total = left.Zip(right, (l, r) => Math.Abs(r - l)).Sum();
Console.WriteLine(total);
