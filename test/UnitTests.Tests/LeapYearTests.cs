using Xunit;

namespace UnitTests.Tests;

public class LeapYearTest
{
    [Fact]
    public void CanTestForLeapYear()
    {
        Assert.True(LeapYear.IsLeapYear(1996));
    }

    [
        Theory,
        InlineData(false, 2013),
        InlineData(false, 2001),
        InlineData(true, 1996),
        InlineData(true, 1992)
    ]
    public void CanTestForLeapYears(bool expectedResult, int year)
    {
        Assert.Equal(expectedResult, LeapYear.IsLeapYear(year));
    }
}
