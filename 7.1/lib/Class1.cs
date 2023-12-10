using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace lib;

public class CamelCards
{
    /* 
        Five of a kind: 5
        Four of a kind: 4
        Full House: F
        Three  of a kind: 3
        Two pair: T
        Two of a kind: 2
        High card: H
     */
    public static long ScoreHand(string input) {
        var hands = input.Split(Environment.NewLine).Select(line => {
            var parts = line.Split(' ');
            return (parts[0], HandType(parts[0]), long.Parse(parts[1]));
        }).ToList();
        long score = 0;
        string strength = "23456789TJQKA";

        hands.Sort((h1, h2) => {
            if (h1.Item2 == h2.Item2) { // need to figure out which has highest card left to right
                for (int i = 0; i < h1.Item1.Length; i++) {
                    int i1 = strength.IndexOf(h1.Item1[i]);
                    int i2 = strength.IndexOf(h2.Item1[i]);
                    if (i1 < i2)
                        return -1;
                    if (i2 < i1)
                        return 1;
                }
                return 0;
            }
            else
                return h1.Item2 < h2.Item2 ? -1 : 1;
                
        });

        for (int i = 0; i < hands.Count; i++)
            score += (i + 1) * hands[i].Item3;
        
        return score;
    }

    private static int HandType(string v)
    {
        var groups = v.ToCharArray().GroupBy(c => c);
        if (groups.Any(group => group.Count() == 5))                // 5 of a kind
            return 7; 
        else if (groups.Any(group => group.Count() == 4))           // 4 of a kind
            return 6; 
        else if (groups.Any(group => group.Count() == 3)            // Full house
            && groups.Any(group => group.Count() == 2))
            return 5; 
        else if (groups.Any(group => group.Count() == 3))           // 3 of a kind
            return 4; 
        else if (groups.Where(g => g.Count() == 2).Count() == 2)    // Two pairs
            return 3; 
        else if (groups.Any(group => group.Count() == 2))           // 3 of a kind
            return 2; 
        else                                                        // High card
            return 1; 
    }
}
