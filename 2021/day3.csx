// string[] input = @"00100
// 11110
// 10110
// 10111
// 10101
// 01111
// 00111
// 11100
// 10000
// 11001
// 00010
// 01010".Split('\n');
string[] input = File.ReadAllLines("input3.txt");

void Part1()
{
    // gamma, epsilon
    long gamma = 0, epsilon = 0;
    int total = input.Length;
    for (var i = 0; i < input[0].Length; ++i)
    {
        int ones = 0, zeros = 0;
        for (var j = 0; j < input.Length; ++j)
        {
            if (input[j][i] == '1')
                ones++;
            else
                zeros++;
        }

        if (ones > zeros)
        {
            gamma |= 1u << (input[0].Length - i - 1);
        }
        else
        {
            epsilon |= 1u << (input[0].Length - i - 1);
        }
    }

    Console.WriteLine($"{gamma}, {epsilon}, {gamma * epsilon}");
}

long BinaryToLong(string number)
{
    long result = 0;
    for (var i = 0; i < number.Length; ++i)
        if (number[i] == '1')
            result |= 1u << (number.Length - i - 1);
    return result;
}

void PrintList(string[] list)
{
    foreach (var element in list)
    {
        Console.WriteLine($"\t{element}");
    }
}

void Part2()
{
    long GetRating(string[] list, char prefer, int position = 0)
    {
        if (list.Length == 0)
            throw new InvalidOperationException("List empty");
        if (list.Length == 1)
            return BinaryToLong(list[0]);
        if (position >= list[0].Length)
            throw new InvalidOperationException("Wrong position");

        long c1 = 0, c0 = 0;
        for (var i = 0; i < list.Length; ++i)
        {
            if (list[i][position] == '1')
                c1++;
        }
        c0 = list.Length - c1;

        char searchFor = prefer;
        if (prefer == '1')
        {
            // o2
            searchFor = c1 >= c0 ? '1' : '0';
        }
        else
        {
            // co2
            searchFor = c1 >= c0 ? '0' : '1';
        }

        string[] filtered = list.Where(x => x[position] == searchFor).ToArray();

        // WriteLine($"Position: {position}, search for {searchFor}");
        // WriteLine("List:");
        // PrintList(list);
        // WriteLine("Filtered:");
        // PrintList(filtered);

        return GetRating(filtered, prefer, position + 1);
    }

    string[] oxygen = input, co2 = input;
    long oxygenrating = GetRating(input, '1');
    long co2rating = GetRating(input, '0');


    Console.WriteLine($"{oxygenrating} {co2rating}, {oxygenrating * co2rating}");
}

// Part1();
Part2();