var solutions = new Dictionary<Game, Win>();
var start = new Game(4, 8, 0, 0, 1);
start = new Game(8, 1, 0, 0, 1);

var configs = new (long, long)[]
{
    (3, 1), (4, 3), (5, 6), (6, 7), (7, 6), (8, 3), (9, 1)
};

long winat = 21;

Win Solve(Game game)
{
    if (solutions.TryGetValue(game, out var s))
    {
        return s;
    }

    if (game.s1 >= winat)
    {
        WriteLine($"Player 1 won with {game.s1}");
        return solutions[game] = new Win(1, 0);
    }
    if (game.s2 >= winat)
    {
        WriteLine($"Player 2 won with {game.s1}");
        return solutions[game] = new Win(0, 1);
    }

    long w1 = 0, w2 = 0;

    // 3: 1, 4: 3, 5: 6, 6: 7, 7: 6, 8: 3, 9: 1
    if (game.c == 1)
    {
        foreach (var config in configs)
        {
            var pn = (game.p1 + config.Item1 - 1) % 10 + 1;
            var game2 = game with
            {
                p1 = pn,
                s1 = game.s1 + pn,
                c = 2
            };

            var (v1, v2) = Solve(game2);
            w1 += v1 * config.Item2;
            w2 += v2 * config.Item2;
        }
    }
    else
    {
        foreach (var config in configs)
        {
            var pn = (game.p2 + config.Item1 - 1) % 10 + 1;
            var game2 = game with
            {
                p2 = pn,
                s2 = game.s2 + pn,
                c = 1
            };

            var (v1, v2) = Solve(game2);
            w1 += v1 * config.Item2;
            w2 += v2 * config.Item2;
        }
    }

    return solutions[game] = new Win(w1, w2);
}

WriteLine(Solve(start));

File.WriteAllLines("out.txt", solutions.Select(kvp => $"{kvp.Key} -> {kvp.Value}"));

record Win(long w1, long w2);

record Game(long p1, long p2, long s1, long s2, long c)
{
    public bool Won1 => s1 >= 21;
    public bool Won2 => s2 >= 21;
}