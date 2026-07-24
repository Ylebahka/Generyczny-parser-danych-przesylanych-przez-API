using ParserApi.Parsers;

namespace ParserApi.Tests;

public class CsvContentParserTests
{
    [Fact]
    public void Parse_ValidCsvWithHeaders_ReturnsSuccessWithCorrectCount()
    {
        // Arrange
        var parser = new CsvContentParser();
        var csvContent = "name,age\nJan,25\nAnna,30";

        // Act
        var result = parser.Parse(csvContent);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(2, result.ProcessedCount);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public void Parse_EmptyContent_ReturnsZeroProcessedCount()
    {
        // Arrange
        var parser = new CsvContentParser();
        var csvContent = "name,age";

        // Act
        var result = parser.Parse(csvContent);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(0, result.ProcessedCount);
    }

    [Fact]
    public void Parse_InvalidCsv_ReturnsFailureWithErrorMessage()
    {
        // Arrange
        var parser = new CsvContentParser();
        var invalidContent = "\"unclosed quote, no end";

        // Act
        var result = parser.Parse(invalidContent);

        // Assert
        Assert.False(result.Success);
        Assert.NotNull(result.ErrorMessage);
    }
}
