string input = "[[[[[9,8],1],2],3],4]";

Pair Parse(string number)
{
    var stack = new Stack<object>();
    foreach (var ch in number)
    {
        switch (ch)
        {
            case '[':
                if (stack.Count == 0 || !(stack.Peek() is string t) || t != "")
                    stack.Push("");
                break;
            case ',':
                stack.Push("");
                break;
            case ']':
                var right = stack.Pop();
                var left = stack.Pop();
                stack.Push(new Pair(left, right));
                break;

            default:
                if (stack.Peek() is string s)
                {
                    stack.Push((string)stack.Pop() + ch);
                }
                break;
        }
    }

    return stack.Pop() as Pair;
}

bool explode(Pair root)
{
    int addLeft = 0, addRight = 0;
    bool exploded = false;

    var flatten = new List<(Pair owner, Action<Func<int, int>> updater)>();

    void Gather(Pair pair)
    {
        if (pair.Left is int i)
            flatten.Add((pair, f => pair.Left = f((int)pair.Left)));
        else
            Gather((Pair)pair.Left);
        if (pair.Right is int j)
            flatten.Add((pair, f => pair.Right = f((int)pair.Right)));
        else
            Gather((Pair)pair.Right);
    }

    bool explode(Pair pair, int depth)
    {
        if (pair.IsNumber && depth >= 4 && !exploded)
        {
            // WriteLine($"\tExploded {pair}");
            exploded = true;
            addLeft = (int)pair.Left;
            addRight = (int)pair.Right;

            flatten
                .TakeWhile(f => f.owner != pair)
                .LastOrDefault()
                .updater?.Invoke(i => i + addLeft);
            flatten
                .SkipWhile(f => f.owner != pair)
                .SkipWhile(f => f.owner == pair)
                .FirstOrDefault()
                .updater?.Invoke(i => i + addRight);
            return true;
        }

        if (pair.Left is Pair left)
        {
            if (explode(left, depth + 1))
            {
                pair.Left = 0;
            }
        }

        if (pair.Right is Pair right)
        {
            if (explode(right, depth + 1))
            {
                pair.Right = 0;
            }
        }

        return false;
    }

    bool explodedOnce = false;
    do
    {
        flatten = new List<(Pair owner, Action<Func<int, int>> updater)>();
        Gather(root);

        exploded = false;
        explode(root, 0);
        if (exploded) explodedOnce = true;
    } while (exploded);

    return explodedOnce;
}

Pair split(int number)
{
    var left = (int)Math.Floor(number / 2.0);
    var right = number - left;
    return new Pair(left, right);
}

bool split(Pair number)
{
    // WriteLine($"  should I split {number}?");
    if (number.Left is int i && i > 9)
    {
        number.Left = split(i);
        return true;
    }
    else if (number.Left is Pair left && split(left))
        return true;

    if (number.Right is int j && j > 9)
    {
        number.Right = split(j);
        return true;
    }
    else if (number.Right is Pair right)
        return split(right);

    return false;
}

Pair reduce(Pair number)
{
    while (true)
    {
        if (explode(number))
        {
            // WriteLine("...exploded...");
        }
        // WriteLine($" -> {number}");
        if (split(number))
        {
            // WriteLine("...split...");
            continue;
        }
        // WriteLine($" => {number}");
        break;
    }
    return number;
}

int magnitude(Pair pair)
{
    var left = pair.Left is int i ? i : magnitude((Pair)pair.Left);
    var right = pair.Right is int j ? j : magnitude((Pair)pair.Right);
    return 3 * left + 2 * right;
}

Pair sum(string[] numbers)
{
    return numbers
        .Select(n => Parse(n))
        .Aggregate((Pair)null, (sum, n) =>
        {
            return reduce(sum + n);
        });
}


Pair explode_test(string input)
{
    var number = Parse(input);
    explode(number);
    return number;
}

long max(string[] input)
{
    var max = 0;

    foreach (var x in input)
    {
        foreach (var y in input)
        {
            var l = Parse(x);
            var r = Parse(y);
            var sum = reduce(l +r);
            var m = magnitude(sum);
            if (m > max)
            {
                WriteLine($"  {x}");
                WriteLine($"+ {y}");
                WriteLine($"= {sum}");
                WriteLine($"New max {m}");
                max = m;
            }
        }
    }

    return max;
}

WriteLine(max(@"[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]
[[[5,[2,8]],4],[5,[[9,9],0]]]
[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]
[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]
[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]
[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]
[[[[5,4],[7,7]],8],[[8,3],8]]
[[9,3],[[9,9],[6,[4,9]]]]
[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]
[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]".Split('\n')));
WriteLine(max(File.ReadAllLines("input18.txt")));

void RunTest<TIn, TOut>(TIn input, TOut expected, Func<TIn, TOut> eval)
{
    ResetColor();
    WriteLine(@"Running test:");
    var actual = eval(input);

    Expect(expected, actual);
}
void RunTests((string input, string expected, Func<string, string> eval)[] tests)
{
    foreach (var test in tests)
    {
        ResetColor();
        WriteLine(@"Running test:");
        var (input, expected, eval) = test;
        var actual = eval(input);

        Expect(expected, actual);
    }
}
void Expect<T>(T expected, T actual)
{
    WriteLine(@"Running test:");

    if (expected.Equals(actual))
    {
        ForegroundColor = ConsoleColor.Green;
        WriteLine($"Test passed! Got {actual}");
    }
    else
    {
        ForegroundColor = ConsoleColor.Red;
        WriteLine("Test failed!");
        WriteLine($"\tExpected: {expected}");
        WriteLine($"\tGot:      {actual}");
    }
    ResetColor();
}

class Pair
{
    public object Left { get; set; }
    public object Right { get; set; }

    public override string ToString()
    {
        return $"[{Left},{Right}]";
    }

    public bool IsNumber => Left is int && Right is int;

    public Pair(object left, object right) => (Left, Right) = (Parse(left), Parse(right));
    private static object Parse(object o)
        => o switch
        {
            Pair p => p,
            int i => i,
            string s => int.Parse(s),
            _ => throw new InvalidOperationException()
        };
    public override bool Equals(object obj)
    {
        return this.ToString() == obj.ToString();
    }

    public static Pair operator +(Pair left, Pair right)
        => left is null ? right : right is null ? left : new Pair(left, right);
}