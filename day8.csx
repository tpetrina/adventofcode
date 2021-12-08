string[] input = @"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce".Split("\n");
input = File.ReadAllLines("input8.txt");

void Part1()
{
    var count = 0;
    var set = new HashSet<string>();
    foreach (var line in input)
    {
        var right = line.Split('|').Select(x => x.Trim()).Skip(1).First();
        foreach (var x in right.Split(' ').Where(x => x.Length != 5 && x.Length != 6))
            set.Add(x);

        count += right.Split(' ').Count(x => x.Length != 5 && x.Length != 6);
    }

    WriteLine(set.Count);
    WriteLine(count);
}

// Part1();

// 0: abcefg  | 6
// 1: cf      | 2
// 2: acdeg   | 5
// 3: acdfg   | 5
// 4: bcdf    | 4
// 5: abdfg   | 5
// 6: abdefg  | 6
// 7: acf     | 3
// 8: abcdefg | 7
// 9: abcdfg  | 6
int Part2_1(string left, string right)
{
    string remove(string a, string b)
    {
        return new string(
            a
                .ToCharArray()
                .Where(ch => !b.Contains(ch))
                .ToArray()
        );
    }

    var patterns = left
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        // .Select(x => new string(x.ToCharArray().OrderBy(x => x).ToArray()))
        .GroupBy(x => x.Length)
        .ToDictionary(x => x.Key, x => x.ToList());

    var numbers = new string[10].ToList();

    numbers[1] = patterns[2][0];
    numbers[4] = patterns[4][0];
    numbers[7] = patterns[3][0];
    numbers[8] = patterns[7][0];

    numbers[9] = patterns[6]
        .Where(n => remove(n, numbers[4]).Length == 2)
        .First();
    numbers[0] = patterns[6]
        .Where(n => n != numbers[9])
        .Select(n => (n, diff: remove(n, numbers[1]).Length))
        .First(x => x.diff == 4)
        .n;
    numbers[6] = patterns[6]
        .Where(n => n != numbers[0] && n != numbers[9])
        .First();

    numbers[3] = patterns[5]
        .Where(n => remove(n, numbers[1]).Length == 3)
        .First();
    numbers[5] = patterns[5]
        .Where(n => n != numbers[3])
        .Where(n => remove(numbers[9], n).Length == 1)
        .First();
    numbers[2] = patterns[5]
        .Where(n => n != numbers[3] && n != numbers[5])
        .First();

    numbers = numbers
        .Select(n => new string(n.ToCharArray().OrderBy(x => x).ToArray()))
        .ToList();

    int result = right
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(n => new string(n.ToCharArray().OrderBy(x => x).ToArray()))
        .Select((n, i) => (int)Math.Pow(10, 3 - i) * numbers.IndexOf(n))
        .Sum();

    WriteLine(string.Join(",", numbers));
    WriteLine();
    return result;
}


// Part2_1("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab", "cdfeb fcadb cdfeb cdbaf");
void Part2()
{
    WriteLine(input.Select(line =>
    {
        var s = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
        var result = Part2_1(s[0], s[1]);
        WriteLine(result);
        return result;
    }).Sum());
}

Part2();