string input = "16,1,2,0,4,2,7,1,2,14";
input = File.ReadAllText("input7.txt");

void Part1()
{
    var numbers = input.Split(',').Select(int.Parse).ToList();
    var sum = numbers.Sum();
    var count = numbers.Count;

    var min = numbers.Min();
    var max = numbers.Max();

    var result = Enumerable.Range(min, max - min + 1)
        .Select(i => numbers.Select(n => Math.Abs(n - i)).Sum())
        .Min();
    WriteLine(result);
}

void Part2()
{
    var numbers = input.Split(',').Select(int.Parse).ToList();
    var sum = numbers.Sum();
    var count = numbers.Count;

    var min = numbers.Min();
    var max = numbers.Max();

    WriteLine($"{max - min}");

    var memo = new Dictionary<int, int>();
    for (var i = 0; i <= max - min; ++i)
        memo[i] = i * (i + 1) / 2;

    var result = Enumerable.Range(min, max - min + 1)
        .Select(i => numbers.Select(n => memo[Math.Abs(n - i)]).Sum())
        .Min();
    WriteLine(result);
}

// Part1();
Part2();
