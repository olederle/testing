using UnitTests.Apples;

namespace UnitTests.Tests.Apples;

// Should be tested with these apples:
// * Fuji, Red
// * Gala, Red
// * Granny Smith, Green
// * Golden Delicious, Yellow
// * Honeycrisp, Red
// * Pink Lady, Pink
// * McIntosh, Red
// * Braeburn, Red
// * Jonagold, Yellow
// * Cortland, Red
//
// Important: We do not want to have them static, but they should be used in a random order.

public class AppleServiceTests
{
    private readonly AppleService appleService;

    public AppleServiceTests()
    {
        // FIXME
        appleService = new AppleService(null!);
    }

    [Fact]
    public void GetAllApples_ReturnsAllApples()
    {
        // Act
        var result = appleService.GetAllApples();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Count);
    }

    [Theory]
    [InlineData("Red", 6)]
    [InlineData("Green", 1)]
    [InlineData("Yellow", 2)]
    [InlineData("Pink", 1)]
    public void GetApplesByColor_ReturnsCorrectApples(string color, int expectedCount)
    {
        // Act
        var result = appleService.GetApplesByColor(color);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCount, result.Count);
        Assert.All(result, a => Assert.Equal(color, a.Color, true));
    }

    [Theory]
    [InlineData("Fuji")]
    [InlineData("Granny Smith")]
    [InlineData("Golden Delicious")]
    public void GetAppleByName_ReturnsCorrectApple(string name)
    {
        // Act
        var result = appleService.GetAppleByName(name);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(name, result.Name, true);
    }

    [Fact]
    public void GetAppleByName_ReturnsNullForNonExistentApple()
    {
        // Act
        var result = appleService.GetAppleByName("NonExistentApple");

        // Assert
        Assert.Null(result);
    }

    [Theory]
    [InlineData('F', 1)]
    [InlineData('G', 2)]
    [InlineData('H', 1)]
    public void GetApplesStartingWith_ReturnsCorrectApples(char letter, int expectedCount)
    {
        // Act
        var result = appleService.GetApplesStartingWith(letter);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCount, result.Count);
        Assert.All(result, a => Assert.StartsWith(letter.ToString(), a.Name));
    }

    [Theory]
    [InlineData("Fuji", 1)]
    [InlineData("Golden", 1)]
    [InlineData("Lady", 1)]
    public void GetApplesContaining_ReturnsCorrectApples(string substring, int expectedCount)
    {
        // Act
        var result = appleService.GetApplesContaining(substring);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedCount, result.Count);
        Assert.All(result, a => Assert.Contains(substring, a.Name));
    }

    [Fact]
    public void GetApplesSortedByName_ReturnsSortedApples()
    {
        // Act
        var result = appleService.GetApplesSortedByName();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Count);
        for (int i = 1; i < result.Count; i++)
        {
            Assert.True(string.Compare(result[i - 1].Name, result[i].Name, true) <= 0);
        }
    }

    [Fact]
    public void GetApplesSortedByColor_ReturnsSortedApples()
    {
        // Act
        var result = appleService.GetApplesSortedByColor();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Count);
        for (int i = 1; i < result.Count; i++)
        {
            Assert.True(string.Compare(result[i - 1].Color, result[i].Color, true) <= 0);
        }
    }

    [Fact]
    public void GetAppleCountByColor_ReturnsCorrectCounts()
    {
        // Act
        var result = appleService.GetAppleCountByColor();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(4, result.Count);
        Assert.Equal(6, result["Red"]);
        Assert.Equal(1, result["Green"]);
        Assert.Equal(2, result["Yellow"]);
        Assert.Equal(1, result["Pink"]);
    }

    [Fact]
    public void GetAppleCountByName_ReturnsCorrectCounts()
    {
        // Act
        var result = appleService.GetAppleCountByName();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Count);
        Assert.All(result, kvp => Assert.Equal(1, kvp.Value));
    }

    [Fact]
    public void GetApplesWithUniqueColors_ReturnsCorrectApples()
    {
        // Act
        var result = appleService.GetApplesWithUniqueColors();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
        Assert.Contains(result, a => a.Color == "Green");
        Assert.Contains(result, a => a.Color == "Pink");
    }

    [Fact]
    public void GetApplesWithDuplicateColors_ReturnsCorrectApples()
    {
        // Act
        var result = appleService.GetApplesWithDuplicateColors();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(7, result.Count);
        // FIXME
        // Assert.All(result, a => Assert.Equal("Red", a.Color) || Assert.Equal("Yellow", a.Color));
    }
}
