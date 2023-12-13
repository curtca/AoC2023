using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Diagnostics;

namespace lib;

/*
New plan:
Recursive search where each call is to place 1 piece. 
Body then recursively cals for all locations of next piece

*/

public class HotSprings
{
    static string template = null;
    static int[] sizes = null;
    public static long Arrangements(string input) {
        string[] lines = input.Split(Environment.NewLine);
          return lines.Sum(line => {
            var parts = line.Split(' ');
            template = parts[0] + '?' + parts[0] + '?' + parts[0] + '?' + parts[0] + '?' + parts[0];
            sizes = parts[1].Split(',').Select(i => int.Parse(i)).ToArray();
            sizes = sizes.Concat(sizes).Concat(sizes).Concat(sizes).Concat(sizes).ToArray();
            long perms = Permutations(0, 0);
            Debug.WriteLine(perms);
            return perms;
        });
    }

    private static long Permutations(int pos, int piece)
    {
        // Build only valid permutations rather than checking them after generated
        // Try putting this piece here AND at subsequent spaces to the right (padded by spaces)
        // Can we even place this piece here? 

        int maxspaces = template.Length - pos; // how many spaces can we possibly shift next piece
        for (int i = piece; i < sizes.Length; i++)
            maxspaces -= sizes[i];
        maxspaces -= sizes.Length - piece - 1;

        long sum = 0;

        for (int space = 0; space <= maxspaces; space++) {
            bool canPlaceHere = true;
            for (int i = 0; i < sizes[piece]; i++) {
                if (template[pos + i] == '.') {
                    canPlaceHere = false;
                    break;
                }
            }
            bool lastPiece = piece == sizes.Length - 1;

            if (canPlaceHere && (lastPiece || (template[pos + sizes[piece] + space] != '#'))) { // need a space after each piece
                if (lastPiece)
                    sum ++;
                else
                    sum += Permutations(pos + sizes[piece] + space + 1, piece + 1);
            }
        }

        return sum;
    }

    private static bool IsValid(string sofar)
    {
        // we know every # and . is on a legal space as precondition, so we are just checking for correct number of groupings
        var groups = sofar.Split('.', StringSplitOptions.RemoveEmptyEntries).Select(grp => grp.Length).ToArray();
        return groups.SequenceEqual(sizes);
    }
}
