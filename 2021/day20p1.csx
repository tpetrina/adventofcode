string input = @"..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..##
#..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###
.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#.
.#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#.....
.#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#..
...####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.....
..##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#

#..#.
#....
##..#
..#..
..###";
input = File.ReadAllText("input20.txt");

string alg = string.Join("", input.Split("\n\n")[0]).Replace("\n", "");
string[] img = input.Split("\n\n")[1].Split('\n');

var map = Enumerable
    .Range(0, 512)
    .Select(d => (d, alg[d], dec2lights(d)))
    .GroupBy(x => x.Item3)
    .ToDictionary(g => g.Key, g => g.First().Item2);

for (var i = 0; i < 5; ++i)
    img = pad(img, '.');

char filler = '.';

for (var i = 0; i < 50; ++i)
    img = evolve(img);
// Print(img);

WriteLine(count(img));

long count(string[] input)
{
    return input.Sum(row => row.Count(x => x == '#'));
}

string dec2lights(int d)
{
    var result = new char[9];
    for (var i = 0; i < 9; i++)
    {
        result[9 - i - 1] = d % 2 == 1 ? '#' : '.';
        d /= 2;
    }
    return new string(result.ToArray());
}

string[] evolve(string[] input)
{
    var result = new char[input.Length][];

    filler = filler == '.'
        ? map["........."]
        : map["#########"];

    var w = input[0].Length;
    var h = input.Length;
    result[0] = new string(filler, w).ToCharArray();
    result[h - 1] = new string(filler, w).ToCharArray();

    for (var j = 1; j < h - 1; ++j)
    {
        result[j] = new char[w];
        for (var i = 1; i < w - 1; ++i)
        {
            var x = $"{input[j - 1][i - 1]}{input[j - 1][i]}{input[j - 1][i + 1]}"
                  + $"{input[j + 0][i - 1]}{input[j + 0][i]}{input[j + 0][i + 1]}"
                  + $"{input[j + 1][i - 1]}{input[j + 1][i]}{input[j + 1][i + 1]}";
            result[j][i] = map[x];
        }

        result[j][0] = filler;
        result[j][w - 1] = filler;
    }

    return pad(result.Select(l => new string(l)).ToArray(), filler);
}

string[] pad(string[] input, char ch)
{
    var result = new List<string>();

    var w = input[0].Length;
    result.Add(new string(ch, w + 2));

    var h = input.Length;
    for (var i = 0; i < h; ++i)
        result.Add($"{ch}{input[i]}{ch}");
    result.Add(new string(ch, w + 2));

    return result.ToArray();
}

void Print(string[] i) => WriteLine(string.Join('\n', i));
