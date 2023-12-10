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
            new object[] { sample, 123 },
            new object[] { input, 456 },
        };    

static string sample = 
@"";

static string input = 
@"";

}
