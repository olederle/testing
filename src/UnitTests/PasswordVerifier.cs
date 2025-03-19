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
            throw new PasswordValidationException(this);
        }
    }
}

public class PasswordValidationException(PasswordValidationResult validationResult)
    : Exception(validationResult.Message)
{
    public PasswordValidationResult ValidationResult = validationResult;
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

        if (password.Length < 8)
        {
            new PasswordValidationResult(
                PasswordValidationRule.Length,
                "password should be larger than 8 chars"
            ).ThrowIfInvalid();
        }

        if (!password.Any(Char.IsLower))
        {
            new PasswordValidationResult(
                PasswordValidationRule.Lowercase,
                "password should have one lowercase letter at least"
            ).ThrowIfInvalid();
        }

        if (!password.Any(Char.IsUpper))
        {
            new PasswordValidationResult(
                PasswordValidationRule.Uppercase,
                "password should have one uppercase letter at least"
            ).ThrowIfInvalid();
        }

        if (!password.Any(Char.IsDigit))
        {
            new PasswordValidationResult(
                PasswordValidationRule.Number,
                "password should have one number at least"
            ).ThrowIfInvalid();
        }
    }
}
