using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;

namespace lib;

public class Class
{
    public static long Function(string input) {
        string[] lines = input.Split(Environment.NewLine);
        var times = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(str => int.Parse(str)).ToArray();
        var dists = lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(str => int.Parse(str)).ToArray();
        long prod = 1;
        
        for (int i = 0; i < times.Length; i++)
        {
            double s1 = (times[i] + Math.Sqrt(times[i] * times[i] - 4 * dists[i])) / 2;
            double s2 = (times[i] - Math.Sqrt(times[i] * times[i] - 4 * dists[i])) / 2;
            if (s2 < s1)
                (s2, s1) = (s1, s2);
            long ls1 = (long) Math.Ceiling(s1);
            long ls2 = (long) Math.Floor(s2);
            if (s1 - ls1 == 0)
                ls1++;
            if (s2 - ls2 == 0)
                ls2--;
            prod *= 1 + ls2 - ls1;
        };

        return prod;
    }

}
