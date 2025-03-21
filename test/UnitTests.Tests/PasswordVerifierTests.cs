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
        Assert.Single(ex.ValidationResults);
        Assert.False(ex.ValidationResults[0].IsValid);
        Assert.Equal(PasswordValidationRule.NotNull, ex.ValidationResults[0].ValidationRule);
        Assert.Equal("password should not be null", ex.ValidationResults[0].Message);
    }

    [
        Theory,
        InlineData("ABcA"), // invalid class
        InlineData("ABcJKLU"), // upper boundary invalid
    ]
    public void DoesThrowForPasswordsWithInvalidLength(string password)
    {
        // act
        PasswordValidationException ex = Assert.Throws<PasswordValidationException>(
            () => PasswordVerifier.Validate(password)
        );

        // assert
        Assert.Equal(2, ex.ValidationResults.Count);
        Assert.False(ex.ValidationResults[0].IsValid);
        Assert.False(ex.ValidationResults[1].IsValid);
        Assert.Contains(
            PasswordValidationRule.Length,
            ex.ValidationResults.Select(v => v.ValidationRule)
        );
        Assert.Contains(
            "password should be larger than 8 chars",
            ex.ValidationResults.Select(v => v.Message)
        );
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
            () => PasswordVerifier.Validate("123456A")
        );

        // assert
        Assert.Single(ex.ValidationResults);
        Assert.False(ex.ValidationResults[0].IsValid);
        Assert.Contains(
            PasswordValidationRule.Lowercase,
            ex.ValidationResults.Select(v => v.ValidationRule)
        );
        Assert.Contains(
            "password should have one lowercase letter at least",
            ex.ValidationResults.Select(v => v.Message)
        );
    }

    [Fact]
    public void ValidatePasswortWithoutUperLetter()
    {
        // act
        PasswordValidationException ex = Assert.Throws<PasswordValidationException>(
            () => PasswordVerifier.Validate("123456a")
        );

        // assert
        Assert.Equal(2, ex.ValidationResults.Count);
        Assert.False(ex.ValidationResults[0].IsValid);
        Assert.False(ex.ValidationResults[1].IsValid);
        Assert.Contains(
            PasswordValidationRule.Uppercase,
            ex.ValidationResults.Select(v => v.ValidationRule)
        );
        Assert.Contains(
            "password should have one uppercase letter at least",
            ex.ValidationResults.Select(v => v.Message)
        );
    }

    [Fact]
    public void ValidatePasswortWithoutNumber()
    {
        // act
        PasswordValidationException ex = Assert.Throws<PasswordValidationException>(
            () => PasswordVerifier.Validate("abcdeM")
        );

        // assert
        Assert.Equal(2, ex.ValidationResults.Count);
        Assert.False(ex.ValidationResults[0].IsValid);
        Assert.False(ex.ValidationResults[1].IsValid);
        Assert.Contains(
            PasswordValidationRule.Number,
            ex.ValidationResults.Select(v => v.ValidationRule)
        );
        Assert.Contains(
            "password should have one number at least",
            ex.ValidationResults.Select(v => v.Message)
        );
    }
}
