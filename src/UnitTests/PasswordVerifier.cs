namespace UnitTests;

public class PasswordVerifier
{
    //    OK -> 1. - password should be larger than 8 chars
    //    OK -> 2. - password should not be null
    //    3. - password should have one uppercase letter at least
    //    4. - password should have one lowercase letter at least
    //    5. - password should have one number at least

    public static bool Validate(string password)
    {
        if (password == null)
            return false;

        if (password.Length < 8)
            return false;

        if (!password.Any(Char.IsLower))
            return false;

        if (!password.Any(Char.IsUpper))
            return false;

        if (!password.Any(Char.IsDigit))
            return false;

        return true;
    }
}
