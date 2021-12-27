string[] input = @"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]".Split('\n');
input = File.ReadAllLines("input10.txt");

void Part1()
{
    WriteLine(input.Select(line =>
    {
        var q = new Stack<char>();

        WriteLine(line);

        var chars = line.ToCharArray();
        for (var i = 0; i < chars.Length; ++i)
        {
            var ch = chars[i];

            switch (ch)
            {
                case '{':
                case '<':
                case '[':
                case '(':
                    q.Push(ch);
                    break;

                case ')':
                    if (!q.Any() || q.Pop() != '(')
                    {
                        WriteLine($"Expected ) at {i}. {string.Join(",", q)}");
                        return 3;
                    }
                    break;
                case ']':
                    if (!q.Any() || q.Pop() != '[')
                    {
                        WriteLine("Expected ]");
                        return 57;
                    }
                    break;
                case '}':
                    if (!q.Any() || q.Pop() != '{')
                    {
                        WriteLine("Expected }");
                        return 1197;
                    }
                    break;
                case '>':
                    if (!q.Any() || q.Pop() != '<')
                    {
                        WriteLine("Expected >");
                        return 25137;
                    }
                    break;
            }
        }

        return 0;
    })
    .Sum());
}

void Part2()
{
    var scores = input
        .Where(line =>
        {
            var s = new Stack<char>();

            var chars = line.ToCharArray();
            for (var i = 0; i < chars.Length; ++i)
            {
                var ch = chars[i];

                switch (ch)
                {
                    case '{':
                    case '<':
                    case '[':
                    case '(':
                        s.Push(ch);
                        break;

                    case ')':
                        if (!s.Any() || s.Pop() != '(')
                            return false;
                        break;
                    case ']':
                        if (!s.Any() || s.Pop() != '[')
                            return false;
                        break;
                    case '}':
                        if (!s.Any() || s.Pop() != '{')
                            return false;
                        break;
                    case '>':
                        if (!s.Any() || s.Pop() != '<')
                            return false;
                        break;
                }
            }

            return true;
        })
        .Select(line =>
        {
            WriteLine(line);

            var s = new Stack<char>();

            var chars = line.ToCharArray();
            for (var i = 0; i < chars.Length; ++i)
            {
                var ch = chars[i];

                switch (ch)
                {
                    case '{':
                    case '<':
                    case '[':
                    case '(':
                        s.Push(ch);
                        break;

                    case ')':
                    case ']':
                    case '}':
                    case '>':
                        s.Pop();
                        break;
                }
            }

            var result = s.Aggregate(0L, (total, ch) =>
            {
                return total * 5 + (ch == '(' ? 1
                : ch == '[' ? 2
                : ch == '{' ? 3
                : 4);
            });

            WriteLine(result);

            return result;
        })
        .OrderBy(x => x)
        .ToList();

    WriteLine(scores[scores.Count / 2]);
}

// Part1();
Part2();