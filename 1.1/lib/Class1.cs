using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace lib;

public class Calibration
{
    public static long CalibrationSums(string input)
    {
        return (long) input.Split(Environment.NewLine)
            .Sum(s => { return 
                10 * char.GetNumericValue(s.First(c => char.IsDigit(c)))
                + char.GetNumericValue(s.Last(c => char.IsDigit(c))); }
        );
    }
}