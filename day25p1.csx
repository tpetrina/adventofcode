var input = @"v...>>.vv>
.vv>>.vv..
>>.>v>...v
>>v>>.>.v.
v>v.vv.v..
>.>>..v...
.vv..>.>v.
v.v..>>v.v
....v..v.>";
input = File.ReadAllText("input25.txt");

var map = input.Split('\n').Select(row => row.Replace("\n", "")).ToArray();

WriteLine($"{map[0].Length}x{map.Length}");
print(map);
WriteLine();

long moves = 0;
for (var i = 0; ; ++i)
{
    (map, moves) = step(map);
    if (moves == 0)
    {
        print(map);
        WriteLine($"Done after {i + 1}");
        break;
    }
}


(string[] result, long moves) step(string[] current)
{
    var result = new char[current.Length][];
    var w = current[0].Length;
    var h = current.Length;

    long moves = 0;

    for (var i = 0; i < current.Length; ++i)
    {
        var old = current[i];
        var row = result[i] = new string('.', w).ToCharArray();

        for (var j = 0; j < w; ++j)
        {
            if (old[j] == '>')
            {
                if (j == w - 1 && old[0] == '.')
                {
                    row[0] = '>';
                    moves++;
                }
                else if (j < w - 1 && old[j + 1] == '.')
                {
                    row[j + 1] = '>';
                    moves++;
                }
                else
                {
                    row[j] = '>';
                }
            }
            else if (old[j] == 'v')
            {
                row[j] = old[j];
            }
        }
    }

    current = result
        .Select(row => new string(row))
        .ToArray();

    for (var j = 0; j < w; ++j)
    {
        for (var i = 0; i < h; ++i)
        {
            if (current[i][j] == 'v')
            {
                if (i == h - 1 && current[0][j] == '.')
                {
                    result[0][j] = 'v';
                    result[i][j] = '.';
                    moves++;
                }
                else if (i < h - 1 && current[i + 1][j] == '.')
                {
                    result[i + 1][j] = 'v';
                    result[i][j] = '.';
                    moves++;
                }
            }
        }
    }

    return (result
        .Select(row => new string(row))
        .ToArray(), moves);
}

void print(string[] map) => WriteLine(string.Join('\n', map));