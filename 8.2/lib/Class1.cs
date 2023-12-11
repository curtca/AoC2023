using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;

namespace lib;

public class Wasteland
{
    public static long Steps(string input) {
        string[] lines = input.Split(Environment.NewLine);
        var turns = lines[0].ToArray();
        var map = new Dictionary<string, (string, string)>();
        foreach (var line in lines.Skip(2)) {
            // AAA = (BBB, CCC)
            map.Add(line.Substring(0, 3), (line.Substring(7, 3), line.Substring(12, 3)));
        }
        var starts = map.Keys.Where(k => k[2] == 'A');
        // For each starting position, find length to end. Then do LCM on those lengths

        long steps = starts.Select(loc => {
            long iturns = 0;
            long steps = 0;
            while (loc[2] != 'Z') {
                loc = turns[iturns] == 'L' ? map[loc].Item1 : map[loc].Item2;
                iturns = (iturns + 1) % turns.Length;
                steps++;
            }
            return steps;
        }).ToArray().Aggregate((lcm, val) => lcm * val / GCD(lcm, val)); 
    
        return steps;
    }

    static long GCD(long n1, long n2)
    {
        if (n2 == 0)
            return n1;
        else
            return GCD(n2, n1 % n2);
    }
}
