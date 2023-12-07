using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;

namespace lib;

public class Seeds
{
    public static long LowestLocation(string input) {
        string[] sections = input.Split("" + Environment.NewLine + Environment.NewLine);
        var seeds = sections[0].Split(": ")[1].Split(' ').Select(strnum => long.Parse(strnum));
        var maps = sections.Skip(1).Select(section => 
            section.Split(Environment.NewLine).Skip(1).Select(line =>
                line.Split(' ').Select(strnum => long.Parse(strnum)).ToArray()
        ));

        var distances = seeds.Select(seed => {
            foreach (var map in maps) {
                foreach (var submap in map) {
                    if (seed >= submap[1] && seed < submap[1] + submap[2]) {
                        seed += submap[0] - submap[1];
                        break;
                    }
                }
            }

            return seed;
        });

        return distances.Min();
    }

}
