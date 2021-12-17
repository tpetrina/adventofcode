long x = 0, y = 0, x1 = 20, x2 = 30, y1 = -10, y2 = -5;

x1 = 211; x2 = 232; y1 = -124; y2 = -69;

List<(long x, long y)> Fly(long vx, long vy)
{
    var line = new List<(long x, long y)> { (0, 0) };

    long px = 0, py = 0;
    while (true)
    {
        px += vx;
        py += vy;

        line.Add((px, py));

        if (x1 <= px && px <= x2 && y1 <= py && py <= y2) break;

        if (py < Math.Min(y1, y2)) break;

        if (vx > 0) vx--;
        else if (vx < 0) vx++;
        vy--;
    }

    return line;
}

var result = long.MinValue;
var result2 = 0;
for (var i = -1000; i <= 1000; ++i)
{
    for (var j = -1000; j <= 1000; ++j)
    {
        var path = Fly(i, j);
        var success = InTarget(path.Last().x, path.Last().y);

        if (success)
        {
            result2++;
            var topy = path.Select(p => p.y).Max();
            WriteLine($"Success for ({i},{j}). Top y={topy}");
            result = Math.Max(topy, result);
        }
    }
}
WriteLine(result);
WriteLine(result2);

bool InTarget(long px, long py) => x1 <= px && px <= x2 && y1 <= py && py <= y2;


void Draw(List<(long x, long y)> line)
{
    var maxx = line.Select(p => p.x).Max();
    var maxy = line.Select(p => p.y).Max();
    var minx = line.Select(p => p.x).Min();
    var miny = line.Select(p => p.y).Min();

    WriteLine(string.Join(",", line.Select(p => $"({p.x}, {p.y})")));

    maxy = new[] { maxy, miny, y1, y2 }.Max();
    miny = new[] { maxy, miny, y1, y2 }.Min();
    maxx = new[] { maxx, minx, x1, x2 }.Max();
    minx = new[] { maxx, minx, x1, x2 }.Min();
    WriteLine($"{minx}..{maxx}, {miny}..{maxy}");

    var map = new char[maxy - miny + 1][];
    var width = maxx - minx + 1;
    for (var i = maxy; i >= miny; --i)
    {
        var row = new string('.', (int)width);
        map[maxy - i] = row.ToCharArray();
    }

    for (var i = x1; i <= x2; ++i)
    {
        for (var j = y1; j <= y2; ++j)
        {
            map[maxy - j][i] = 'T';
        }
    }

    map[maxy][minx] = 'S';
    for (var i = 1; i < line.Count; ++i)
    {
        var (x, y) = line[i];
        map[maxy - y][x] = '#';
    }
    WriteLine(string.Join('\n', map.Select(row => new string(row))));
}