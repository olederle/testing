using IntegrationTests;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTestTest;

public class AppleControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public AppleControllerTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetApples()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/apples");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal(
            "application/json; charset=utf-8",
            response.Content.Headers.ContentType.ToString()
        );
    }
}
