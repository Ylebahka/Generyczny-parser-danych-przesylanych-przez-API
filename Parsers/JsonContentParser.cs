using System.Text.Json;
using ParserApi.Models;
using ParserApi.Models.Responses;

namespace ParserApi.Parsers;

public class JsonContentParser : IContentParser
{
    public ContentType Type => ContentType.INTERNAL_JSON;
    public ParseContentResponse Parse(string decodedContent)
    {
        try
        {
            using var document = JsonDocument.Parse(decodedContent);
            var root = document.RootElement;

            List<object> records;

            if (root.ValueKind == JsonValueKind.Array)
            {
                records = JsonSerializer.Deserialize<List<object>>(decodedContent)!;
            }
            else
            {
                var singleRecord = JsonSerializer.Deserialize<object>(decodedContent);
                records = singleRecord != null ? new List<object> { singleRecord } : new List<object>();
            }

            return new ParseContentResponse
            {
                Success = true,
                ProcessedCount = records.Count,
                Data = records
            };
        }
        catch (JsonException ex)
        {
            return new ParseContentResponse
            {
                Success = false,
                ProcessedCount = 0,
                ErrorMessage = $"Failed to parse JSON content: {ex.Message}"
            };
        }
    }
}