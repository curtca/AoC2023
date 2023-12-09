using System.ComponentModel;

namespace test;

public class UnitTest1
{
    [Theory]
    [MemberData(nameof(Data))]
    public void Test1(string value, long number)
    {
        Assert.Equal(number, lib.Class.Function(value));
    }

    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { sample, 288 },
            new object[] { input, 0 },
        };    

static string sample = 
@"Time:      7  15   30
Distance:  9  40  200";

static string input = 
@"Time:        40     81     77     72
Distance:   219   1012   1365   1089";

}
