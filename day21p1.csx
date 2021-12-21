var p1 = 8L;
var p2 = 1L;
var s1 = 0L;
var s2 = 0L;

var current = 1;

long roll()
{
    var c = current;

    current++;
    if (current > 100) current = 1;

    return c;
}

var rolls = 0L;

while (true)
{
    var n = roll() + roll() + roll();
    rolls += 3;
    p1 = ((p1 - 1) + n) % 10 + 1;
    s1 += p1;
    WriteLine($"Player 1 rolled {n} and moves to space {p1} for total score of {s1}");

    if (s1 >= 1000)
    {
        WriteLine("Player 1 wins");
        WriteLine(s2 * rolls);
        break;
    }

    n = roll() + roll() + roll();
    rolls += 3;
    p2 = ((p2 - 1) + n) % 10 + 1;
    s2 += p2;
    WriteLine($"Player 2 rolled {n} and moves to space {p2} for total score of {s2}");
    if (s2 >= 1000)
    {
        WriteLine("Player 2 wins");
        WriteLine(s1 * rolls);
        break;
    }
}