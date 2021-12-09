string[] input = @"2199943210
3987894921
9856789892
8767896789
9899965678".Split('\n');
input = File.ReadAllLines("input9.txt");

void Part1()
{
    var width = input[0].Length;
    var height = input.Length;

    var result = 0;

    for (var y = 0; y < height; ++y)
    {
        for (var x = 0; x < width; ++x)
        {
            var pt = input[y][x];
            if ((x == 0 || input[y][x - 1] > pt) &&
                (x == width - 1 || input[y][x + 1] > pt) &&
                (y == 0 || input[y - 1][x] > pt) &&
                (y == height - 1 || input[y + 1][x] > pt))
            {
                WriteLine($"{x}, {y} - {pt}");
                result += 1 + pt - '0';
                WriteLine(result);
            }
        }
    }

    WriteLine(result);
}

void Part2()
{
    var width = input[0].Length;
    var height = input.Length;

    var basins = new List<int>();

    int GetBasinSize(int x, int y)
    {
        var size = 0;
        var copy = new bool[width, height];

        var toVisit = new Queue<(int px, int py)>();
        toVisit.Enqueue((x, y));
        do
        {
            var (px, py) = toVisit.Dequeue();
            if (copy[px, py]) continue;
            copy[px, py] = true;
            var value = input[py][px];
            size++;

            if (px > 0 && input[py][px - 1] >= value && input[py][px - 1] != '9')
                toVisit.Enqueue((px - 1, py));
            if (py > 0 && input[py - 1][px] >= value && input[py - 1][px] != '9')
                toVisit.Enqueue((px, py - 1));
            if (px < width - 1 && input[py][px + 1] >= value && input[py][px + 1] != '9')
                toVisit.Enqueue((px + 1, py));
            if (py < height - 1 && input[py + 1][px] >= value && input[py + 1][px] != '9')
                toVisit.Enqueue((px, py + 1));

        } while (toVisit.Any());

        // var board = new string[height];
        // for (var j = 0; j < height; j++)
        // {
        //     var row = new char[width];
        //     for (var i = 0; i < width; i++)
        //         row[i] = copy[i, j] ? '+' : '.';
        //     board[j] = new string(row);
        // }
        // WriteLine(string.Join('\n', board));

        return size;
    }

    for (var y = 0; y < height; ++y)
    {
        for (var x = 0; x < width; ++x)
        {
            var pt = input[y][x];
            if ((x == 0 || input[y][x - 1] > pt) &&
                (x == width - 1 || input[y][x + 1] > pt) &&
                (y == 0 || input[y - 1][x] > pt) &&
                (y == height - 1 || input[y + 1][x] > pt))
            {
                WriteLine($"Low point: {x}, {y} - {pt}");
                basins.Add(GetBasinSize(x, y));
            }
        }
    }

    WriteLine(string.Join(",", basins));
    WriteLine(basins.OrderByDescending(x => x).Take(3).Aggregate(1, (x, n) => x * n));
}

// Part1();
Part2();