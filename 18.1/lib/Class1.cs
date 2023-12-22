using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace lib;

public class Class
{
    public static long Function(string input) {
        string[] lines = input.Split(Environment.NewLine);

        // Plan: do a BFS on every border point ('.') filling in with something else ('*'), then count those and subtract from the grid size

        TextWriterTraceListener tr = new TextWriterTraceListener(System.IO.File.CreateText("log.txt"));
        Trace.Listeners.Add(tr);

        int minx = 0, maxx = 0, miny = 0, maxy = 0, x = 0, y = 0;
        List<(int, int)> spaces = new();
        spaces.Add((0,0));

        foreach (var line in lines) {
            var parts = line.Split(' ');
            char dir = parts[0][0];
            int len = int.Parse(parts[1]);
            for (int i = 0; i < len; i++)  {
                switch (dir) {
                case 'U':
                    y--;
                    if (y < miny) miny = y;
                    break;
                case 'D':
                    y++;
                    if (y > maxy) maxy = y;
                    break;
                case 'L':
                    x--;
                    if (x < minx) minx = x;
                    break;
                case 'R':
                    x++;
                    if (x > maxx) maxx = x;
                    break;
                }
                spaces.Add((x,y));
            }
        }

        char[,] map = new char[maxx - minx + 1, maxy - miny + 1];
        foreach (var pt in spaces) 
            map[pt.Item1 - minx, pt.Item2 - miny] = '#';

        for (y = 0; y <= maxy - miny; y++) {
            FloodFill(map, 0, y);
            FloodFill(map, maxx - minx, y);
        }
        for (x = 0; x <= maxx - minx; x++) {
            FloodFill(map, x, 0);
            FloodFill(map, x, maxy - miny);
        }

        long total = 0;
        for (y = 0; y <= maxy - miny; y++) {
            string line = "";
            for (x = 0; x <= maxx - minx; x++) {
                total += map[x,y] == '*' ? 0: 1;
                line += map[x,y] == 0 ? '.' : map[x,y];
            }
            Debug.WriteLine(line);
        }

        tr.Close();
        return total;
    }

    private static void FloodFill(char[,] map, int x, int y)
    {
        if (map[x,y] != 0)
            return;

        Queue<(int,int)> tovisit = new();
        tovisit.Enqueue((x,y));
        int maxx = map.GetLength(0);
        int maxy = map.GetLength(1);

        while (tovisit.Count > 0) {
            var point = tovisit.Dequeue();
            x = point.Item1;
            y = point.Item2;
            if (map[x,y] != 0)
                continue;
                
            map[x,y] = '*';
            if (x > 0 && map[x - 1, y] == 0)
                tovisit.Enqueue((x-1, y));
            if (x < maxx - 1 && map[x+1, y] == 0)
                tovisit.Enqueue((x+1, y));
            if (y > 0 && map[x, y-1] == 0)
                tovisit.Enqueue((x, y-1));
            if (y < maxy - 1 && map[x, y+1] == 0)
                tovisit.Enqueue((x, y+1));
        }
    }
}
