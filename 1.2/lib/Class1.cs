using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace lib;

public class Calibration
{
    public static long CalibrationSums(string input)
    {
        return input.Split(Environment.NewLine)
            .Sum(s => Value(s));
    }

    private static long Value(string s)
    {
        int i1 = int.MaxValue, i2 = int.MinValue;
        long v1 = 0, v2 = 0;
        foreach (var item in numbers)
        {
            int i = s.IndexOf(item.Key);
            if (i > -1 && i < i1)
            {
                i1 = i;
                v1 = item.Value;
            }

            i = s.LastIndexOf(item.Key);
            if (i > i2)
            {
                i2 = i;
                v2 = item.Value;
            }
        }
        return 10 * v1 + v2;
    }

    private static Dictionary<string, long> numbers = new Dictionary<string, long>() {
        {"1", 1},
        {"2", 2},
        {"3", 3},
        {"4", 4},
        {"5", 5},
        {"6", 6},
        {"7", 7},
        {"8", 8},
        {"9", 9},
        {"0", 0},
        {"one", 1},
        {"two", 2},
        {"three", 3},
        {"four", 4},
        {"five", 5},
        {"six", 6},
        {"seven", 7},
        {"eight", 8},
        {"nine", 9},
        {"zero", 0},
    };
}