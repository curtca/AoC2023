using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace lib;

public class PipeMaze
{
   /*
        Build map of loop (excluding junk not part of the loop).
        Replace S with its actial pipe
        Just iterate each row and start counting vertical crossings left to right
        | is 1 crossing. 
        L -* J is 2 crossings
        L -* 7 is 1 crossing
        F -* 7 is 2 crossings
        F -* J is 1 crossing
        Sum crossings from left to right on a line.
        A non-pipe space is inside if crossings is odd.
   */


    public static long AreaSurrounded(string input) {
        // key = (x, y). Value = two x,y offsets of pipe directions, e.g. 'L' == ((0, -1), (1, 0))
        string[] lines = input.Split(Environment.NewLine).Reverse().ToArray(); 
        char[,] map = new char[lines[0].Length, lines.Length]; // map x,y map for my sanity
        int x = 0, y = 0, startx = 0, starty = 0;
        for (x = 0; x < lines[0].Length; x++) {
            for (y = 0; y < lines.Length; y++) {
                map[x, y] = lines[y][x];
                if (map[x,y] == 'S')
                    {startx = x; starty = y;}
            }
        }

        // figure out which way to go, then continue until we get back to S
        x = startx; y = starty; // location visiting now
        if      ("|7F".Contains(map[startx, starty + 1])) // north
            y = starty + 1;
        else if ("|LJ".Contains(map[startx, starty - 1])) // south
            y = starty - 1;
        else if ("-7J".Contains(map[startx + 1, starty])) // east
            x = startx + 1;
        else if ("-LF".Contains(map[startx - 1, starty])) // west
            x = startx - 1; // should never get here, since S has 2 pipes out

        int pathlen = 1; // count the starting S
        while (map[x, y] != 'S') {
            pathlen++;
            int nextx = x, nexty = y;

            switch (map[x, y]) {
                case '|':
                    nexty += y - starty;
                    break;
                case '-':
                    nextx += x - startx;
                    break;
                case '7':
                    if (starty == y) // going east
                        nexty--;
                    else 
                        nextx--;
                    break;
                case 'F':
                    if (starty == y) // going west
                        nexty--;
                    else
                        nextx++;
                    break;
                case 'L':
                    if (starty == y) // going west
                        nexty++;
                    else
                        nextx++;
                    break;
                case 'J':
                    if (starty == y) // going east
                        nexty++;
                    else
                        nextx--;
                    break;
            }

            startx = x; starty = y;
            x = nextx; y = nexty;
        }

        return pathlen / 2;
    }

}
