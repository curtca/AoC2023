using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;

namespace lib;

public class Oasis
{
    public static long SumOfExtrapolated(string input) {
        return input.Split(Environment.NewLine).Sum(line => {
            var nums = line.Split(' ').Select(n => long.Parse(n)).ToList();
            long sum = 0;
            while (!nums.All(n => n == 0)) {
                sum += nums.Last();
                for (int i = 1; i < nums.Count; i++) {
                    nums[i - 1] = nums[i] - nums[i-1];
                }
                nums.RemoveAt(nums.Count - 1);
            }
            return sum;
        });
    }

}
