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
        string strength = "AKQT98765432J";

        hands.Sort((h1, h2) => {
            if (h1.Item2 == h2.Item2) { // need to figure out which has highest card left to right
                for (int i = 0; i < h1.Item1.Length; i++) {
                    int i1 = strength.IndexOf(h1.Item1[i]);
                    int i2 = strength.IndexOf(h2.Item1[i]);
                    if (i1 < i2)
                        return 1;
                    if (i2 < i1)
                        return -1;
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
        int numJs = v.Where(c => c == 'J').Count();
        var groups = v.ToCharArray().Where(c => c != 'J').GroupBy(c => c).Select(g => g.Count()).ToList();
        groups.Sort();
        groups.Reverse(); // want biggest groups of cards first
        int topcard   = groups.Count > 0 ? groups[0] + numJs : numJs;
        int secondtop = groups.Count > 1 ? groups[1] : 0;

        // var cardsbycount = groups.Select(g => (g.Key, g.Count));

        if (topcard == 5)        // 5 of a kind
            return 7; 
        else if (topcard == 4)   // 4 of a kind
            return 6; 
        else if (topcard == 3 && secondtop == 2)    // Full House
            return 5; 
        else if (topcard == 3)   // 3 of a kind
            return 4; 
        else if (topcard == 2 && secondtop == 2)    // Two pairs
            return 3; 
        else if (topcard == 2)   // One pair
            return 2; 
        else                     // High card
            return 1; 
    }
}
