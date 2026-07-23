using ParserApi.Models;
using ParserApi.Models.Responses;

namespace ParserApi.Parsers;

public interface IContentParser
{
    ContentType Type { get; }
    ParseContentResponse Parse(string decodedContent);
}