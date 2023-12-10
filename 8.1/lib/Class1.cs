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
        string here = "AAA";
        long steps = 0, iturns = 0;

        while (here != "ZZZ") {
            here = turns[iturns] == 'L' ? map[here].Item1 : map[here].Item2;
            steps++;
            iturns = (iturns + 1) % turns.Length;
        }

        return steps;
    }

}
