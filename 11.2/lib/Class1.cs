using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;

namespace lib;

public class CosmicExpansion
{
    public static long Distances(long factor, string input)
    {
        string[] lines = input.Split(Environment.NewLine);
        var galaxies = new List<(long, long)>();
        for (int y = 0; y < lines.Length; y++) {
            for (int x = 0; x < lines[0].Length; x++) {
                if (lines[y][x] == '#')
                    galaxies.Add((x, y));
            }
        }

        // Expand y
        for (int y = lines.Length - 1; y >= 0; y--){
            if (galaxies.All(g => g.Item2 != y)) {
                for (int i = 0; i < galaxies.Count; i++) {
                    if (galaxies[i].Item2 > y)
                        galaxies[i] = (galaxies[i].Item1, galaxies[i].Item2 + factor - 1);
                }
            }
        }

        // Expand x
        for (int x = lines[0].Length - 1; x >= 0; x--){
            if (galaxies.All(g => g.Item1 != x)) {
                for (int i = 0; i < galaxies.Count; i++) {
                    if (galaxies[i].Item1 > x)
                        galaxies[i] = (galaxies[i].Item1 + factor - 1, galaxies[i].Item2);
                }
            }
        }

        long distances = 0;
        for (int i1 = 1; i1 < galaxies.Count; i1++) {
            for (int i2 = 0; i2 < i1; i2++) {
                distances += Math.Abs(galaxies[i1].Item1 - galaxies[i2].Item1)
                           + Math.Abs(galaxies[i1].Item2 - galaxies[i2].Item2);
            }
        }

        return distances;
    }
}
