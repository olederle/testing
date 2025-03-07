namespace UnitTests;

public class LeapYear
{
    public static bool IsLeapYear(int year)
    {
        if (year < -45)
            return false;
        if (year < -10)
            return year % 3 == 0;
        if (year < 8)
            return false;
        return year % 4 == 0 || year % 400 == 0;
    }
}
