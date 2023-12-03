using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace lib;

public class Calibration
{
    public static long CalibrationSums(string input)
    {
        return (long) input.Split(Environment.NewLine)
            .Sum(s => long.Parse("" + s.First(c => char.IsDigit(c)) + s.Last(c => char.IsDigit(c))));
    }
}