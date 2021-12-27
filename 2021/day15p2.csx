string input = @"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581";
input = File.ReadAllText("input15.txt");

var map = input
    .Split('\n')
    .Select(x => x.Replace("\n", ""))
    .Select(x => x.Replace("\r", ""))
    .Select(row => row.ToCharArray().Select(ch => ch - '0').ToArray())
    .ToArray();

var w = map[0].Length;
var h = map.Length;
// WriteLine(string.Join('\n', map.Select(row => string.Join("", row))));

int[][] map2 = new int[h * 5][];
for (var i = 0; i < h * 5; i++) map2[i] = new int[w * 5];

for (var i = 0; i < 5; i++)
{
    for (var j = 0; j < 5; j++)
    {
        for (var x = 0; x < w; ++x)
        {
            for (var y = 0; y < h; ++y)
            {
                var n = map[y][x] + i + j;
                if (n > 9) n -= 9;
                map2[y + j * h][x + i * w] = n;
            }
        }
    }
}
map = map2;
w = w * 5;
h = h * 5;

// WriteLine(string.Join('\n', map.Select(row => string.Join("", row))));


var mins = new int[w * h];
Array.Fill(mins, int.MaxValue);

mins[0] = 0;

var toVisit = new Queue<(int x, int y)>();
toVisit.Enqueue((x: 0, y: 0));
var movements = new[]
{
    (dx: -1, dy: 0),
    (dx: 1, dy: 0),
    (dx: 0, dy: -1),
    (dx: 0, dy: 1),
};

while (toVisit.Any())
{
    var (x, y) = toVisit.Dequeue();
    var curr = x + y * w;

    foreach (var (dx, dy) in movements)
    {
        var x2 = x + dx;
        var y2 = y + dy;

        if (x2 >= 0 && x2 < w && y2 >= 0 && y2 < h)
        {
            // in bounds
            var curr2 = x2 + y2 * w;
            if (map[y2][x2] + mins[curr] < mins[curr2])
            {
                mins[curr2] = map[y2][x2] + mins[curr];
                toVisit.Enqueue((x: x2, y: y2));
            }
        }
    }
}

//WriteLine(string.Join("\n", Enumerable.Range(0, h).Select(row => string.Join("", Enumerable.Range(0, w).Select(col => $"{mins[col + row * w],5}")))));

WriteLine(mins[w - 1 + (h - 1) * w]);
