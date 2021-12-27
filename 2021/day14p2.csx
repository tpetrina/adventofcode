string input = @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";
input = File.ReadAllText("input14.txt");

var split = input.Split("\n\n");
var start = split[0];
var instructions = split[1].Split('\n').Select(s => s.Split(" -> ")).ToArray();

var pairs = Enumerable.Range(0, start.Length - 1)
    .Select(i => start.Substring(i, 2))
    .GroupBy(i => i)
    .ToDictionary(g => g.Key, g => (long)g.Count());
var counts = new long[26];
foreach (var ch in start) counts[ch - 'A']++;

for (var i = 0; i < 40; ++i)
{
    WriteLine($"Step: {i + 1}");

    var next = instructions.Select(x => x[0]).GroupBy(x => x).ToDictionary(g => g.Key, _ => (long)0);
    foreach (var pair in pairs)
    {
        if (pair.Value == 0) continue;
        var rule = instructions.FirstOrDefault(instruction => instruction[0] == pair.Key);
        if (rule is not null)
        {
            next[$"{pair.Key[0]}{rule[1]}"] += pair.Value;
            next[$"{rule[1]}{pair.Key[1]}"] += pair.Value;
            counts[rule[1][0] - 'A'] += pair.Value;
        }
    }

    pairs = next;

    var usages = counts
        .Select((c, i) => ((ch: (char)('A' + i), c)))
        .Where(p => p.c > 0)
        .OrderBy(p => p.c)
        .ToArray();
    WriteLine(string.Join(", ", usages.Select(p => $"{p.ch}:{p.c}")));

    WriteLine(usages.Last().c - usages.First().c);
}