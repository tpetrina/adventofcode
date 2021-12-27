using System.Diagnostics;

string input = "";
input = File.ReadAllText("input19_sample1.txt");
input = File.ReadAllText("input19_sample2.txt");
input = File.ReadAllText("input19.txt");

var transforms = new (Func<Point, Point>, Func<Point, Point>)[]
{
    (p => new Point( p.x,  p.y,  p.z), p => new Point( p.x,  p.y,  p.z) ),
    (p => new Point( p.x,  p.z, -p.y), p => new Point( p.x, -p.z,  p.y) ),
    (p => new Point( p.x, -p.y, -p.z), p => new Point( p.x, -p.y, -p.z) ),
    (p => new Point( p.x, -p.z,  p.y), p => new Point( p.x,  p.z, -p.y) ),

    (p => new Point( p.y, -p.x,  p.z), p => new Point(-p.y,  p.x,  p.z) ),
    (p => new Point( p.y,  p.z,  p.x), p => new Point( p.z,  p.x,  p.y) ),
    (p => new Point( p.y,  p.x, -p.z), p => new Point( p.y,  p.x, -p.z) ),
    (p => new Point( p.y, -p.z, -p.x), p => new Point(-p.z,  p.x, -p.y) ),

    (p => new Point( p.z, -p.x, -p.y), p => new Point(-p.y, -p.z,  p.x) ),
    (p => new Point( p.z, -p.y,  p.x), p => new Point( p.z, -p.y,  p.x) ),
    (p => new Point( p.z,  p.x,  p.y), p => new Point( p.y,  p.z,  p.x) ),
    (p => new Point( p.z,  p.y, -p.x), p => new Point(-p.z,  p.y,  p.x) ),

    (p => new Point(-p.x,  p.y, -p.z), p => new Point(-p.x,  p.y, -p.z) ),
    (p => new Point(-p.x,  p.z,  p.y), p => new Point(-p.x,  p.z,  p.y) ),
    (p => new Point(-p.x, -p.y,  p.z), p => new Point(-p.x, -p.y,  p.z) ),
    (p => new Point(-p.x, -p.z, -p.y), p => new Point(-p.x, -p.z, -p.y) ),

    (p => new Point(-p.y,  p.x,  p.z), p => new Point( p.y, -p.x,  p.z) ),
    (p => new Point(-p.y,  p.z, -p.x), p => new Point(-p.z, -p.x,  p.y) ),
    (p => new Point(-p.y, -p.x, -p.z), p => new Point(-p.y, -p.x, -p.z) ),
    (p => new Point(-p.y, -p.z,  p.x), p => new Point( p.z, -p.x, -p.y) ),

    (p => new Point(-p.z,  p.x, -p.y), p => new Point( p.y, -p.z, -p.x) ),
    (p => new Point(-p.z, -p.y, -p.x), p => new Point(-p.z, -p.y, -p.x) ),
    (p => new Point(-p.z, -p.x,  p.y), p => new Point(-p.y,  p.z, -p.x) ),
    (p => new Point(-p.z,  p.y,  p.x), p => new Point( p.z,  p.y, -p.x) ),
};

var reports = input
    .Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
    .Select(report =>
    {
        var lines = report.Split('\n');
        var scanner = lines[0].Split("scanner ")[1].Split(" ")[0];
        var points = lines
            .Skip(1)
            .Select(line => line.Split(",").Select(int.Parse).ToArray())
            .Select(x => new Point(x[0], x[1], x[2]))
            .ToArray();
        return (scanner, points, scanners: new List<Point> { new Point(0, 0, 0) });
    })
    .ToArray();

for (var i = 0; i < transforms.Length; ++i)
{
    var (t, inverse) = transforms[i];
    var p = new Point(1, 2, -3);
    var p1 = t(p);
    var p2 = inverse(p1);
    if (p2 != p)
    {
        WriteLine($"Transform {i} doesn't work!");
    }
}

Solve();

void Solve()
{
    var beacons = new HashSet<Point>(reports[0].points);

    var done = new bool[reports.Length];
    done[0] = true;

    while (true)
    {
        bool wasMatch = false;
        for (var i = 0; i < reports.Count() - 1; ++i)
        {
            WriteLine($"{string.Join("", done.Select(x => x ? "#" : "."))}");
            WriteLine($"\nChecking report {reports[i].scanner}");

            for (var j = i + 1; j < reports.Count(); ++j)
            {
                if (done[j]) continue;

                var (isMatch, ps, inverter) = Check(reports[i].points, reports[j].points);

                if (isMatch)
                {
                    done[j] = true;
                    WriteLine($"Overlap between {reports[i].scanner} and {reports[j].scanner}. S{reports[j].scanner}={ps}");

                    WriteLine($"  Before ({i}): {reports[i].points.Length}");
                    reports[i].points = new HashSet<Point>(reports[i].points.Union(reports[j].points.Select(p => inverter(ps + p)))).ToArray();
                    reports[i].scanners.AddRange(reports[j].scanners.Select(s => inverter(ps + s)));
                    WriteLine($"  After ({i}): {reports[i].points.Length}");
                    WriteLine();

                    wasMatch = true;
                }
            }
        }

        if (!wasMatch)
        {
            break;
        }
    }

    WriteLine($"{reports[0].points.Length} beacons");
    for (var i = 0; i < done.Length; ++i)
    {
        WriteLine($"Scanner {reports[i].scanner}: {reports[i].points.Length}");
        if (!done[i])
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine($"Missing solution for {i}!");
        }
    }

    WriteLine($"Scanners: {string.Join(", ", reports[0].scanners)}");
    var max = reports[0].scanners
        .Select(s => reports[0].scanners.Select(s2 => dist(s, s2)).Max())
        .Max();
    WriteLine($"Max: {max}");
}

long dist(Point p, Point q)
    => Math.Abs(p.x - q.x) + Math.Abs(p.y - q.y) + Math.Abs(p.z - q.z);

(bool isMatch, Point ps, Func<Point, Point> inverter) Check(IEnumerable<Point> points, IEnumerable<Point> points2)
{
    for (var i = 0; i < transforms.Length; ++i)
    {
        var t = transforms[i].Item1;
        var invert = transforms[i].Item2;

        var transformed = points.Select(p => t(p)).ToArray();
        foreach (var anchor2 in points2)
        {
            var rebased2 = new HashSet<Point>(points2.Select(p => p - anchor2));

            foreach (var anchor in transformed)
            {
                var rebased = new HashSet<Point>(transformed.Select(p => p - anchor));

                var common = GetCommon(rebased2, rebased).ToList();
                if (common.Count >= 12)
                {
                    var first = common.First();

                    WriteLine($"  Transformation: {i}");
                    WriteLine($"  1st space: {invert(first + anchor)}");
                    WriteLine($"  2st space: {first + anchor2}");

                    var ps = anchor - anchor2;
                    WriteLine($"  Scanner is at {invert(ps)}");

                    return (true, ps, invert);
                }
            }
        }
    }

    return (false, Point.Zero, null);
}

IEnumerable<Point> GetCommon(HashSet<Point> p, HashSet<Point> q)
{
    return p.Where(x => q.Contains(x)).ToList();
}



record Point(int x, int y, int z)
{
    public static Point operator +(Point p1, Point p2)
        => new Point(p1.x + p2.x, p1.y + p2.y, p1.z + p2.z);
    public static Point operator -(Point p1, Point p2)
        => new Point(p1.x - p2.x, p1.y - p2.y, p1.z - p2.z);
    public override string ToString()
        => $"({x},{y},{z})";
    public static Point Zero = new Point(0, 0, 0);
}