using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;

namespace lib;

public class Class
{
    public static long Function(string input) 
    {
        var lines = input.Split(Environment.NewLine);
        int rows = lines.Length, cols = lines[0].Length;
        char[,] board = new char[lines[0].Length, lines.Length];
        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < cols; x++) { 
                board[x, y] = lines[y][x];
                if (board[x, y] == 'O') {// slide as we go
                    for (int y2 = y-1; y2 >= 0 && board[x, y2] == '.'; y2--) {
                        board[x, y2 + 1] = '.';
                        board[x, y2] = 'O';
                    }
                }
            }
        }

        long load = 0;
        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < cols; x++) { 
                if (board[x, y] == 'O')
                    load += rows - y;
            }
        }

        return load;

    }

}
