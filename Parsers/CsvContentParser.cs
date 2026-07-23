using System.Globalization;
using CsvHelper;
using ParserApi.Models;
using ParserApi.Models.Responses;

namespace ParserApi.Parsers;

public class CsvContentParser : IContentParser
{
    public ContentType Type => ContentType.CSV;

    public ParseContentResponse Parse(string decodedContent)
    {
        try
        {
            using var reader = new StringReader(decodedContent);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<dynamic>().ToList();

            return new ParseContentResponse
            {
                Success = true,
                ProcessedCount = records.Count,
                Data = records
            };

        }
        catch (Exception ex)
        {
            return new ParseContentResponse
            {
                Success = false,
                ProcessedCount = 0,
                Data = null,
                ErrorMessage = $"Failed to parse CSV content: {ex.Message}"
            };
        }
    }
}