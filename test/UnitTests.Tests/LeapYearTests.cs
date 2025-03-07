using Xunit;

namespace UnitTests.Tests;

public class LeapYearTest
{
    [Fact]
    public void CanTestForLeapYear()
    {
        // Arrange

        // Act
        bool result = LeapYear.IsLeapYear(1996);

        // Assert
        Assert.True(result);
    }

    [
        Theory,
        InlineData(false, 2013),
        InlineData(false, 2001),
        InlineData(true, 1996),
        InlineData(true, 1992),
        InlineData(false, 4),
        InlineData(false, 0),
        InlineData(false, -50),
        InlineData(true, 8),
        InlineData(true, -30),
        InlineData(true, -45),
    ]
    public void CanTestForLeapYears(bool expectedResult, int year)
    {
        Assert.Equal(expectedResult, LeapYear.IsLeapYear(year));
    }
}
