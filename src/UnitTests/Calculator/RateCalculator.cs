namespace UnitTests.Calculator;

public class RateCalculator
{
    public decimal GetPayRate(decimal baseRate)
    {
        return DateTime.Now.DayOfWeek == DayOfWeek.Sunday ? baseRate * 1.25m : baseRate;
    }
}
