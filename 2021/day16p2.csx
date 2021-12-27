var l2b = @"0 = 0000
1 = 0001
2 = 0010
3 = 0011
4 = 0100
5 = 0101
6 = 0110
7 = 0111
8 = 1000
9 = 1001
A = 1010
B = 1011
C = 1100
D = 1101
E = 1110
F = 1111"
    .Split('\n')
    .Select(l => l.Split('='))
    .GroupBy(x => x[0].Trim())
    .ToDictionary(g => g.Key[0], g => g.First()[1].Trim());

string hex2bits(string input) => string.Join("", input.Select(ch => l2b[ch]));
long b2dec(string bits) => bits.Aggregate(0L, (v, c) => v * 2 + (c - '0'));

string input = "9C0141080250320F1802104A08";
input = File.ReadAllText("input16.txt");
var (_, result) = Parse(hex2bits(input));
WriteLine(result);

(int parsed, long value) Parse(string decoded)
{
    var version = decoded.Substring(0, 3);
    var type = b2dec(decoded.Substring(3, 3));

    if (type == 4)
    {
        int idx = 6;
        var value = "";
        while (decoded[idx] != '0')
        {
            value += decoded.Substring(idx + 1, 4);
            idx += 5;
        }
        value += decoded.Substring(idx + 1, 4);
        idx += 5;

        WriteLine($"Value: {b2dec(value)}");
        return (idx, b2dec(value));
    }
    else
    {
        var lengthType = decoded[6];
        var values = new List<long>();

        if (lengthType == '0')
        {
            var totalLength = b2dec(decoded.Substring(7, 15));
            var parsed = 0;
            do
            {
                var (p, value) = Parse(decoded.Substring(parsed + 22));
                parsed += p;
                values.Add(value);
            }
            while (parsed < totalLength);

            return (22 + (int)totalLength, op(type, values));
        }
        else
        {
            var packets = b2dec(decoded.Substring(7, 11));
            var offset = 18;
            for (int i = 0; i < packets; ++i)
            {
                var (p, value) = Parse(decoded.Substring(offset));
                offset += p;
                values.Add(value);
            }
            return (offset, op(type, values));
        }
    }
}

long op(long type, List<long> values)
{
    switch (type)
    {
        case 0:
            WriteLine($"sum({string.Join(", ", values)})");
            return values.Sum();
        case 1:
            WriteLine($"mul({string.Join(", ", values)})");
            return values.Aggregate(1L, (result, current) => result * current);
        case 2:
            WriteLine($"min({string.Join(", ", values)})");
            return values.Min();
        case 3:
            WriteLine($"max({string.Join(", ", values)})");
            return values.Max();
        case 5:
            WriteLine($"{values[0]} > {values[1]}");
            return values[0] > values[1] ? 1 : 0;
        case 6:
            WriteLine($"{values[0]} < {values[1]}");
            return values[0] < values[1] ? 1 : 0;
        case 7:
            WriteLine($"{values[0]} == {values[1]}");
            return values[0] == values[1] ? 1 : 0;
    }
    return -1;
}