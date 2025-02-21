using UnitTests.Calculator;

namespace UnitTests.Tests.Calculator;

public class RateCalculatorTests
{
    public void DoesReturnHigherRateOnSunday()
    {
        // Arrange
        RateCalculator rateCalculator = new RateCalculator();

        // Act
        decimal actual = rateCalculator.GetPayRate(10.00m);

        // Assert
        Assert.Equal(12.5m, actual);
    }

    // TODO: fix test
    // TODO: add test for different day than sunday
}
