using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;

namespace lib;

public class Cards
{
    public static long SumOfCardScores(string input) {
        return input.Split(Environment.NewLine).Sum(line => {
            string[] lineparts = line.Split(new string[] {": ", " | "}, StringSplitOptions.TrimEntries);
            var winners = lineparts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(strnum => long.Parse(strnum));
            var have = lineparts[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(strnum => long.Parse(strnum));
            int matches = winners.Intersect(have).Count();
            return matches <= 1 ? matches : (long) Math.Pow(2, matches - 1);
        });
    }

}
