namespace UnitTests.Tests;

public class PasswordVerifierTests
{
    [Fact]
    public void DoesNotAcceptNullAsPassword()
    {
        // act
        PasswordValidationException ex = Assert.Throws<PasswordValidationException>(
            () => PasswordVerifier.Validate(null!)
        );

        // assert
        Assert.False(ex.ValidationResult.IsValid);
        Assert.Equal(PasswordValidationRule.NotNull, ex.ValidationResult.ValidationRule);
        Assert.Equal("password should not be null", ex.ValidationResult.Message);
    }

    [
        Theory,
        InlineData("aB7a"), // invalid class
        InlineData("aB7jklu"), // upper boundary invalid
    ]
    public void DoesThrowForPasswordsWithInvalidLength(string password)
    {
        // act
        PasswordValidationException ex = Assert.Throws<PasswordValidationException>(
            () => PasswordVerifier.Validate(password)
        );

        // assert
        Assert.False(ex.ValidationResult.IsValid);
        Assert.Equal(PasswordValidationRule.Length, ex.ValidationResult.ValidationRule);
        Assert.Equal("password should be larger than 8 chars", ex.ValidationResult.Message);
    }

    [
        Theory,
        InlineData("aB7abcdefgh"), // valid class
        InlineData("aB7jklua") // lower boundary valid
    ]
    public void DoesAcceptPasswordsWithCorrectLength(string password)
    {
        // act
        PasswordVerifier.Validate(password);

        // assert
        // nothng to do, because if password is invalid an exception is thrown
    }

    [Fact]
    public void ValidatePasswortWithoutLowerLetter()
    {
        // act
        PasswordValidationException ex = Assert.Throws<PasswordValidationException>(
            () => PasswordVerifier.Validate("123456789A")
        );

        // assert
        Assert.False(ex.ValidationResult.IsValid);
        Assert.Equal(PasswordValidationRule.Lowercase, ex.ValidationResult.ValidationRule);
        Assert.Equal(
            "password should have one lowercase letter at least",
            ex.ValidationResult.Message
        );
    }

    [Fact]
    public void ValidatePasswortWithoutUperLetter()
    {
        // act
        PasswordValidationException ex = Assert.Throws<PasswordValidationException>(
            () => PasswordVerifier.Validate("123456789a")
        );

        // assert
        Assert.False(ex.ValidationResult.IsValid);
        Assert.Equal(PasswordValidationRule.Uppercase, ex.ValidationResult.ValidationRule);
        Assert.Equal(
            "password should have one uppercase letter at least",
            ex.ValidationResult.Message
        );
    }

    [Fact]
    public void ValidatePasswortWithoutNumber()
    {
        // act
        PasswordValidationException ex = Assert.Throws<PasswordValidationException>(
            () => PasswordVerifier.Validate("abcdefghijkLM")
        );

        // assert
        Assert.False(ex.ValidationResult.IsValid);
        Assert.Equal(PasswordValidationRule.Number, ex.ValidationResult.ValidationRule);
        Assert.Equal("password should have one number at least", ex.ValidationResult.Message);
    }
}
