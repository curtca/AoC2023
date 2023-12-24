using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Drawing;
namespace lib;

public class Class
{
    public static long Function(string input) {
        string[] lines = input.Split(Environment.NewLine);

        // Common sense approach impossible. Magic comes from math. Specifically:
        // https://www.themathdoctors.org/polygon-coordinates-and-areas
               

        long x = 0, y = 0, xprev = 0, yprev = 0, left = 0, right = 0, perim = 0;
        foreach (var line in lines) {
            var parts = line.Split(' ');
            long l = long.Parse(parts[2].Substring(2, 5), System.Globalization.NumberStyles.HexNumber);
            int dir = int.Parse(parts[2].Substring(7, 1), System.Globalization.NumberStyles.HexNumber);
            // part 1: long l = long.Parse(parts[1]);
            // part 1: int dir = "RDLU".IndexOf(parts[0][0]);
            perim += l;
            switch (dir) {
            case 0:
                x += l;
                break;
            case 1:
                y -= l;
                break;
            case 2:
                x -= l;
                break;
            case 3:
                y += l;
                break;
            default: 
                Debug.Fail($"bad direction: {dir}");
                break;
            }
            left += xprev * y;
            right += x * yprev;
            xprev = x; yprev = y;
        }
        Debug.Assert(xprev == 0 && yprev == y);
        long total = Math.Abs(left - right) / 2;
        total += perim / 2 + 1;

        return total;
    }
}
