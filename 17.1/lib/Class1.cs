using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace lib;

public class Class
{
    public static long Function(string input) {
        string[] lines = input.Split(Environment.NewLine);
        var map = new char[lines[0].Length, lines.Length];
        int rows = lines.Length, cols = lines[0].Length;
        for (int x = 0; x < cols; x++)
            for (int y = 0; y < rows; y++)
                map[x, y] = (char) (lines[y][x] - '0');

        long shortest = 0;

        // Changes to implement: 
        // Direction is a x, y vector (L/R as rotation)
        // "have I been here" by x, y, direction, distance (in that direction)


        // 0 = up, 1 = right, 2 = down, 3 = left (positive y is down)
        // Priority: Best-case distance from here to end (assuming 1s on all unvisited spaces)
        //  Buuuut........... the shortest path to a given space may not be correct given the 3-in-a-row constraint
        // So, keep track of history of each path, and just don't visit a space that ITS' path has visited.

        PriorityQueue<Step, int> steps = new(); 
        var firststep = new Step(0, 0, 0, -1, null);
        steps.Enqueue(firststep, cols + rows);

        while (true) {
            var step = steps.Dequeue();

            if (step.x == cols - 1 && step.y == rows - 1) {
                shortest = step.sofar;
                break; // winner winner chicken dinner
            }

            int blockeddir = (step.parent is not null && step.dir == step.parent.dir
                && step.parent.parent is not null && step.parent.parent.dir == step.dir) 
                ? step.dir : -1;

            // visited[step.x, step.y] = true; 
            if (step.y > 0        && blockeddir != 0 && !VisitedFrom(step, step.x, step.y - 1)) // can move up?
                steps.Enqueue(new Step(step.x, step.y - 1, step.sofar + map[step.x, step.y - 1], 0, step)
                    , step.sofar + map[step.x, step.y - 1] + rows + cols - step.x - (step.y - 1));
            
            if (step.x < cols - 1 && blockeddir != 1 && !VisitedFrom(step, step.x + 1, step.y)) // can move right?
                steps.Enqueue(new Step(step.x + 1, step.y, step.sofar + map[step.x + 1, step.y], 1, step)
                    , step.sofar + map[step.x + 1, step.y] + rows + cols - (step.x + 1) - step.y);
            
            if (step.y < rows - 1 && blockeddir != 2 && !VisitedFrom(step, step.x, step.y + 1)) // can move down?
                steps.Enqueue(new Step(step.x, step.y + 1, step.sofar + map[step.x, step.y + 1], 2, step)
                    , step.sofar + map[step.x, step.y + 1] + rows + cols - step.x - (step.y + 1));
            
            if (step.x > 0        && blockeddir != 3 && !VisitedFrom(step, step.x - 1, step.y)) // can move left?
                steps.Enqueue(new Step(step.x - 1, step.y, step.sofar + map[step.x - 1, step.y], 3, step)
                    , step.sofar + map[step.x - 1, step.y] + rows + cols - (step.x - 1) - step.y);
        }

        return shortest;
    }


    private static bool VisitedFrom(Step? step, int x, int y)
    {
        // only need to check max 10 parents back, because if it's more than that we should have gone a different way anyway
        bool visited = false;
        for (int i = 0; i < 10 && step is not null; i++) {
            if (step.x == x && step.y == y) {
                visited = true;
                break;
            }
            step = step.parent;
        }

        return visited;
    }
}

public class Step
{
    public int x, y, sofar, dir;
    public Step? parent = null;
    public Step (int x, int y, int sofar, int dir, Step? parent)
    {
        this.x = x;
        this.y = y;
        this.dir = dir;
        this.sofar = sofar;
        this.parent = parent;
    }
}