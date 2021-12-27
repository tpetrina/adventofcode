string[] input = File.ReadAllLines("input2.txt");

public void Part1()
{
    long h = 0, d = 0;
    foreach (var command in input)
    {
        switch (command[0])
        {
            case 'f':
                h += int.Parse(command.Substring(8));
                break;
            case 'd':
                d += int.Parse(command.Substring(5));
                break;
            case 'u':
                d -= int.Parse(command.Substring(3));
                break;
        }
    }

    Console.WriteLine($"{h}, {d}, {h * d}");
}

public void Part2()
{
    long h = 0, d = 0, aim = 0;

    foreach (var command in input)
    {
        switch (command[0])
        {
            case 'd':
                aim += int.Parse(command.Substring(5));
                break;
            case 'u':
                aim -= int.Parse(command.Substring(3));
                break;
            case 'f':
                long x = int.Parse(command.Substring(8));
                h += x;
                d += aim * x;
                break;
        }
    }

    Console.WriteLine($"{h}, {d}, {aim} {h * d}");
}

// Part1();
Part2();