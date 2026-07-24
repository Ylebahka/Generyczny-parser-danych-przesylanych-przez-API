using ParserApi.Parsers;

namespace ParserApi.Tests;

public class JsonContentParserTests
{
    [Fact]
    public void Parse_ValidJson_ReturnsSuccessWithCorrectCount()
    {
        // Arrange
        var parser = new JsonContentParser();
        var jsonContent = "[{\"name\": \"Jan\", \"age\": 25}, {\"name\": \"Anna\", \"age\": 30}]";

        // Act
        var result = parser.Parse(jsonContent);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(2, result.ProcessedCount);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public void Parse_EmptyContent_ReturnsZeroProcessedCount()
    {
        // Arrange
        var parser = new JsonContentParser();
        var jsonContent = "[]";

        // Act
        var result = parser.Parse(jsonContent);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(0, result.ProcessedCount);
    }

    [Fact]
    public void Parse_InvalidJson_ReturnsFailureWithErrorMessage()
    {
        // Arrange
        var parser = new JsonContentParser();
        var invalidContent = "[{\"id\": 100,    \"user\": \"alex_dev\",   \"email\": \"alex@example.com\"]";

        // Act
        var result = parser.Parse(invalidContent);

        // Assert
        Assert.False(result.Success);
        Assert.NotNull(result.ErrorMessage);
    }
}
