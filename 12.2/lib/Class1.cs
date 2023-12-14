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
    static Dictionary<(int, int), long> memo = null;

    public static long Arrangements(string input) {
        string[] lines = input.Split(Environment.NewLine);
          return lines.Sum(line => {
            var parts = line.Split(' ');
            template = parts[0] + '?' + parts[0] + '?' + parts[0] + '?' + parts[0] + '?' + parts[0];
            sizes = parts[1].Split(',').Select(i => int.Parse(i)).ToArray();
            sizes = sizes.Concat(sizes).Concat(sizes).Concat(sizes).Concat(sizes).ToArray();
            memo = new Dictionary<(int, int), long>();
            long perms = Permutations(0, 0, new List<(int,int)>());
            Debug.WriteLine(perms);
            return perms;
        });
    }

    private static long Permutations(int pos, int piece, List<(int, int)> howdidigethere)
    {
        // Build only valid permutations rather than checking them after generated
        // Try putting this piece here AND at subsequent spaces to the right (padded by spaces)
        // Can we even place this piece here? 

        long sum = 0;

        if (memo.TryGetValue((pos, piece), out sum))
            return sum;

        int maxspaces = template.Length - pos; // how many spaces can we possibly shift next piece
        for (int i = piece; i < sizes.Length; i++)
            maxspaces -= sizes[i];
        maxspaces -= sizes.Length - piece - 1;

        for (int space = 0; space <= maxspaces; space++) {
            if (space > 0 && template[pos + space - 1] == '#') break; // can't skip spaces that have #

            bool canPlaceHere = true;
            for (int i = 0; i < sizes[piece]; i++) {
                if (template[pos + space + i] == '.') {
                    canPlaceHere = false;
                    break;
                }
            }
            bool lastPiece = piece == sizes.Length - 1;

            if (canPlaceHere && (lastPiece || (template[pos + sizes[piece] + space] != '#'))) { // need a space before and after each piece
                List<(int, int)> howdidigethere2 = null;
                if (Debugger.IsAttached) {
                    howdidigethere2 = new List<(int, int)>(howdidigethere);
                    howdidigethere2.Add((pos + space, sizes[piece]));
                }
                if (lastPiece) {
                    // any extra #s == not valid
                    if (template.IndexOf('#', pos + space + sizes[piece]) == -1) {
                        sum ++;
                        string soln = null;
                        if (Debugger.IsAttached) 
                            Debug.Assert(Validate(howdidigethere2, out soln), string.Format("{0}\r\n{1}\r\n", template, soln));
                    }
                }
                else {
                    sum += Permutations(pos + sizes[piece] + space + 1, piece + 1, howdidigethere2);
                }
            }
        }

        memo.Add((pos, piece), sum);
        return sum;
    }

    private static bool Validate(List<(int, int)> hdigh, out string solnout) // (position, length)
    {
        solnout = null;
        var soln = new string('.', template.Length).ToCharArray();
        hdigh.ForEach(piece => {
            for (int i = 0; i < piece.Item2; i++) 
                soln[piece.Item1 + i] = '#';
            });
        bool valid = true;
        for (int i = 0; i < template.Length; i++)
        {
            if ((template[i] == '#' && soln[i] == '.')
                || (template[i] == '.' && soln[i] == '#'))
                valid = false;
        }
        if (!valid) 
        {
            solnout = new string(soln);
            Debug.WriteLine(solnout);
        }
        return valid;
    }
}
