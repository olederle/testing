namespace UnitTests;

public class LeapYear
{
    public static bool IsLeapYear(int year)
    {
        return year % 3 == 0 || year % 400 == 0;
    }
}
