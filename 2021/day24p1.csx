/*
    add a b -> a += b
    mul a b -> a *= b
    div a b -> a = (int)Math.Floor((double)a / b);
    mod a b -> a = a % b
    eql a b -> a = a == b ? 1 : 0
*/

string[] input = File.ReadAllLines("input24.txt");

(long x, long y, long z) eval(long[] input)
{
    long x = 0, y = 0, z = 0, w = 0;
    short index = 0;

    w = input[index++];
    x = x * 0;
    x = x + z;
    x = x % 26;
    z = (long)Math.Floor((double)z / 1);
    x = x + 15;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y = y * 0;
    y = y + 25;
    y = y * x;
    y = y + 1;
    z = z * y;
    y = y * 0;
    y = y + w;
    y = y + 9;
    y = y * x;
    z = z + y;

    w = input[index++];
    x = x * 0;
    x = x + z;
    x = x % 26;
    z = (long)Math.Floor((double)z / 1);
    x = x + 11;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y = y * 0;
    y = y + 25;
    y = y * x;
    y = y + 1;
    z = z * y;
    y = y * 0;
    y = y + w;
    y = y + 1;
    y = y * x;
    z = z + y;

    w = input[index++];
    x = x * 0;
    x = x + z;
    x = x % 26;
    z = (long)Math.Floor((double)z / 1);
    x = x + 10;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y = y * 0;
    y = y + 25;
    y = y * x;
    y = y + 1;
    z = z * y;
    y = y * 0;
    y = y + w;
    y = y + 11;
    y = y * x;
    z = z + y;
    w = input[index++];
    x = x * 0;
    x = x + z;
    x = x % 26;
    z = (long)Math.Floor((double)z / 1);
    x = x + 12;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y = y * 0;
    y = y + 25;
    y = y * x;
    y = y + 1;
    z = z * y;
    y = y * 0;
    y = y + w;
    y = y + 3;
    y = y * x;
    z = z + y;
    w = input[index++];
    x = x * 0;
    x = x + z;
    x = x % 26;
    z = (long)Math.Floor((double)z / 26);
    x = x + -11;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y = y * 0;
    y = y + 25;
    y = y * x;
    y = y + 1;
    z = z * y;
    y = y * 0;
    y = y + w;
    y = y + 10;
    y = y * x;
    z = z + y;
    w = input[index++];
    x = x * 0;
    x = x + z;
    x = x % 26;
    z = (long)Math.Floor((double)z / 1);
    x = x + 11;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y = y * 0;
    y = y + 25;
    y = y * x;
    y = y + 1;
    z = z * y;
    y = y * 0;
    y = y + w;
    y = y + 5;
    y = y * x;
    z = z + y;
    w = input[index++];
    x = x * 0;
    x = x + z;
    x = x % 26;
    z = (long)Math.Floor((double)z / 1);
    x = x + 14;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y = y * 0;
    y = y + 25;
    y = y * x;
    y = y + 1;
    z = z * y;
    y = y * 0;
    y = y + w;
    y = y + 0;
    y = y * x;
    z = z + y;
    w = input[index++];
    x = x * 0;
    x = x + z;
    x = x % 26;
    z = (long)Math.Floor((double)z / 26);
    x = x + -6;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y = y * 0;
    y = y + 25;
    y = y * x;
    y = y + 1;
    z = z * y;
    y = y * 0;
    y = y + w;
    y = y + 7;
    y = y * x;
    z = z + y;
    w = input[index++];
    x = x * 0;
    x = x + z;
    x = x % 26;
    z = (long)Math.Floor((double)z / 1);
    x = x + 10;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y = y * 0;
    y = y + 25;
    y = y * x;
    y = y + 1;
    z = z * y;
    y = y * 0;
    y = y + w;
    y = y + 9;
    y = y * x;
    z = z + y;
    w = input[index++];
    x = x * 0;
    x = x + z;
    x = x % 26;
    z = (long)Math.Floor((double)z / 26);
    x = x + -6;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y = y * 0;
    y = y + 25;
    y = y * x;
    y = y + 1;
    z = z * y;
    y = y * 0;
    y = y + w;
    y = y + 15;
    y = y * x;
    z = z + y;
    w = input[index++];
    x = x * 0;
    x = x + z;
    x = x % 26;
    z = (long)Math.Floor((double)z / 26);
    x = x + -6;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y = y * 0;
    y = y + 25;
    y = y * x;
    y = y + 1;
    z = z * y;
    y = y * 0;
    y = y + w;
    y = y + 4;
    y = y * x;
    z = z + y;
    w = input[index++];
    x = x * 0;
    x = x + z;
    x = x % 26;
    z = (long)Math.Floor((double)z / 26);
    x = x + -16;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y = y * 0;
    y = y + 25;
    y = y * x;
    y = y + 1;
    z = z * y;
    y = y * 0;
    y = y + w;
    y = y + 10;
    y = y * x;
    z = z + y;
    w = input[index++];
    x = x * 0;
    x = x + z;
    x = x % 26;
    z = (long)Math.Floor((double)z / 26);
    x = x + -4;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y = y * 0;
    y = y + 25;
    y = y * x;
    y = y + 1;
    z = z * y;
    y = y * 0;
    y = y + w;
    y = y + 4;
    y = y * x;
    z = z + y;
    w = input[index++];
    x = x * 0;
    x = x + z;
    x = x % 26;
    z = (long)Math.Floor((double)z / 26);
    x = x + -2;
    x = x == w ? 1 : 0;
    x = x == 0 ? 1 : 0;
    y = y * 0;
    y = y + 25;
    y = y * x;
    y = y + 1;
    z = z * y;
    y = y * 0;
    y = y + w;
    y = y + 9;
    y = y * x;
    z = z + y;

    return (x, y, z);
}


