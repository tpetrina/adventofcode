string input = "";
input = File.ReadAllText("input19_sample1.txt");
input = File.ReadAllText("input19_sample2.txt");
// input = File.ReadAllText("input19.txt");

var transforms = new (Func<Point, Point>, Func<Point, Point>)[]
{
    (p => new Point( p.x,  p.y,  p.z), p => new Point( p.x,  p.y,  p.z) ),
    (p => new Point( p.x,  p.z, -p.y), p => new Point( p.x, -p.z,  p.y) ),
    (p => new Point( p.x, -p.y,  p.z), p => new Point( p.x, -p.y,  p.z) ),
    (p => new Point( p.x, -p.z,  p.y), p => new Point( p.x,  p.z, -p.y) ),

    (p => new Point( p.y, -p.x,  p.z), p => new Point(-p.y,  p.x,  p.z) ),
    (p => new Point( p.y,  p.z,  p.x), p => new Point( p.z,  p.x,  p.y) ),
    (p => new Point( p.y,  p.x,  p.z), p => new Point( p.y,  p.x,  p.z) ),
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
        return (scanner, points);
    })
    .ToArray();

// WriteLine(string.Join("\n", reports[0].points.Select(p => p.ToString())));
// foreach(var t in transforms)
// {
//     WriteLine($"\nTransform:");
//     WriteLine(string.Join("\n", reports[0].points.Select(p => t(p).ToString())));
// }

Solve();

void Solve()
{
    var beacons = new HashSet<Point>(reports[0].points);

    var inversions = new (int, Point, Func<Point, Point>)[reports.Count()];
    // reset stuff
    for (var i = 0; i < reports.Count(); ++i) inversions[i] = (-1, Point.Zero, null);
    // first one is trivial (won't be used anyway)
    inversions[0] = (-1, Point.Zero, transforms[0].Item1);

    for (var i = 0; i < reports.Count() - 1; ++i)
    {
        var report = reports[i];
        WriteLine($"{string.Join("", inversions.Select(x => x.Item3 != null ? "#" : "."))}");
        WriteLine($"\nChecking report {report.scanner}");

        bool wasMatch = false;
        for (var j = i + 1; j < reports.Count(); ++j)
        {
            if (inversions[j].Item1 > -1) continue;

            var report2 = reports[j];

            var (isMatch, ps, inverter) = Check(report.points, report2.points);
            if (isMatch)
            {
                WriteLine($"Overlap between {report.scanner} and {report2.scanner}. S{report2.scanner}={ps}");
                inversions[j] = (i, ps, inverter);

                if (i == 0)
                {
                    foreach (var q in report2.points.Select(p => ps + inverter(p)))
                    {
                        beacons.Add(q);
                    }
                }
                else
                {
                    // ps is in space(i)
                    // points are in their space -> transform to parent
                    var qs = report2.points.Select(p => ps + inverter(p));

                    var prev = i;
                    do
                    {
                        var (grand, ps_prev, inverter_prev) = inversions[prev];
                        var ps0 = ps_prev + inverter_prev(ps);

                        WriteLine($" - S{j} relative to S{prev} is {ps0}");

                        qs = qs.Select(p => ps_prev + inverter_prev(p)).ToList();

                        prev = grand;
                    } while (prev != 0);

                    foreach (var q in qs)
                        beacons.Add(q);
                }

                wasMatch = true;
            }
        }

        if (wasMatch)
        {
            i = -1;
            reports[0].points = new HashSet<Point>(beacons).ToArray();
        }
    }

    reports[0].points = new HashSet<Point>(beacons).ToArray();

    var all = new HashSet<Point>(beacons).Count;

    WriteLine($"{beacons.Count} beacons");
    File.WriteAllLines("beacons.txt", beacons.Select(x => x.ToString()));
}

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

                    WriteLine($"Transformation: {i}");
                    WriteLine($"  Common point: {first}");
                    WriteLine($"  1st space: {invert(first + anchor)}");
                    WriteLine($"  2st space: {first + anchor2}");

                    var ps = invert(anchor - anchor2);
                    WriteLine($"  Scanner is at {ps} (anchor={invert(anchor)}, anchor2={anchor2})");

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