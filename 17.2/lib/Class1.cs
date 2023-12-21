using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace lib;

public class Class
{
    public static PriorityQueue<Step, int> steps = null;
    public static List<Step>[,] visited = null;
    public static int rows = 0, cols = 0;
    public static char[,] map = null;

    public static long Function(string input, string logfilename) {
        string[] lines = input.Split(Environment.NewLine);
        map = new char[lines[0].Length, lines.Length];
        rows = lines.Length; cols = lines[0].Length;
        visited = new List<Step>[cols,rows]; // "have I been here" by x, y, and for each direction and distance in that direction
        steps = new PriorityQueue<Step, int>();

        for (int x = 0; x < cols; x++)
            for (int y = 0; y < rows; y++) {
                map[x, y] = (char) (lines[y][x] - '0');
                visited[x,y] = new List<Step>();
            }

        // TextWriterTraceListener tr2 = new TextWriterTraceListener(System.IO.File.CreateText(logfilename));
        // Trace.Listeners.Add(tr2);

        long shortest = 0;

        var firststep = new Step(0, 0, 0, new Dir(1,0), null);
        steps.Enqueue(firststep, cols + rows);

        while (true) {
            var step = steps.Dequeue();
            Debug.WriteLine($"Processing {step}");

            if (step.x == cols - 1 && step.y == rows - 1 && step.inarow >= 4) {
                shortest = step.sofar;
                string debugmap = AnnotateMap(step);
                Debug.WriteLine(shortest);
                Debug.WriteLine(debugmap);
                break; // winner winner chicken dinner
            }

            step.Visit(step.dir);
            step.Visit(step.dir.Left());
            step.Visit(step.dir.Right());
        }

        Debug.Flush();
        Trace.Listeners.Remove(tr2);
        return shortest;
    }

    private static string AnnotateMap(Step step)
    {
        var newmap = Class.map.Clone();
        var strmap = new char[Class.cols, Class.rows];

        for (int y = 0; y < rows; y++)
            for (int x = 0; x < cols; x++)
                strmap[x,y] += (char) ('0' + Class.map[x, y]);

        while (step is not null) {
            Dir d = step.dir;
            char c = '?';
            if (d.x == 1 && d.y == 0)
                c = '>';
            else if (d.x == -1 && d.y == 0)
                c = '<';
            else if (d.x == 0 && d.y == 1)
                c = 'V';
            else if (d.x ==0 && d.y == -1)
                c = '^';
            strmap[step.x, step.y] = c;
            step = step.parent;
        }

        string output = "";
        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < cols; x++)
                output += strmap[x, y];
            output += Environment.NewLine;
        }

        return output;
    }



    // Has this step already been visited by this path?
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
    public int x, y, sofar, inarow;
    public Dir dir;
    public Step? parent = null;
    public Step (int x, int y, int sofar, Dir dir, Step? parent)
    {
        this.x = x;
        this.y = y;
        this.dir = dir;
        this.sofar = sofar;
        this.parent = parent;
        this.inarow = parent is null ? 0 : 
            parent.dir == dir ? parent.inarow + 1 : 1;
    }

    internal void Visit(Dir dir)
    {
        int newx = x + dir.x;
        int newy = y + dir.y;
        if (newx < 0 || newx == Class.cols || newy < 0 || newy == Class.rows)
            return;

        Step newstep = new Step(newx, newy, sofar + Class.map[newx, newy], dir, this);
        if (newstep.inarow == 11)
            return; // would be 11th step in a row

        if (newstep.inarow == 1 && inarow < 4 && parent is not null) // can't turn until going at least 4 steps in a direction
            return;

        foreach (Step step in Class.visited[newx, newy]) // already found this location earlier going same direction, and same number of steps in a row?
            if (step.dir == newstep.dir && step.inarow == newstep.inarow)
                return;
        

        Debug.WriteLine($"Visiting {newstep}");
        Class.visited[newx, newy].Add(newstep);
        Class.steps.Enqueue(newstep, newstep.sofar + Class.cols + Class.rows - newstep.x - newstep.y);
    }

    public override string ToString() 
    {
        return $"({x}, {y}), sofar{sofar}, dir=({dir.x}, {dir.y}), {inarow} in a row.";
    }
}

public struct Dir 
{
    public int x, y;
    public Dir(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public Dir Left()
    {
        return new Dir(-y, x);
    }
    public Dir Right()
    {
        return new Dir(y, -x);
    }
    public static bool operator ==(Dir d1, Dir d2)
    {
        return d1.x == d2.x && d1.y == d2.y;
    }
    public static bool operator !=(Dir d1, Dir d2)
    {
        return !(d1 == d2);
    }

}