(long x, long y, long z) partial(long z, long w, long dx, long dz, long dy)
{
    long x = z % 26 + dx;
    x = x != w ? 1 : 0;
    long y = 25 * x + 1;
    z = (long)Math.Floor((double)z / dz);
    z = z * y;
    y = (w + dy) * x;
    z = z + y;
    return (x, y, z);
}

(long x, long y, long z) eval2(long[] input)
{
    long x = 0, y = 0, z = 0, w = 0;
    short index = 0;

    w = input[index++]; // 1
    z = w + 9;

    w = input[index++];
    z = z * 26 + w + 1;

    w = input[index++];
    z = z * 26 + w + 11;

    w = input[index++]; // 4
    z = z * 26 + w + 3;

    w = input[index++];  // 5
    x = z % 26 - 11;
    x = x != w ? 1 : 0;
    y = 25 * x + 1;
    z = (long)Math.Floor((double)z / 26);
    z = z * y;
    y = (w + 10) * x;
    z = z + y;

    w = input[index++]; // 6
    z = z * 26 + w + 5;

    w = input[index++]; // 7
    z = z * 26 + w;

    w = input[index++]; // 8
    x = z % 26 - 6;
    x = x != w ? 1 : 0;
    y = 25 * x + 1;
    z = (long)Math.Floor((double)z / 26);
    z = z * y;
    y = (w + 7) * x;
    z = z + y;

    w = input[index++]; // 9
    z = z * 26 + w + 9;

    w = input[index++]; // 10
    x = z % 26 - 6;
    x = x != w ? 1 : 0;
    y = 25 * x + 1;
    z = (long)Math.Floor((double)z / 26);
    z = z * y;
    y = (w + 15) * x;
    z = z + y;

    w = input[index++]; // 11
    x = z % 26 - 6;
    x = x != w ? 1 : 0;
    y = 25 * x + 1;
    z = (long)Math.Floor((double)z / 26);
    z = z * y;
    y = (w + 4) * x;
    z = z + y;

    w = input[index++]; // 12
    x = z % 26 - 16;
    x = x != w ? 1 : 0;
    y = 25 * x + 1;
    z = (long)Math.Floor((double)z / 26);
    z = z * y;
    y = (w + 10) * x;
    z = z + y;

    w = input[index++]; // 13
    x = z % 26 - 4;
    x = x != w ? 1 : 0;
    y = 25 * x + 1;
    z = (long)Math.Floor((double)z / 26);
    z = z * y;
    y = (w + 4) * x;
    z = z + y;

    w = input[index++]; // 14
    x = z % 26 - 2;
    // x = 0 => x = w
    x = x != w ? 1 : 0;
    y = 25 * x + 1;
    z = (long)Math.Floor((double)z / 26);
    z = z * y;
    y = (w + 9) * x;
    z = z + y;

    return (x, y, z);
}

// let's pretend x is zero
(long x, long y, long z) eval_inf(long[] input)
{
    long x = 0, y = 0, z = 0, w = 0;
    short index = 0;

    w = input[index++]; // 1
    z = w + 9;

    w = input[index++];
    z = z * 26 + w + 1;

    w = input[index++];
    z = z * 26 + w + 11;

    w = input[index++];
    z = z * 26 + w + 3;

    w = input[index++];  // 5
    x = z % 26 - 11; // == 0
    z = (long)Math.Floor((double)z / 26);

    w = input[index++]; // 6
    z = z * 26 + w + 5;

    w = input[index++]; // 7
    z = z * 26 + w;

    w = input[index++]; // 8
    x = z % 26 - 6; // == 0
    z = (long)Math.Floor((double)z / 26);

    w = input[index++]; // 9
    z = z * 26 + w + 9;

    w = input[index++]; // 10
    x = z % 26 - 6; // == 0
    z = (long)Math.Floor((double)z / 26);

    w = input[index++]; // 11
    x = z % 26 - 6; // == 0
    z = (long)Math.Floor((double)z / 26);

    w = input[index++]; // 12
    x = z % 26 - 16; // == 0
    z = (long)Math.Floor((double)z / 26);

    w = input[index++]; // 13
    x = z % 26 - 4; // == 0
    z = (long)Math.Floor((double)z / 26);

    w = input[index++]; // 14
    x = z % 26 - 2; // == 0
    z = (long)Math.Floor((double)z / 26);

    return (x, y, z);
}

