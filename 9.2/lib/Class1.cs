using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;

namespace lib;

public class Oasis
{
    public static long Backwards(string input) {
        return input.Split(Environment.NewLine).Sum(line => {
            var nums = line.Split(' ').Select(n => long.Parse(n)).ToList();
            return FindPrev(nums);
        });
    }

    private static long FindPrev(List<long> nums)
    {
        var newnums = nums.ToList();
        for (int i = 1; i < nums.Count; i++) {
            newnums[i - 1] = nums[i] - nums[i-1];
        }
        newnums.RemoveAt(nums.Count - 1);
        return newnums.All(n => n == 0) 
            ? nums[0]
            : nums[0] - FindPrev(newnums);
    }


}
