using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace lib;

public class HotSprings
{
    static string template = null;
    static int[] sizes = null;
    public static long Arrangements(string input) {
        string[] lines = input.Split(Environment.NewLine);
          return lines.Sum(line => {
            var parts = line.Split(' ');
            template = parts[0];
            sizes = parts[1].Split(',').Select(i => int.Parse(i)).ToArray();
            return Permutations("");
        });
    }

    private static long Permutations(string sofar)
    {
        // Lame brute force approach: Generate every possible combo ./# of correct length and validate
        if (sofar.Length == template.Length)
            return IsValid(sofar) ? 1 : 0;
        else {
            long sum = 0;
            if (template[sofar.Length] != '#')
                sum += Permutations(sofar + ".");
            if (template[sofar.Length] != '.')
                sum += Permutations(sofar + "#");
            return sum;

        }
    }

    private static bool IsValid(string sofar)
    {
        // we know every # and . is on a legal space as precondition, so we are just checking for correct number of groupings
        var groups = sofar.Split('.', StringSplitOptions.RemoveEmptyEntries).Select(grp => grp.Length).ToArray();
        return groups.SequenceEqual(sizes);
    }
}
