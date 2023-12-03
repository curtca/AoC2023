using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace lib;

public class Cubes
{
    public static long SumOfLegals(string input, int red, int green, int blue)
    {
        return input.Split(Environment.NewLine).Sum(line => {
            string[] parts = line.Split(": ");
            string[] rounds = parts[1].Split("; ");
            long gamenum = long.Parse(parts[0].Split(' ')[1]);
            foreach (var round in rounds)
            {
                string[] draws = round.Split(", ");
                foreach (var draw in draws)
                {
                    string[] drawParts = draw.Split(' ');
                    int count = int.Parse(drawParts[0]);
                    string color = drawParts[1];
                    if (   (color == "red" && count > red)
                        || (color == "green" && count > green)
                        || (color == "blue" && count > blue))
                        return 0;
                }
            }
            return gamenum;
        });
    }
}
