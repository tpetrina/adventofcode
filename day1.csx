using System.IO;
using System.Linq;

var depths = File.ReadAllLines("input1.txt")
    .Select(x => int.Parse(x))
    .ToArray();

var slides = depths
    .Take(depths.Length - 2)
    .Select((x, i) =>
    {
        return depths[i] + depths[i + 1] + depths[i + 2];
    })
    .ToArray();

Console.WriteLine(string.Join(", ", depths.Take(10)));
Console.WriteLine(string.Join(", ", slides.Take(10)));

int count = Enumerable.Range(1, slides.Length - 1)
    .Where(i => slides[i] > slides[i - 1])
    .Count();

Console.WriteLine(count);