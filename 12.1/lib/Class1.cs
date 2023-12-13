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

    /* 
        private static long Permutations(string template, int[] sizes)
        {
            long perms = 0;

            /*
                Find the first legal position of all pieces (left-most)
                Advance pieces (starting from left) until legal
                Do a "+1" on positioning. Advance rightmost piece by 1 until legal. If no legal options, try 2nd to last, etc.


            // find a legal starting position
            int[] positions = GetLegalPlacement(template, sizes, 0, 0);
            int pos = 0;
            for (int p = 0; p < positions.Length; p++)
            {
                if (CanPlace(template, pos, sizes[p])) {
                    positions[p] = pos;
                    pos += sizes[p] + 1;
                }
            }

            while (Advance(template, sizes, positions))
                perms++;

            return perms;
        }

        private static int[] GetLegalPlacement(string template, int[] sizes, int piece, int startpos)
        {
            var pos = new int[sizes.Length];
            while (piece < sizes.Length) {
                if (CanPlace(template, startpos, sizes[piece])) {
                    pos[piece] = startpos;
                    piece++;

                }

            }

            return pos;
        }

        private static bool CanPlace(string template, int start, int len)
        {
            for (int i = 0; i < len; i++)
            {
                if (template[start + i] == '.')
                    return false;
            }
            return true;
        }

        private static bool Advance(string str, int[] sizes, int[] pos)
        {
            var postry = (int[]) pos.Clone();
            for (int i = postry.Length - 1; i >= 0; i--)
            {
                // try marching this piece forward
            }
            // while (p < sizes.Length) {
            //    int minremaining = sizes.Skip(p).Sum() + sizes.Length - 1;
            //}

            return false;
        }
         */
}
