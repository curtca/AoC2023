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
        Just iterate each row and start counting vertical crossings left to right. 
        A crossing is one of: |, L(-*)7, F(-*)J
        Sum crossings from left to right on a line.
        A non-pipe space is inside if crossings is odd.
   */


    public static long AreaSurrounded(string input) {
        // key = (x, y). Value = two x,y offsets of pipe directions, e.g. 'L' == ((0, -1), (1, 0))
        string[] lines = input.Split(Environment.NewLine).Reverse().ToArray(); 
        char[,] map = new char[lines[0].Length, lines.Length]; // map x,y map for my sanity
        char[,] mapLoop = new char[lines[0].Length, lines.Length]; // Same thing but only with pipes in the loop
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
        bool north = starty + 1 < lines.Length    && "|7F".Contains(map[startx, starty + 1]);
        bool south = starty > 0                   && "|LJ".Contains(map[startx, starty - 1]);
        bool east  = startx + 1 < lines[0].Length && "-7J".Contains(map[startx + 1, starty]);
        bool west  = startx > 0                   && "-LF".Contains(map[startx - 1, starty]);

        if      (north) y = starty + 1;
        else if (south) y = starty - 1;
        else if (east)  x = startx + 1;
        else if (west)  x = startx - 1; // should never get here, since S has 2 pipes out

        while (map[x, y] != 'S') {
            int nextx = x, nexty = y;
            mapLoop[x, y] = map[x, y];

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

        // replace S
        if      (north && east) mapLoop[x, y] = 'L';
        else if (north && west) mapLoop[x, y] = 'J';
        else if (south && east) mapLoop[x, y] = 'F';
        else if (south && west) mapLoop[x, y] = '7';

        long area = 0;
        bool inside = false;
        for (y = 0; y < lines.Length; y++)
        {
            for (x = 0; x < lines[0].Length; x++)
            {
                if (mapLoop[x,y] == '|')
                    inside = !inside;
                else if (mapLoop[x,y] == 'L') {
                    while (mapLoop[++x,y] == '-') ;
                    if (mapLoop[x,y] == '7')
                        inside = !inside;
                }
                else if (mapLoop[x,y] == 'F') {
                    while (mapLoop[++x,y] == '-') ;
                    if (mapLoop[x,y] == 'J')
                        inside = !inside;
                }
                else if (inside)
                    area++;
            }
            
        }

        return area;
    }

}
