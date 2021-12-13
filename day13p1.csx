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

var firstFold = input.First(x => x.StartsWith("fold")).Split("along", StringSplitOptions.RemoveEmptyEntries)[1].Trim();
var axis = firstFold[0];
var along = int.Parse(firstFold.Split('=')[1]);

var visible = new HashSet<(int x, int y)>(dots.Select(p =>
{
    if (axis == 'y')
    {
        return p.y > along ? (x: p.x, y: height - p.y) : p;
    }
    else if (axis == 'x')
    {
        return p.x > along ? (x: width - p.x, p.y) : p;
    }
    return p;
})
.Where(p =>
{
    if (axis == 'y') return p.y != along;
    if (axis == 'x') return p.x != along;
    return true;
}));

WriteLine(visible.Count);