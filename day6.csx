string input = "3,4,3,1,2";
input = File.ReadAllText("input6.txt");

void Part1()
{
    List<long> Evolve(List<long> input)
    {
        var next = input[0];
        input.RemoveAt(0);
        input[6] += next;
        input.Add(next);
        return input;
    }

    var fishes = new List<long> { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    foreach (var fish in input
        .Split(',')
        .Select(long.Parse))
    {
        fishes[(int)fish]++;
    }

    for (var i = 0; i < 256; ++i)
    {
        fishes = Evolve(fishes);
        Console.WriteLine(string.Join(",", fishes));
    }

    Console.WriteLine(fishes.Sum());
}

Part1();