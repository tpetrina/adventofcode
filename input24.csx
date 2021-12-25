long[] input = null;
long x = 0, y = 0, z = 0;
long w, index = 0;

w = input[index++];
x = 1;
y = w + 9;
z = w + 9;

w = input[index++];
x = z % 26 + 11;
x = x == w ? 1 : 0;
x = x == 0 ? 1 : 0;
y = 25 * x + 1;
z = z * y;
y = (w + 1) * x;
z = z + y;

w = input[index++];
x = z % 26 + 10;
x = x == w ? 1 : 0;
x = x == 0 ? 1 : 0;
y = 25 * x + 1;
z = z * y;
y = (w + 11) * x;
z = z + y;

w = input[index++];
x = z % 26 + 12;
x = x == w ? 1 : 0;
x = x == 0 ? 1 : 0;
y = 25 * x + 1;
z = z * y;
y = (w + 3) * x;
z = z + y;

w = input[index++];
x = z % 26 - 11;
x = x == w ? 1 : 0;
x = x == 0 ? 1 : 0;
y = 25 * x + 1;
z = (long)Math.Floor((double)z / 26);
z = z * y;
y = (w + 10) * x;
z = z + y;

w = input[index++];
x = z % 26 + 11;
x = x == w ? 1 : 0;
x = x == 0 ? 1 : 0;
y = 25 * x + 1;
z = z * y;
y = (w + 5) * x;
z = z + y;

w = input[index++];
x = z % 26 + 14;
x = x == w ? 1 : 0;
x = x == 0 ? 1 : 0;
y = 25 * x + 1;
z = z * y;
y = w * x;
z = z + y;

w = input[index++];
x = z % 26 - 6;
x = x == w ? 1 : 0;
x = x == 0 ? 1 : 0;
y = 25 * x + 1;
z = (long)Math.Floor((double)z / 26);
z = z * y;
y = (w + 7) * x;
z = z + y;

w = input[index++];
x = z % 26 + 10;
x = x == w ? 1 : 0;
x = x == 0 ? 1 : 0;
y = 25 * x + 1;
z = z * y;
y = (w + 9) * x;
z = z + y;

w = input[index++];
x = z % 26 - 6;
x = x == w ? 1 : 0;
x = x == 0 ? 1 : 0;
y = 25 * x + 1;
z = (long)Math.Floor((double)z / 26);
z = z * y;
y = (w + 15) * x;
z = z + y;

w = input[index++];
x = z % 26 - 6;
x = x == w ? 1 : 0;
x = x == 0 ? 1 : 0;
y = 25 * x + 1;
z = (long)Math.Floor((double)z / 26);
z = z * y;
y = (w + 4) * x;
z = z + y;

w = input[index++];
x = z % 26 - 16;
x = x == w ? 1 : 0;
x = x == 0 ? 1 : 0;
y = 25 * x + 1;
z = (long)Math.Floor((double)z / 26);
z = z * y;
y = (w + 10) * x;
z = z + y;

w = input[index++];
x = z % 26 - 4;
x = x == w ? 1 : 0;
x = x == 0 ? 1 : 0;
y = 25 * x + 1;
z = (long)Math.Floor((double)z / 26);
z = z * y;
y = (w + 4) * x;
z = z + y;

w = input[index++];
x = z % 26 - 2;
x = x == w ? 1 : 0;
x = x == 0 ? 1 : 0;
y = 25 * x + 1;
z = (long)Math.Floor((double)z / 26);
z = z * y;
y = (w + 9) * x;
z = z + y;