using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;

namespace lib;

public class Cards
{
    public static long TotalScratchCards(string input) {
        string[] lines = input.Split(Environment.NewLine);
        var cards = lines.Select(line => 1).ToArray();
        int linenum = 0;
        return lines.Sum(line => {
            string[] lineparts = line.Split(new string[] {": ", " | "}, StringSplitOptions.TrimEntries);
            var winners = lineparts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(strnum => long.Parse(strnum));
            var have = lineparts[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(strnum => long.Parse(strnum));
            int matches = winners.Intersect(have).Count();
            for (int i = 0; i < matches; i++)
                cards[linenum + i + 1] += cards[linenum];

            return cards[linenum++];
        });
    }
}