(long x, long y, long z) eval3(long[] input)
{
    long x = 0, y = 0, z = 0, w = 0;
    short index = 0;

    w = input[index++];
    (x, y, z) = partial(z, w, 15, 1, 9);
    WriteLine((w, x, y, z, z % 26));

    w = input[index++];
    (x, y, z) = partial(z, w, 11, 1, 1);
    WriteLine((w, x, y, z, z % 26));

    w = input[index++];
    (x, y, z) = partial(z, w, 10, 1, 11);
    WriteLine((w, x, y, z, z % 26));

    w = input[index++];
    (x, y, z) = partial(z, w, 12, 1, 3);
    WriteLine((w, x, y, z, z % 26));

    w = input[index++];
    (x, y, z) = partial(z, w, -11, 26, 10);
    WriteLine((w, x, y, z, z % 26));

    w = input[index++];
    (x, y, z) = partial(z, w, 11, 1, 5);
    WriteLine((w, x, y, z, z % 26));

    w = input[index++];
    (x, y, z) = partial(z, w, 14, 1, 0);
    WriteLine((w, x, y, z, z % 26));

    w = input[index++];
    (x, y, z) = partial(z, w, -6, 26, 7);
    WriteLine((w, x, y, z, z % 26));

    w = input[index++];
    (x, y, z) = partial(z, w, 10, 1, 9);
    WriteLine((w, x, y, z, z % 26));

    w = input[index++];
    (x, y, z) = partial(z, w, -6, 26, 15);
    WriteLine((w, x, y, z, z % 26));

    w = input[index++];
    (x, y, z) = partial(z, w, -6, 26, 4);
    WriteLine((w, x, y, z, z % 26));

    w = input[index++];
    (x, y, z) = partial(z, w, -16, 26, 10);
    WriteLine((w, x, y, z, z % 26));

    w = input[index++];
    (x, y, z) = partial(z, w, -4, 26, 4);
    WriteLine((w, x, y, z, z % 26));

    w = input[index++];
    (x, y, z) = partial(z, w, -2, 26, 9);
    WriteLine((w, x, y, z, z % 26));

    return (x, y, z);
}

void eval_test(long[] input)
{
    long x = 0, y = 0, z = 0;
    long w, index = 0;

    w = input[index++];
    x = 1;
    y = w + 9;
    z = w + 9;

    w = input[index++];
    x = 1;
    y = w + 1;
    z = z * 26 + y;

    w = input[index++];
    x = 1;
    y = w + 11;
    z = z * 26 + y;

    w = input[index++];
    x = 1;
    y = w + 3;
    z = z * 26 + y;

    if (x != 1)
        WriteLine((string.Join("", input), x, y, z, w));
}

// Enumerable
//     .Range(11111, 99999 - 11111 + 1)
//     .Select(i =>
//     {
//         eval_test(i.ToString().Select(x => long.Parse($"{x}")).ToArray());
//         return i;
//     })
//     .ToList();

// for (var n = 79999999999999; ; n--)
// {
//     var (x, y, z) = eval(n.ToString().ToCharArray());
//     WriteLine($"{n}: {x}, {y}, {z}");
//     if (z == 0)
//     {
//         WriteLine($"Found it! {n}");
//         break;
//     }
// }

var max = 29991993698469L;
var min = 14691271141118L;

/*
  w4 = w5 + 8
  w7 = w8 + 6
  w9 = w10 - 3
  w11 = w6 - 1
  w12 = w3 - 5
  w13 = w2 - 3
  w14 = w1 + 7
*/

var n = 13579246899999L.ToString().ToCharArray().Select(ch => (long)(ch - '0')).ToArray();
WriteLine(eval(n));
WriteLine(eval2(n));
WriteLine(eval3(n));

void experiment()
{
    var index = 0;
    while (true)
    {
        Clear();
        WriteLine(string.Join(" ", n));
        WriteLine(new string(' ', index * 2) + "^");
        WriteLine(new string(' ', index * 2) + $"{index + 1}");
        eval3(n);
        WriteLine(string.Join("", n.Select(x => (char)('a' + x))));
        WriteLine(eval(n));
        WriteLine(eval2(n));
        ConsoleKeyInfo ch = ReadKey();
        switch ((long)ch.Key)
        {
            case 37: index = index == 0 ? 0 : index - 1; break;
            case 39: index = index == 13 ? 13 : index + 1; break;
            case 38: n[index] = next(n[index]); break;
            case 40: n[index] = prev(n[index]); break;
        }
    }

    long next(long x) => x == 9 ? 1 : x + 1;
    long prev(long x) => x == 1 ? 9 : x - 1;
}

experiment();

//

/*
 12345678901234
 abcdefghijklmn

 
 n = m + 2 => 

*/