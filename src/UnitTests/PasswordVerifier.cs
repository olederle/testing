namespace UnitTests;

public enum PasswordValidationRule
{
    Length,
    NotNull,
    Uppercase,
    Lowercase,
    Number,
}

public readonly struct PasswordValidationResult
{
    public bool IsValid { get; }

    public PasswordValidationRule ValidationRule { get; }

    public string Message { get; }

    public PasswordValidationResult(PasswordValidationRule validationRule, string message)
    {
        IsValid = false;
        ValidationRule = validationRule;
        Message = message;
    }

    public PasswordValidationResult(PasswordValidationRule validationRule)
    {
        IsValid = true;
        ValidationRule = validationRule;
        Message = string.Empty;
    }

    public void ThrowIfInvalid()
    {
        if (IsValid == false)
        {
            throw new PasswordValidationException([this]);
        }
    }
}

public class PasswordValidationException(IReadOnlyList<PasswordValidationResult> validationResults)
    : Exception(string.Join(", ", validationResults.Select(v => v.Message)))
{
    public IReadOnlyList<PasswordValidationResult> ValidationResults = validationResults;
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
            new PasswordValidationResult(
                PasswordValidationRule.NotNull,
                "password should not be null"
            ).ThrowIfInvalid();
            return;
        }

        if (!password.Any(Char.IsLower))
        {
            new PasswordValidationResult(
                PasswordValidationRule.Lowercase,
                "password should have one lowercase letter at least"
            ).ThrowIfInvalid();
            return;
        }

        List<PasswordValidationResult> results = [];

        if (password.Length < 8)
        {
            results.Add(
                new PasswordValidationResult(
                    PasswordValidationRule.Length,
                    "password should be larger than 8 chars"
                )
            );
        }

        if (!password.Any(Char.IsUpper))
        {
            results.Add(
                new PasswordValidationResult(
                    PasswordValidationRule.Uppercase,
                    "password should have one uppercase letter at least"
                )
            );
        }

        if (!password.Any(Char.IsDigit))
        {
            results.Add(
                new PasswordValidationResult(
                    PasswordValidationRule.Number,
                    "password should have one number at least"
                )
            );
        }

        if (results.Count > 1)
        {
            throw new PasswordValidationException(results);
        }
    }
}
