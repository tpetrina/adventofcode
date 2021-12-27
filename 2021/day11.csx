short[][] input = @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526"
    .Split('\n')
    .Select(row => row.ToCharArray().Select(ch => (short)(ch - '0')).ToArray()).ToArray();

input = @"11111
19991
19191
19991
11111"
    .Split('\n')
    .Select(row => row.ToCharArray().Select(ch => (short)(ch - '0')).ToArray()).ToArray();
input = @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526"
    .Split('\n')
    .Select(row => row.ToCharArray().Select(ch => (short)(ch - '0')).ToArray()).ToArray();
input = File.ReadAllLines("input11.txt")
    .Select(row => row.ToCharArray().Select(ch => (short)(ch - '0')).ToArray()).ToArray();

void Draw(short[][] map)
{
    WriteLine(string.Join('\n', map.Select(x => string.Join("", x))));
}

void Part2()
{
    Draw(input);
    var width = input[0].Length;
    var height = input.Length;
    for (var step = 0; ; ++step)
    {
        var flashes = new bool[width, height];
        for (var i = 0; i < height; ++i)
        {
            for (var j = 0; j < width; ++j)
            { input[i][j]++; }
        }

    start:
        for (var i = 0; i < height; ++i)
        {
            for (var j = 0; j < width; ++j)
            {
                if (input[i][j] > 9)
                {
                    input[i][j] = 0;
                    flashes[j, i] = true;

                    for (var di = -1; di <= 1; ++di)
                        for (var dj = -1; dj <= 1; ++dj)
                            if (i + di >= 0 && i + di < height &&
                                j + dj >= 0 && j + dj < width)
                            {
                                if (!flashes[j + dj, i + di])
                                    input[i + di][j + dj]++;
                            }

                    goto start;
                }
            }
        }

        WriteLine();
        Draw(input);

        var cnt = 0L;
        var e = flashes.GetEnumerator();
        while (e.MoveNext())
            cnt += (bool)e.Current ? 1 : 0;
        if (cnt == width * height)
        {
            WriteLine(step + 1);
            break;
        }
    }
}

void Part1()
{
    Draw(input);
    var width = input[0].Length;
    var height = input.Length;
    var result = 0L;
    for (var step = 0; step < 100; ++step)
    {
        var flashes = new bool[width, height];
        for (var i = 0; i < height; ++i)
        {
            for (var j = 0; j < width; ++j)
            { input[i][j]++; }
        }

    start:
        for (var i = 0; i < height; ++i)
        {
            for (var j = 0; j < width; ++j)
            {
                if (input[i][j] > 9)
                {
                    input[i][j] = 0;
                    flashes[j, i] = true;

                    for (var di = -1; di <= 1; ++di)
                        for (var dj = -1; dj <= 1; ++dj)
                            if (i + di >= 0 && i + di < height &&
                                j + dj >= 0 && j + dj < width)
                            {
                                if (!flashes[j + dj, i + di])
                                    input[i + di][j + dj]++;
                            }

                    goto start;
                }
            }
        }

        WriteLine();
        Draw(input);

        var cnt = 0L;
        var e = flashes.GetEnumerator();
        while (e.MoveNext())
            cnt += (bool)e.Current ? 1 : 0;
        result += cnt;
    }
    WriteLine(result);
}

// Part1();
Part2();