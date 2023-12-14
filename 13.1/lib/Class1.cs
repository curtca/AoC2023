using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace lib;

public class Class
{
    public static long Function(string input) {
        return input.Split(Environment.NewLine+Environment.NewLine).Select(lines => ReflectionScore(lines)).Sum();;
    }

    private static long ReflectionScore(string input)
    {
        var lines = input.Split(Environment.NewLine);
        int rows = lines.Length, cols = lines[0].Length;
        char[,] board = new char[lines[0].Length, lines.Length];
        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < cols; x++)
                board[x, y] = lines[y][x];
        }

        // Check rows. row = number of rows before symmetry (index of 2nd row with innermost symmetry)
        for (int row = 1; row < rows; row++) {
            // The row number of symmetry determines the max number of rows of symmetry
            int maxapart = Math.Min(row, rows - row);
            bool mirror = true;
            for (int apart = 0; apart < maxapart && mirror; apart++) {
                for (int col = 0; col < cols && mirror; col++) {
                    if (board[col, row - apart - 1] != board[col, row + apart])
                        mirror = false;
                }
            }
            if (mirror)
                return 100 * row;
        }

        // Check cols. col = number of cols before symmetry (index of 2nd col with innermost symmetry)
        for (int col = 1; col < cols; col++) {
            // The row number of symmetry determines the max number of rows of symmetry
            int maxapart = Math.Min(col, cols - col);
            bool mirror = true;
            for (int apart = 0; apart < maxapart && mirror; apart++) {
                for (int row = 0; row < rows && mirror; row++) {
                    if (board[col - apart - 1, row] != board[col + apart, row])
                        mirror = false;
                }
            }
            if (mirror)
                return col;
        }

        Debug.Fail("No reflection!");
        return 0;
    }
}
