string[] input = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end".Split('\n');
input = @"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc".Split('\n');
input = @"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW".Split('\n');
input = File.ReadAllLines("input12.txt");

void Part1()
{
    var nodes = input
        .Select(x => x.Split('-', StringSplitOptions.RemoveEmptyEntries))
        .SelectMany(x => x)
        .Distinct()
        .Where(x => x != "start" && x != "end")
        .OrderBy(x => x)
        .ToList();
    var edges = input
        .Select(x => x.Split('-', StringSplitOptions.RemoveEmptyEntries))
        .Select(x => (a: x[0], b: x[1]))
        .ToList();
    WriteLine(string.Join(",", nodes));
    WriteLine(string.Join(",", edges.Select(e => $"{e.a}-{e.b}")));

    var connections = edges
        .Union(edges.Select(e => ((a: e.b, b: e.a))))
        .GroupBy(e => e.a)
        .ToDictionary(g => g.Key, g => g.Select(x => x.b).Where(x => x != "start").ToList());
    WriteLine(string.Join("\n", connections.Select(kvp => $"{kvp.Key}: {string.Join(",", kvp.Value)}")));

    // assume no loops
    long Count(string cave, List<string> visited = null)
    {
        visited = visited ?? new List<string>();

        // WriteLine($"Entering {cave} ({string.Join(",", visited)})");

        if (cave == "end")
        {
            // WriteLine($"{string.Join(",", visited)},end");
            return 1;
        }

        if (char.IsLower(cave[0]) && visited.Contains(cave))
        {
            return 0;
        }
        visited.Add(cave);

        return connections[cave]
            .Select(c => Count(c, visited.Select(x => x).ToList()))
            .Sum();
    }

    WriteLine(Count("start"));
}

void Part2()
{
    var nodes = input
        .Select(x => x.Split('-', StringSplitOptions.RemoveEmptyEntries))
        .SelectMany(x => x)
        .Distinct()
        .Where(x => x != "start" && x != "end")
        .OrderBy(x => x)
        .ToList();
    var edges = input
        .Select(x => x.Split('-', StringSplitOptions.RemoveEmptyEntries))
        .Select(x => (a: x[0], b: x[1]))
        .ToList();
    WriteLine(string.Join(",", nodes));
    WriteLine(string.Join(",", edges.Select(e => $"{e.a}-{e.b}")));

    var connections = edges
        .Union(edges.Select(e => ((a: e.b, b: e.a))))
        .GroupBy(e => e.a)
        .ToDictionary(g => g.Key, g => g.Select(x => x.b).Where(x => x != "start").ToList());
    WriteLine(string.Join("\n", connections.Select(kvp => $"{kvp.Key}: {string.Join(",", kvp.Value)}")));

    // assume no loops
    long Count(string cave, List<string> visited = null)
    {
        visited = visited ?? new List<string>();

        if (cave == "end")
        {
            // WriteLine($"{string.Join(",", visited)},end");
            return 1;
        }

        if (char.IsLower(cave[0]))
        {
            var t = visited
                .Where(x => char.IsLower(x[0]))
                .GroupBy(x => x)
                .ToDictionary(g => g.Key, g => g.Count());
            if (t.TryGetValue(cave, out var visitations))
            {
                if (visitations == 2)
                    return 0;
                else if (t.Any(kv => kv.Value == 2))
                    return 0;
            }
        }
        visited.Add(cave);

        return connections[cave]
            .Select(c => Count(c, visited.Select(x => x).ToList()))
            .Sum();
    }

    WriteLine(Count("start"));
}

// Part1();
Part2();