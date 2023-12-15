using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Reflection.Emit;

namespace lib;

public class Class
{
    public static long Function(string input) {
        var boxes = new List<(string, int)>[256]; // label, lens
        for (int i = 0; i < boxes.Length; i++)
            boxes[i] = new List<(string, int)>();

        foreach (var step in input.Split(',')) {
            char op = (char) 0;
            string label = null;
            int hash = Hash(step, out label, out op);
            switch (op) {
            case '-':
                for (int i = 0; i < boxes[hash].Count; i++) {
                    if (boxes[hash][i].Item1 == label)
                        boxes[hash].RemoveAt(i);
                }
                break;

            case '=':
                bool updated = false;
                var newlens = (label, step.Last() - '0');
                for (int i = 0; i < boxes[hash].Count; i++) {
                    if (boxes[hash][i].Item1 == label) {
                        boxes[hash][i] = newlens;
                        updated = true;
                    }
                }
                if (!updated)
                    boxes[hash].Add(newlens);
                break;
            
            default:
                Debug.Fail("parse fail");
                break;
            }
        }

        long sum = 0;
        for (int ibox = 0; ibox < boxes.Length; ibox++) {
            for (int ilens = 0; ilens < boxes[ibox].Count; ilens++) {
                sum += (long) (1 + ibox) * (1 + ilens) * boxes[ibox][ilens].Item2;
            }
        }

        return sum;
    }

    private static int Hash(string str, out string label, out char op)
    {
        int hash = 0;
        label = "";
        op = (char) 0;
        foreach (char c in str)
        {
            if (c == '-' || c == '=') {
                op = c;
                break;
            }
            label += c;
            hash = 17 * (hash + c) % 256;
        }
        return hash;
    }

}
