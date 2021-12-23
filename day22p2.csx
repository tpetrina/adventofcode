string[] input = @"on x=10..12,y=10..12,z=10..12
on x=11..13,y=11..13,z=11..13
off x=9..11,y=9..11,z=9..11
on x=10..10,y=10..10,z=10..10".Split('\n');

Cuboid overlap(Cuboid c1, Cuboid c2)
{
    var c = new Cuboid(
        Math.Max(c1.x1, c2.x1), Math.Min(c1.x2, c2.x2),
        Math.Max(c1.y1, c2.y1), Math.Min(c1.y2, c2.y2),
        Math.Max(c1.z1, c2.z1), Math.Min(c1.z2, c2.z2),
        true
    );
    return c.IsValid ? c : null;
}

long Process(string[] input)
{
    var positive = new List<Cuboid>();
    var negative = new List<Cuboid>();

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

        var c = new Cuboid(ds[0].s, ds[0].e, ds[1].s, ds[1].e, ds[2].s, ds[2].e);
        if (op == "on")
        {
            var o1 = positive
                .Select(x => overlap(x, c))
                .Where(x => x != null)
                .ToList();
            var o2 = negative
                .Where(x => x.intersected)
                .Select(x => overlap(x, c))
                .Where(x => x != null)
                .ToList();
            positive.Add(c);
            positive.AddRange(o2);
            negative.AddRange(o1);
        }
        else
        {
            var o1 = positive
                .Select(x => overlap(x, c))
                .Where(x => x != null)
                .ToList();
            var o2 = negative
                .Select(x => overlap(x, c))
                .Where(x => x != null)
                .ToList();
            // negative.Add(c);
            positive.AddRange(o2);
            negative.AddRange(o1);
        }

        WriteLine($@"After {line}: {positive.Sum(x => x.Volume) - negative.Sum(x => x.Volume)}");
//         WriteLine($@"After {line}: {positive.Sum(x => x.Volume) - negative.Sum(x => x.Volume)}
// {string.Join('\n', positive.Select(x => $" + {x}"))}

// {string.Join('\n', negative.Select(x => $" - {x}"))}
//     ");
    }

    return positive.Sum(x => x.Volume) - negative.Sum(x => x.Volume);
}

// Process(input);
Process(@"on x=-5..47,y=-31..22,z=-19..33
on x=-44..5,y=-27..21,z=-14..35
on x=-49..-1,y=-11..42,z=-10..38
on x=-20..34,y=-40..6,z=-44..1
off x=26..39,y=40..50,z=-2..11
on x=-41..5,y=-41..6,z=-36..8
off x=-43..-33,y=-45..-28,z=7..25
on x=-33..15,y=-32..19,z=-34..11
off x=35..47,y=-46..-34,z=-11..5
on x=-14..36,y=-6..44,z=-16..29
on x=-57795..-6158,y=29564..72030,z=20435..90618
on x=36731..105352,y=-21140..28532,z=16094..90401
on x=30999..107136,y=-53464..15513,z=8553..71215
on x=13528..83982,y=-99403..-27377,z=-24141..23996
on x=-72682..-12347,y=18159..111354,z=7391..80950
on x=-1060..80757,y=-65301..-20884,z=-103788..-16709
on x=-83015..-9461,y=-72160..-8347,z=-81239..-26856
on x=-52752..22273,y=-49450..9096,z=54442..119054
on x=-29982..40483,y=-108474..-28371,z=-24328..38471
on x=-4958..62750,y=40422..118853,z=-7672..65583
on x=55694..108686,y=-43367..46958,z=-26781..48729
on x=-98497..-18186,y=-63569..3412,z=1232..88485
on x=-726..56291,y=-62629..13224,z=18033..85226
on x=-110886..-34664,y=-81338..-8658,z=8914..63723
on x=-55829..24974,y=-16897..54165,z=-121762..-28058
on x=-65152..-11147,y=22489..91432,z=-58782..1780
on x=-120100..-32970,y=-46592..27473,z=-11695..61039
on x=-18631..37533,y=-124565..-50804,z=-35667..28308
on x=-57817..18248,y=49321..117703,z=5745..55881
on x=14781..98692,y=-1341..70827,z=15753..70151
on x=-34419..55919,y=-19626..40991,z=39015..114138
on x=-60785..11593,y=-56135..2999,z=-95368..-26915
on x=-32178..58085,y=17647..101866,z=-91405..-8878
on x=-53655..12091,y=50097..105568,z=-75335..-4862
on x=-111166..-40997,y=-71714..2688,z=5609..50954
on x=-16602..70118,y=-98693..-44401,z=5197..76897
on x=16383..101554,y=4615..83635,z=-44907..18747
off x=-95822..-15171,y=-19987..48940,z=10804..104439
on x=-89813..-14614,y=16069..88491,z=-3297..45228
on x=41075..99376,y=-20427..49978,z=-52012..13762
on x=-21330..50085,y=-17944..62733,z=-112280..-30197
on x=-16478..35915,y=36008..118594,z=-7885..47086
off x=-98156..-27851,y=-49952..43171,z=-99005..-8456
off x=2032..69770,y=-71013..4824,z=7471..94418
on x=43670..120875,y=-42068..12382,z=-24787..38892
off x=37514..111226,y=-45862..25743,z=-16714..54663
off x=25699..97951,y=-30668..59918,z=-15349..69697
off x=-44271..17935,y=-9516..60759,z=49131..112598
on x=-61695..-5813,y=40978..94975,z=8655..80240
off x=-101086..-9439,y=-7088..67543,z=33935..83858
off x=18020..114017,y=-48931..32606,z=21474..89843
off x=-77139..10506,y=-89994..-18797,z=-80..59318
off x=8476..79288,y=-75520..11602,z=-96624..-24783
on x=-47488..-1262,y=24338..100707,z=16292..72967
off x=-84341..13987,y=2429..92914,z=-90671..-1318
off x=-37810..49457,y=-71013..-7894,z=-105357..-13188
off x=-27365..46395,y=31009..98017,z=15428..76570
off x=-70369..-16548,y=22648..78696,z=-1892..86821
on x=-53470..21291,y=-120233..-33476,z=-44150..38147
off x=-93533..-4276,y=-16170..68771,z=-104985..-24507".Split('\n'));
Process(File.ReadAllLines("input22.txt"));
// Process(@"on x=10..10,y=10..10,z=10..10
// on x=10..10,y=10..10,z=10..10
// off x=10..10,y=10..10,z=10..10".Split('\n'));

// Process(@"on x=1..2,y=1..2,z=1..2
// off x=1..1,y=1..1,z=1..1".Split('\n'));

record Cuboid(long x1, long x2, long y1, long y2, long z1, long z2, bool intersected = false)
{
    public long Volume = (x2 - x1 + 1) * (y2 - y1 + 1) * (z2 - z1 + 1);
    public bool IsValid = x1 <= x2 && y1 <= y2 && z1 <= z2;

    public override string ToString()
        => $"{x1}..{x2},{y1}..{y2},{z1}..{z2} ({Volume})";
}