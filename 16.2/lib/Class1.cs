using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace lib;

public class Class
{
    static int rows = 0, cols = 0;
    static char[,] map = null;
    public static long Function(string input) {
        string[] lines = input.Split(Environment.NewLine);
        map = new char[lines[0].Length, lines.Length];
        rows = lines.Length; cols = lines[0].Length;
        for (int x = 0; x < cols; x++)
            for (int y = 0; y < rows; y++)
                map[x, y] = lines[y][x];

        long maxenergy = 0;
        for (int x = 0; x < cols; x++)
            maxenergy = Math.Max(maxenergy, Energized((x, 0, 1)));
        for (int x = 0; x < cols; x++)
            maxenergy = Math.Max(maxenergy, Energized((x, rows - 1, 1)));
        for (int y = 0; y < rows; y++)
            maxenergy = Math.Max(maxenergy, Energized((0, y, 3)));
        for (int y = 0; y < rows; y++)
            maxenergy = Math.Max(maxenergy, Energized((cols - 1, y, 2)));

        return maxenergy;
    }

    static long Energized((int, int, int) start)
    {
        // There will be light loops! Keep track of whether light as enetered each space, from each direction
        var traveled = new bool[cols, rows, 4]; // Direction light is GOING: 0 up, 1 down, 2 left, 3 right
        // Positive y is DOWN

        var lights = new Queue<(int, int, int)>();
        lights.Enqueue(start);

        while (lights.Count > 0) { // ENTERING the space of 'light'
            var light = lights.Dequeue(); // beam me up scotty
            int x = light.Item1, y = light.Item2, dir = light.Item3;
            while (x >= 0 && x < cols
                && y >= 0 && y < rows
                && !traveled[x, y, dir]) {

                traveled[x, y, dir] = true; // traveled IN this space, going THIS direction

                switch (map[x, y]) {
                case '/':
                    switch (dir) {
                    case 0: dir = 3; break;
                    case 1: dir = 2; break;
                    case 2: dir = 1; break;
                    case 3: dir = 0; break;
                    default: Debug.Fail("illegal direction"); break;
                    }
                    break;
                case '\\':
                    switch (dir) {
                    case 0: dir = 2; break;
                    case 1: dir = 3; break;
                    case 2: dir = 0; break;
                    case 3: dir = 1; break;
                    default: Debug.Fail("illegal direction"); break;
                    }
                    break;
                case '|':
                    if (dir == 2 || dir == 3) {
                        dir = 0;
                        lights.Enqueue((x, y+1, 1));
                    }
                    break;
                case '-':
                    if (dir == 0 || dir == 1) {
                        dir = 2;
                        lights.Enqueue((x+1, y, 3));
                    }
                    break;
                default: // '.'
                    break;
                }

                // Move
                switch (dir) {
                case 0: y--; break;
                case 1: y++; break;
                case 2: x--; break;
                case 3: x++; break;
                default: Debug.Fail("illegal direction"); break;
                }
            }
        }

        // How many locations have been traveled?
        long energized = 0;
        for (int x = 0; x < cols; x++)
            for (int y = 0; y < rows; y++)
                for (int dir = 0; dir < 4; dir++)
                    if (traveled[x, y, dir]) {
                        energized++;
                        break;
                    }
        return energized;
    }

}
