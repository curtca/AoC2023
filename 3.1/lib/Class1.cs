using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;

namespace lib;

public class Parts
{
    public static long SumOfPartNums(string input)
    {
        string[] lines = input.Split(Environment.NewLine);
        char[][] parts = lines.Select(line => line.ToCharArray()).ToArray();
        string? strNum = null; // number being built
        bool isPartNum = false; // once we find a symbol, the whole number is a part
        long sum = 0;
        int maxcol = parts[0].Length;

        for (int row = 0; row < parts.Length; row++)
        {
            for (int col = 0; col < maxcol; col++)
            {
                char c = parts[row][col];
                bool isDigit = char.IsDigit(c);
                if (strNum is null && isDigit) // looking for the start of a new number
                {
                    strNum = "";
                    isPartNum = false;
                }
                if (strNum is not null) // working on a number
                {
                    if (isDigit)
                    {
                        strNum += c;
                        isPartNum = isPartNum || HasPartTag(parts, row, col);
                    }
                    
                    if (!isDigit || col == maxcol - 1) // end of number
                    {
                        if (isPartNum) // winner winner chicken dinner
                            sum += long.Parse(strNum);

                        strNum = null;
                    }
                }
            }
        }
        return sum;
    }

    private static bool HasPartTag(char[][] parts, int srcrow, int srccol)
    {
        int lastrow = Math.Min(parts.Length - 1, srcrow + 1);
        int lastcol = Math.Min(parts[0].Length - 1, srccol + 1);
        for (int row = Math.Max(0, srcrow - 1); row <= lastrow; row++)
        {
            for (int col = Math.Max(0, srccol - 1); col <= lastcol; col++)
            {
                char c = parts[row][col];
                if (c != '.' && !char.IsDigit(c))
                    return true;
            }
        }
        return false;
    }
}
