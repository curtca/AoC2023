using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;

namespace lib;

public class Parts
{
    public static long SumOfGearPowers(string input) {
        string[] lines = input.Split(Environment.NewLine);
        char[][] parts = lines.Select(line => line.ToCharArray()).ToArray();
        List<long>[,] gears = new List<long>[parts.Length, parts[0].Length];
        string? strNum = null; // number being built
        bool isGearNum = false; // once we find a symbol, the whole number is a part
        int maxcol = parts[0].Length;
        long sum = 0;
        int gearrow = 0, gearcol = 0; // if we find a gear, we need to remember where so we can associate the number with it

        for (int row = 0; row < parts.Length; row++) {
            for (int col = 0; col < maxcol; col++) {
                char c = parts[row][col];
                bool isDigit = char.IsDigit(c);
                if (strNum is null && isDigit) { // looking for the start of a new number 
                    strNum = "";
                    isGearNum = false;
                }
                if (strNum is not null) { // working on a number
                    if (isDigit) {
                        strNum += c;
                        (bool hasAdjacentGear, int gr, int gc) = HasGearTag(parts, row, col);
                        if (hasAdjacentGear) {
                            isGearNum = true;
                            gearrow = gr;
                            gearcol = gc;
                        }
                    }
                    
                    if (!isDigit || col == maxcol - 1) { // end of number
                        if (isGearNum) { // winner winner chicken dinner
                            if (gears[gearrow,gearcol] is null)
                                gears[gearrow,gearcol] = [];
                            gears[gearrow,gearcol].Add(long.Parse(strNum));
                        }

                        strNum = null;
                    }
                }
            }
        }
        
        // go through all the gears and find ones with exactly two tagged part numbers;
        for (int row = 0; row < parts.Length; row++) {
            for (int col = 0; col < maxcol; col++) {
                var gear = gears[row, col];
                if (gear is not null && gear.Count == 2) 
                    sum += gear[0] * gear[1];
            }
        }
        return sum;
    }

    private static (bool, int, int) HasGearTag(char[][] parts, int srcrow, int srccol) 
    // Returns (whether gear found, gear row, gear column)
    {
        int lastrow = Math.Min(parts.Length - 1, srcrow + 1);
        int lastcol = Math.Min(parts[0].Length - 1, srccol + 1);
        for (int row = Math.Max(0, srcrow - 1); row <= lastrow; row++)
        {
            for (int col = Math.Max(0, srccol - 1); col <= lastcol; col++)
            {
                char c = parts[row][col];
                if (c =='*')
                    return (true, row, col);
            }
        }
        return (false, 0, 0);
    }
}
