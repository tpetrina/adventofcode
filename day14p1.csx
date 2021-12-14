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
var instructions = split[1].Split('\n').Select(s => s.Split(" -> ")).ToArray(); ;

for (var i = 0; i < 10; ++i)
{
    WriteLine($"Step: {i + 1}");
    var sb = new StringBuilder();

    for (var j = 0; j < start.Length - 1; ++j)
    {
        var rule = instructions.FirstOrDefault(x => x[0] == start.Substring(j, 2));
        if (rule is not null)
        {
            if (j == 0)
                sb.Append(rule[0][0]);
            sb.Append($"{rule[1][0]}{rule[0][1]}");
        }
    }

    start = sb.ToString();
    PrintResults(start);
}

void PrintResults(string start)
{
    var counts = new Dictionary<char, long>();
    for (var ch = 'A'; ch <= 'Z'; ++ch) counts[ch] = 0;

    foreach (char ch in start)
        counts[ch]++;

    var usages = counts.Where(kvp => kvp.Value != 0).OrderBy(kvp => kvp.Value).ToArray();
    WriteLine(string.Join(", ", usages.Select(kvp => $"{kvp.Key}:{kvp.Value}")));

    WriteLine(usages.Last().Value - usages.First().Value);
}
