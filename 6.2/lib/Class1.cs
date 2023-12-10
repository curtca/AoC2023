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
        var time = long.Parse(string.Concat(lines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)));
        var dist = long.Parse(string.Concat(lines[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)));
        
            double s1 = (time + Math.Sqrt(time * time - 4 * dist)) / 2;
            double s2 = (time - Math.Sqrt(time * time - 4 * dist)) / 2;
            if (s2 < s1)
                (s2, s1) = (s1, s2);
            long ls1 = (long) Math.Ceiling(s1);
            long ls2 = (long) Math.Floor(s2);
            if (s1 - ls1 == 0)
                ls1++;
            if (s2 - ls2 == 0)
                ls2--;

        return ls2 - ls1 + 1;
    }

}
