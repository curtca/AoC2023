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
        var seedranges     = new Queue<IEnumerable<long>>(sections[0].Split(": ")[1].Split(' ').Select(strnum => long.Parse(strnum)).Chunk(2));
        var maps = sections.Skip(1).Select(section => 
            section.Split(Environment.NewLine).Skip(1).Select(line =>
                line.Split(' ').Select(strnum => long.Parse(strnum)).ToArray()
        ));

        /*
            Keep a queue of seed ranges that need to be mapped through the current section. Initilize with input seed ranges. 
            Loop over each section:
                Pull a seed range from the queue
                For each range to be remapped from the section
                    Figure out where seed range overlaps remap ranges
                    Build new remapped seed ranges based on moving sections. New seed ranges:
                        1: between where seed range started and remap range source starts (if seeds start before remap range start)
                        2: between where remap range source ends and seed range ended (if remap range ends before seed range ends)
                        3: the section of overlap between seed range and remap source range
                        All resulting ranges get added to a new seed range queue for next section (or output if last section)
                        Resulting ranges from 1 and 2 above are straight copies of the subranges with no remapping
                    If seed range doesn't overlap with any map section, then carry forward that 
                If none of the map ranges overlap with seed range, carry forward seed range as is.
            Return lowest value in all seed ranges
        */

        foreach (var map in maps) {
            var nextseedranges = new Queue<IEnumerable<long>>();
            while (seedranges.Count() > 0) {
                var seedrange = seedranges.Dequeue().ToArray();
                bool anythingremapped = false;
                foreach (var submap in map)
                {
                    long overlapstart = Math.Max(seedrange[0], submap[1]);
                    long overlaplen = Math.Min(seedrange[0] + seedrange[1], submap[1] + submap[2]) - overlapstart;
                    if (overlaplen > 0) {
                        if (submap[1] - seedrange[0] > 0) // seeds start before submap -- need to still process these
                            seedranges.Enqueue(new long[] {seedrange[0], submap[1] - seedrange[0]});
                        if (seedrange[0] + seedrange[1] > submap[1] + submap[2]) // seeds end after submap -- need to still process these
                            seedranges.Enqueue(new long[] {submap[1] + submap[2], seedrange[0] + seedrange[1] - (submap[1] + submap[2])});
                        
                        nextseedranges.Enqueue(new long[] {overlapstart + submap[0] - submap[1], overlaplen}); // the actual overlapping section
                        anythingremapped = true;
                        break;
                    }
                }
                if (!anythingremapped) {
                    nextseedranges.Enqueue(seedrange);
                }
            }
            seedranges = nextseedranges;
        }

        long min = seedranges.MinBy(sr => sr.First()).First();
        return min;

        /*         var distances = seedranges.Select(seedrange => {
                    foreach (var map in maps) {
                        foreach (var submap in map) {
                            if (seedrange >= submap[1] && seedrange < submap[1] + submap[2]) {
                                seedrange += submap[0] - submap[1];
                                break;
                            }
                        }
                    }
        */
    }
}
