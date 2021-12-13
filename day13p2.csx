string[] input = @"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5".Split('\n');
input = File.ReadAllLines("input13.txt");

var dots = input
    .Where(x => x.Contains(","))
    .Select(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries))
    .Select(arr => (x: int.Parse(arr[0]), y: int.Parse(arr[1])))
    .ToArray();
var width = dots.Max(p => p.x);
var height = dots.Max(p => p.y);
var set = new HashSet<(int x, int y)>(dots);

// PrintSet(set, width, height);

foreach (var line in input.Where(line => line.Length > 1 && line[0] == 'f'))
{
    var firstFold = line.Split("along", StringSplitOptions.RemoveEmptyEntries)[1].Trim();

    set = Fold(set, firstFold, width, height);
    width = firstFold[0] == 'y' ? width : (width - 1) / 2;
    height = firstFold[0] == 'x' ? height : (height - 1) / 2;

}
PrintSet(set, width, height);

HashSet<(int x, int y)> Fold(HashSet<(int x, int y)> prev, string instructions, int w, int h)
{
    var axis = instructions[0];
    var along = int.Parse(instructions.Split('=')[1]);

    WriteLine($"Folding {w}x{h} on {axis} along {along}");

    return new HashSet<(int x, int y)>(prev.Select(p =>
    {
        if (axis == 'y')
        {
            return p.y > along ? (x: p.x, y: h - p.y) : p;
        }
        else if (axis == 'x')
        {
            return p.x > along ? (x: w - p.x, p.y) : p;
        }
        return p;
    })
    .Where(p =>
    {
        if (axis == 'y') return p.y != along;
        if (axis == 'x') return p.x != along;
        return true;
    }));
}
void PrintSet(HashSet<(int x, int y)> set, int w, int h)
{
    for (var i = 0; i <= h; ++i)
    {
        var row = new char[w + 1];
        Array.Fill(row, '.');
        foreach (var p in set.Where(p => p.y == i))
            row[p.x] = '#';
        WriteLine(row);
    }

    WriteLine();
}