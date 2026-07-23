namespace ParserApi.Models.Responses;

using ParserApi.Models;

public class ParseContentResponse
{
    public bool Success { get; set; }
    public int ProcessedCount { get; set; }
    public object? Data { get; set; }
    public string? ErrorMessage { get; set; }
}