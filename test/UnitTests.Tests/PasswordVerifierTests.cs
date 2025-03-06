namespace UnitTests.Tests;

public class PasswordVerifierTests
{
    [
        Theory,
        InlineData("aB7a", false), // invalid class
        InlineData("aB7abcdefgh", true), // valid class
        InlineData("aB7jklu", false), // upper boundary invalid
        InlineData("aB7jklua", true) // lower boundary valid
    ]
    public void ValidatePasswordByLength(string password, bool isValid)
    {
        // act
        bool isValidPassword = PasswordVerifier.Validate(password);

        // assert
        Assert.Equal(isValid, isValidPassword);
    }

    [Fact]
    public void ValidatePasswortWithoutLowerLetter()
    {
        bool isValidPassword = PasswordVerifier.Validate("123456789A");
        Assert.False(isValidPassword);
    }

    [Fact]
    public void ValidatePasswortWithoutUperLetter()
    {
        bool isValidPassword = PasswordVerifier.Validate("123456789a");
        Assert.False(isValidPassword);
    }

    [Fact]
    public void ValidatePasswortWithoutNumber()
    {
        bool isValidPassword = PasswordVerifier.Validate("abcdefghijkLM");
        Assert.False(isValidPassword);
    }
}
