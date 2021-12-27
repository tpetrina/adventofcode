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
int b2dec(string bits) => bits.Aggregate(0, (v, c) => v * 2 + (c - '0'));

var sum = 0;
string input = "C0015000016115A2E0802F182340";
input = File.ReadAllText("input16.txt");
Parse(hex2bits(input));
WriteLine(sum);

int Parse(string decoded)
{
    var version = decoded.Substring(0, 3);
    var type = decoded.Substring(3, 3);
    WriteLine($"Version: {b2dec(version)}, type: {(b2dec(type) == 4 ? "value" : "operator")}");
    sum += b2dec(version);

    if (type == "100")
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
        return idx;
    }
    else
    {
        var lengthType = decoded[6];
        if (lengthType == '0')
        {
            var totalLength = b2dec(decoded.Substring(7, 15));
            WriteLine($"{totalLength} bits");
            var parsed = 0;
            do
            {
                parsed += Parse(decoded.Substring(parsed + 22));
            }
            while (parsed < totalLength);
            return 22 + totalLength;
        }
        else
        {
            var packets = b2dec(decoded.Substring(7, 11));
            WriteLine($"{packets} packets");
            var offset = 18;
            for (int i = 0; i < packets; ++i)
            {
                offset += Parse(decoded.Substring(offset));
            }
            return offset;
        }
    }
}