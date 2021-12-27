using System.Collections.Generic;

string[] input = @"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19

 3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6

14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7".Split('\n');
input = File.ReadAllLines("input4.txt");

void Part1()
{
    var numbers = input[0]
        .Split(',', StringSplitOptions.RemoveEmptyEntries)
        .Select(x => int.Parse(x))
        .ToList();

    var boards = new List<int[][]>();

    for (var i = 2; i < input.Length; i += 6)
    {
        var board = input.Skip(i).Take(5)
            .Select(t => t.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
            .ToArray();
        boards.Add(board);
    }

    int winningboard = -1;
    int winat = int.MaxValue;

    for (var i = 0; i < boards.Count; ++i)
    {
        WriteLine($"Board {i}");
        var board = boards[i];

        WriteLine(string.Join("\n",
            board.Select(r => string.Join(",", r))
        ));

        WriteLine();

        for (var row = 0; row < 5; ++row)
        {
            if (board[row].All(n => numbers.IndexOf(n) >= 0))
            {
                int winatround = board[row].Select(n => numbers.IndexOf(n)).Max();

                if (winatround < winat)
                {
                    WriteLine($"\tRow won! At round {winatround}");

                    winat = winatround;
                    winningboard = i;
                }
            }
        }

        for (var col = 0; col < 5; ++col)
        {
            var column = Enumerable.Range(0, 5).Select(r => board[r][col]);
            if (column.All(n => numbers.IndexOf(n) >= 0))
            {
                int winatround = column.Select(n => numbers.IndexOf(n)).Max();
                if (winatround < winat)
                {
                    WriteLine($"\tColumn won! At round {winatround}");

                    winat = winatround;
                    winningboard = i;
                }
            }
        }
    }

    int winningnumber = numbers[winat];
    WriteLine($"At round {winat} number {winningnumber} won.");
    WriteLine($"Winning board ({winningboard}):");
    WriteLine(string.Join("\n",
        boards[winningboard].Select(r => string.Join(",", r))
    ));

    var toCheck = new HashSet<int>(numbers.Take(winat + 1));
    var sum = boards[winningboard]
        .SelectMany(row => row)
        .Where(n => !toCheck.Contains(n))
        .Sum();

    WriteLine($"Sum: {sum}, last number: {winningnumber}, result: {sum * winningnumber}");
}

void Part2()
{
    var numbers = input[0]
        .Split(',', StringSplitOptions.RemoveEmptyEntries)
        .Select(x => int.Parse(x))
        .ToList();

    var boards = new List<int[][]>();

    for (var i = 2; i < input.Length; i += 6)
    {
        var board = input.Skip(i).Take(5)
            .Select(t => t.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray())
            .ToArray();
        boards.Add(board);
    }

    var boardWinsAt = new int[boards.Count];

    for (var i = 0; i < boards.Count; ++i)
    {
        WriteLine($"Board {i}");
        var board = boards[i];
        boardWinsAt[i] = int.MaxValue;

        WriteLine(string.Join("\n",
            board.Select(r => string.Join(",", r))
        ));

        WriteLine();

        for (var row = 0; row < 5; ++row)
        {
            if (board[row].All(n => numbers.IndexOf(n) >= 0))
            {
                int winatround = board[row].Select(n => numbers.IndexOf(n)).Max();
                boardWinsAt[i] = Math.Min(winatround, boardWinsAt[i]);
            }
        }

        for (var col = 0; col < 5; ++col)
        {
            var column = Enumerable.Range(0, 5).Select(r => board[r][col]);
            if (column.All(n => numbers.IndexOf(n) >= 0))
            {
                int winatround = column.Select(n => numbers.IndexOf(n)).Max();
                boardWinsAt[i] = Math.Min(winatround, boardWinsAt[i]);
            }
        }
    }

    WriteLine(string.Join(',', boardWinsAt));
    var lastRound = boardWinsAt.Where(x => x != int.MaxValue).Max();
    var winningboard = boardWinsAt.Select((x, i) => (x, i)).First(pair => pair.x == lastRound).i;

    int winningnumber = numbers[lastRound];
    WriteLine($"Board {winningboard} won at round {lastRound} with number {winningnumber}");

    var toCheck = new HashSet<int>(numbers.Take(lastRound + 1));
    var sum = boards[winningboard]
        .SelectMany(row => row)
        .Where(n => !toCheck.Contains(n))
        .Sum();

    WriteLine($"Sum: {sum}, last number: {winningnumber}, result: {sum * winningnumber}");
}

// Part1();
Part2();