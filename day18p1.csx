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
            WriteLine($"  {sum}");
            WriteLine($"+ {n}");
            var result = reduce(sum + n);
            WriteLine($"= {result}");
            return result;
        });
}


Pair explode_test(string input)
{
    var number = Parse(input);
    explode(number);
    return number;
}

RunTests(new (string, string, Func<string, string>)[]
{
    ("[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]", i => explode_test(i).ToString()),
    ("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7,0]]]]", i => explode_test(i).ToString()),
    ("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]", i => explode_test(i).ToString()),
    ("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", i => explode_test(i).ToString()),
    ("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]", i => explode_test(i).ToString()),
});
RunTest(10, new Pair(5, 5), split);
RunTest(11, new Pair(5, 6), split);
RunTest(12, new Pair(6, 6), split);
RunTest(18, new Pair(9, 9), split);
RunTest(20, new Pair(10, 10), split);
Expect(Parse("[[1,1],[2,2]]"), Parse("[1,1]") + Parse("[2,2]"));
Expect(Parse("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]"), Parse("[[[[4,3],4],4],[7,[[8,4],9]]]") + Parse("[1,1]"));
Expect(29, magnitude(Parse("[9,1]")));
Expect(129, magnitude(Parse("[[9,1],[1,9]]")));
Expect(143, magnitude(Parse("[[1,2],[[3,4],5]]")));
Expect(3488, magnitude(Parse("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]")));
Expect(4140, magnitude(Parse("[[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]]")));
Expect(Parse("[[[[1,1],[2,2]],[3,3]],[4,4]]"), sum(@"[1,1]
[2,2]
[3,3]
[4,4]".Split('\n')));
Expect(Parse("[[[[3,0],[5,3]],[4,4]],[5,5]]"), sum(@"[1,1]
[2,2]
[3,3]
[4,4]
[5,5]".Split('\n')));
Expect(Parse("[[[[5,0],[7,4]],[5,5]],[6,6]]"), sum(@"[1,1]
[2,2]
[3,3]
[4,4]
[5,5]
[6,6]".Split('\n')));
Expect(Parse("[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]"), sum(@"[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]
[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]".Split('\n')));
Expect(Parse("[[[[6,7],[6,7]],[[7,7],[0,7]]],[[[8,7],[7,7]],[[8,8],[8,0]]]]"), sum(@"[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]
[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]".Split('\n')));
Expect(Parse("[[[[7,7],[7,7]],[[8,7],[8,7]]],[[[7,0],[7,7]],9]]"), sum(@"[[[[7,8],[6,7]],[[6,8],[0,8]]],[[[7,7],[5,0]],[[5,5],[5,6]]]]
[[[5,[7,4]],7],1]".Split('\n')));
Expect(Parse("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]"), sum(@"[[[[7,7],[7,7]],[[8,7],[8,7]]],[[[7,0],[7,7]],9]]
[[[[4,2],2],6],[8,7]]".Split('\n')));

Expect(Parse("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]"), sum(@"[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]
[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]
[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]
[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]
[7,[5,[[3,8],[1,4]]]]
[[2,[2,2]],[8,[8,1]]]
[2,9]
[1,[[[9,3],9],[[9,0],[0,7]]]]
[[[5,[7,4]],7],1]
[[[[4,2],2],6],[8,7]]".Split('\n')));

WriteLine(magnitude(sum(File.ReadAllLines("input18.txt"))));

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