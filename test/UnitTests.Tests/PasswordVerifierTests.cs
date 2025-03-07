namespace UnitTests.Tests;

public class PasswordVerifierTests
{
    [
        Theory,
        InlineData("aB7abcdefgh"), // valid class
        InlineData("aB7jklua") // lower boundary valid
    ]
    public void ValidatePasswordByLength(string password)
    {
        // act
        PasswordVerifier.Validate(password);
    }

    [
        Theory,
        InlineData("aBaa"), // invalid class
        InlineData("aBajklu"), // upper boundary invalid
    ]
    public void DoesThrowForInvalidLength(string password)
    {
        PasswordVerificationException ex = Assert.Throws<PasswordVerificationException>(() =>
        {
            PasswordVerifier.Validate(password);
        });

        Assert.Equal(2, ex.VerificationResults.Count);
        Assert.Contains(
            PasswordVerificationRule.Length,
            ex.VerificationResults.Select(v => v.Rule)
        );
        Assert.Contains(PasswordVerificationRule.Digit, ex.VerificationResults.Select(v => v.Rule));
    }

    [Fact]
    public void ValidatePasswortWithoutLowerLetter()
    {
        PasswordVerificationException ex = Assert.Throws<PasswordVerificationException>(() =>
        {
            PasswordVerifier.Validate("123456A");
        });

        Assert.Equal(2, ex.VerificationResults.Count);
        Assert.Contains(
            PasswordVerificationRule.Length,
            ex.VerificationResults.Select(v => v.Rule)
        );
        Assert.Contains(PasswordVerificationRule.Lower, ex.VerificationResults.Select(v => v.Rule));
    }

    [Fact]
    public void ValidatePasswortWithoutUperLetter()
    {
        PasswordVerificationException ex = Assert.Throws<PasswordVerificationException>(() =>
        {
            PasswordVerifier.Validate("123456a");
        });

        Assert.Equal(2, ex.VerificationResults.Count);
        Assert.Contains(
            PasswordVerificationRule.Length,
            ex.VerificationResults.Select(v => v.Rule)
        );
        Assert.Contains(PasswordVerificationRule.Upper, ex.VerificationResults.Select(v => v.Rule));
    }

    [Fact]
    public void ValidatePasswortWithoutNumber()
    {
        PasswordVerificationException ex = Assert.Throws<PasswordVerificationException>(() =>
        {
            PasswordVerifier.Validate("abcdefM");
        });

        Assert.Equal(2, ex.VerificationResults.Count);
        Assert.Contains(
            PasswordVerificationRule.Length,
            ex.VerificationResults.Select(v => v.Rule)
        );
        Assert.Contains(PasswordVerificationRule.Digit, ex.VerificationResults.Select(v => v.Rule));
    }
}
