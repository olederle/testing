namespace UnitTests;

public enum PasswordVerificationRule
{
    NotNull,
    Length,
    Lower,
    Upper,
    Digit,
}

public struct PasswordVerificationResult
{
    public bool IsValid { get; }
    public PasswordVerificationRule Rule { get; }
    public string Message { get; }

    public PasswordVerificationResult(PasswordVerificationRule rule, string message)
    {
        IsValid = false;
        Rule = rule;
        Message = message;
    }

    public PasswordVerificationResult(PasswordVerificationRule rule)
    {
        IsValid = true;
        Rule = rule;
        Message = string.Empty;
    }

    public void ThrowIfInvalid()
    {
        if (IsValid == false)
        {
            throw new PasswordVerificationException([this]);
        }
    }
}

public class PasswordVerificationException : Exception
{
    public List<PasswordVerificationResult> VerificationResults { get; }

    public PasswordVerificationException(List<PasswordVerificationResult> verificationResults)
        : base()
    {
        VerificationResults = verificationResults;
    }
}

public class PasswordVerifier
{
    //    OK -> 1. - password should be larger than 8 chars
    //    OK -> 2. - password should not be null
    //    3. - password should have one uppercase letter at least
    //    4. - password should have one lowercase letter at least
    //    5. - password should have one number at least

    public static void Validate(string password)
    {
        if (password == null)
        {
            new PasswordVerificationResult(
                PasswordVerificationRule.NotNull,
                "password should not be null"
            ).ThrowIfInvalid();
            return;
        }

        List<PasswordVerificationResult> invalids = [];

        if (password.Length < 8)
        {
            invalids.Add(
                new PasswordVerificationResult(
                    PasswordVerificationRule.Length,
                    "password should be larger than 8 chars"
                )
            );
        }

        if (!password.Any(Char.IsLower))
        {
            invalids.Add(
                new PasswordVerificationResult(
                    PasswordVerificationRule.Lower,
                    "password should have one lowercase letter at least"
                )
            );
        }

        if (!password.Any(Char.IsUpper))
        {
            invalids.Add(
                new PasswordVerificationResult(
                    PasswordVerificationRule.Upper,
                    "password should have one uppercase letter at least"
                )
            );
        }

        if (!password.Any(Char.IsDigit))
        {
            invalids.Add(
                new PasswordVerificationResult(
                    PasswordVerificationRule.Digit,
                    "password should have one number at least"
                )
            );
        }

        if (invalids.Count > 1)
        {
            throw new PasswordVerificationException(invalids);
        }

        return;
    }
}
