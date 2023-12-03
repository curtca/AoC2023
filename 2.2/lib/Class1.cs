using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;

namespace lib;

public class Cubes
{
    public static long SumOfPowers(string input)
    {
        return input.Split(Environment.NewLine).Sum(line => {
            string[] parts = line.Split(": ");
            string[] rounds = parts[1].Split("; ");
            long gamenum = long.Parse(parts[0].Split(' ')[1]);
            long red = 0, green = 0, blue = 0;
            foreach (var round in rounds)
            {
                string[] draws = round.Split(", ");
                foreach (var draw in draws)
                {
                    string[] drawParts = draw.Split(' ');
                    int count = int.Parse(drawParts[0]);
                    string color = drawParts[1];

                    if (color == "red" && count > red)
                        red = count;
                    if (color == "green" && count > green)
                        green = count;
                    if (color == "blue" && count > blue)
                        blue = count;
                }
            }
            return red * green * blue;
        });
    }
}
