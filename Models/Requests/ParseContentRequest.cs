namespace ParserApi.Models.Requests;

using ParserApi.Models;

public class ParseContentRequest
{
    public ContentType Type { get; set; }
    public string Content { get; set; } = String.Empty;
}