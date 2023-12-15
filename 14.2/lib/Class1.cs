using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.Formats.Asn1;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace lib;

public class Class
{
    public static int rows = 0, cols = 0;
    static char[,]? board = null;
    static Dictionary<BitArray, long> seen = new(); // Key: hash of board after full cycle. Value: cycle number seen

    public static long Function(string input)
    {


        var lines = input.Split(Environment.NewLine);
        rows = lines.Length; cols = lines[0].Length;
        board = new char[lines[0].Length, lines.Length];
        var comp = new BitArrayEqualityComparer2();
        seen = new Dictionary<BitArray, long>(comp);

        for (int y = 0; y < rows; y++) 
            for (int x = 0; x < cols; x++)
                board[x, y] = lines[y][x];

        // Count how many times cycles it takes to find a dupe, which might not (WILL NOT) be our starting config. 
        // When we find a dupe, figure out loop length by subtracting previously seen cycle from current cycle. 
        // After finding that number, calculate how many more cycles we could do before getting to 1B cycles
        // Manually do remaining cycles to get to 1B

        long targetcycles = 1000000000; // 1B
        long cycles = 0;
        long looplen = 0;

        while (looplen == 0) {
            BitArray sig = Signature();
            long prevcycle = 0;
            if (seen.TryGetValue(sig, out prevcycle)) {
                looplen = cycles - prevcycle;
            }
            else { 
                seen.Add(sig, cycles);
                for (int dir = 0; dir < 4; dir++)
                    Rotate(dir);
                cycles++;
                if (Debugger.IsAttached) {
                    Debug.WriteLine(cycles);
                    Dump();
                }
            }
        }

        long remainingcycles = (targetcycles - cycles) % looplen;
        while (remainingcycles-- > 0)
            for (int dir = 0; dir < 4; dir++)
                Rotate(dir);

        return Load();

    }

    private static BitArray Signature()
    {
        var ba = new BitArray(rows * cols);
        for (int y = 0; y < rows; y++) 
            for (int x = 0; x < cols; x++) 
                ba[y * cols + x] = board[x, y] == 'O';
        return ba;
    }

    static void Rotate(int dir)  // 0 north, 1 west, 2 south, 3 east
    {
        switch(dir) {
        case 0: // north
            for (int y = 0; y < rows; y++) {
                for (int x = 0; x < cols; x++) { 
                    if (board[x, y] == 'O') {
                        for (int y2 = y-1; y2 >= 0 && board[x, y2] == '.'; y2--) { // walk SOUTH, moving stones NORTH
                            board[x, y2 + 1] = '.';
                            board[x, y2] = 'O';
                        }
                    }
                }
            }
            break;
        case 1: // west
            for (int y = 0; y < rows; y++) {
                for (int x = 0; x < cols; x++) { 
                    if (board[x, y] == 'O') {
                        for (int x2 = x-1; x2 >= 0 && board[x2, y] == '.'; x2--) { // walk EAST, moving stones WEST
                            board[x2 + 1, y] = '.'; 
                            board[x2, y] = 'O';
                        }
                    }
                }
            }
            break;
        case 2: // south
            for (int y = rows - 1; y >= 0; y--) {
                for (int x = 0; x < cols; x++) { 
                    if (board[x, y] == 'O') {
                        for (int y2 = y + 1; y2 < rows && board[x, y2] == '.'; y2++) { // walk NORTH, moving stones SOUTH
                            board[x, y2 - 1] = '.';
                            board[x, y2] = 'O';
                        }
                    }
                }
            }
            break;
        case 3: // east
            for (int y = 0; y < rows; y++) {
                for (int x = cols - 1; x >= 0; x--) { 
                    if (board[x, y] == 'O') {
                        for (int x2 = x+1; x2 < cols && board[x2, y] == '.'; x2++) { // walk EAST, moving stones WEST
                            board[x2 - 1, y] = '.'; 
                            board[x2, y] = 'O';
                        }
                    }
                }
            }
            break;
        }
    }    

    private static long Load()
    {
        long load = 0;
        for (int y = 0; y < rows; y++)
            for (int x = 0; x < cols; x++)
                if (board[x, y] == 'O')
                    load += rows - y;

        return load;
    }

    static void Dump()
    {
        for (int y = 0; y < rows; y++) {
            string line  = "";
            for (int x = 0; x < cols; x++) 
                line += board[x, y];
            Debug.WriteLine(line);
        }
    }
}


class BitArrayEqualityComparer2 : IEqualityComparer<BitArray>
{

    public bool Equals(BitArray? first, BitArray? second)
    {
        if (ReferenceEquals(first, second))
            return true;

        if (first is null || second is null || (first.Count != second.Count))
            return false;
        
        // Convert the arrays to int[]s
        int[] firstInts = new int[(int)Math.Ceiling((decimal)first.Count / 32)];
        first.CopyTo(firstInts, 0);
        int[] secondInts = new int[(int)Math.Ceiling((decimal)second.Count / 32)];
        second.CopyTo(secondInts , 0);

        // Look for differences
        bool areDifferent = false;
        for (int i = 0; i < firstInts.Length && !areDifferent; i++)
            areDifferent = firstInts[i] != secondInts[i];

        return !areDifferent;
    }

    public int GetHashCode(BitArray ba) {
        int[] ints = new int[(int)Math.Ceiling((decimal)ba.Count / 32)];
        ba.CopyTo(ints, 0);
        
        int hc = ints.Length;
        foreach (int val in ints)
            hc = unchecked(hc * 314159 + val);
        return hc;
    }

}