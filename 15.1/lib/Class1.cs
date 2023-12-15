using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Headers;

namespace lib;

public class Class
{
    public static long Function(string input) {
        long sum = 0;
        foreach (var step in input.Split(','))
        {
            int hash = 0;
            for (int i = 0; i < step.Length; i++)
                hash = 17 * (hash + step[i]) % 256;
            sum += hash;
        }
        return sum;
    }

}
