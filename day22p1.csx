string[] input = @"on x=10..12,y=10..12,z=10..12
on x=11..13,y=11..13,z=11..13
off x=9..11,y=9..11,z=9..11
on x=10..10,y=10..10,z=10..10".Split('\n');
input = @"on x=-20..26,y=-36..17,z=-47..7
on x=-20..33,y=-21..23,z=-26..28
on x=-22..28,y=-29..23,z=-38..16
on x=-46..7,y=-6..46,z=-50..-1
on x=-49..1,y=-3..46,z=-24..28
on x=2..47,y=-22..22,z=-23..27
on x=-27..23,y=-28..26,z=-21..29
on x=-39..5,y=-6..47,z=-3..44
on x=-30..21,y=-8..43,z=-13..34
on x=-22..26,y=-27..20,z=-29..19
off x=-48..-32,y=26..41,z=-47..-37
on x=-12..35,y=6..50,z=-50..-2
off x=-48..-32,y=-32..-16,z=-15..-5
on x=-18..26,y=-33..15,z=-7..46
off x=-40..-22,y=-38..-28,z=23..41
on x=-16..35,y=-41..10,z=-47..6
off x=-32..-23,y=11..30,z=-14..3
on x=-49..-5,y=-3..45,z=-29..18
off x=18..30,y=-20..-8,z=-3..13
on x=-41..9,y=-7..43,z=-33..15
on x=-54112..-39298,y=-85059..-49293,z=-27449..7877
on x=967..23432,y=45373..81175,z=27513..53682".Split('\n');
input = File.ReadAllLines("input22.txt");

var map = new bool[101, 101, 101];
foreach (var line in input)
{
    var op = line.Split(' ')[0];
    var ds = line
        .Split(' ')[1]
        .Split(',')
        .Select(d =>
        {
            var dims = d.Split('=')[1].Split("..");
            return (s: int.Parse(dims[0]), e: int.Parse(dims[1]));
        })
        .ToArray();

    // WriteLine($"{op} x={ds[0].s}..{ds[0].e},x={ds[1].s}..{ds[1].e},x={ds[2].s}..{ds[2].e}");
    if (ds[0].s > 50 || ds[0].e < -50 ||
        ds[2].s > 50 || ds[1].e < -50 ||
        ds[1].s > 50 || ds[2].e < -50) continue;

    var v = op == "on";
    for (var i = ds[0].s; i <= ds[0].e; ++i)
    {
        for (var j = ds[1].s; j <= ds[1].e; ++j)
        {
            for (var k = ds[2].s; k <= ds[2].e; ++k)
            {
                map[i + 50, j + 50, k + 50] = v;
            }
        }
    }
}

var counter = 0;
foreach (var x in map)
{
    if (x) counter++;
}

WriteLine(counter);