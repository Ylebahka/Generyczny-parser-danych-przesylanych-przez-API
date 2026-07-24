using Microsoft.AspNetCore.Mvc;
using ParserApi.Models.Requests;
using ParserApi.Models.Responses;
using ParserApi.Parsers;

namespace ParserApi.Controllers;

[ApiController]
[Route("api/v1")]
public class ParserController : ControllerBase
{
    private readonly IEnumerable<IContentParser> _parsers;

    public ParserController(IEnumerable<IContentParser> parsers)
    {
        _parsers = parsers;
    }

    [HttpPost]
    [Route("parse-content")]
    public IActionResult ParseContent([FromBody] ParseContentRequest request)
    {
        var parser = _parsers.FirstOrDefault(p => p.Type == request.Type);

        if (string.IsNullOrWhiteSpace(request.Content))
        {
            return BadRequest(new ParseContentResponse { Success = false, ErrorMessage = "Content cannot be empty." });
        }

        if (parser == null)
        {
            return BadRequest(new ParseContentResponse { Success = false, ErrorMessage = $"No parser found for content type: {request.Type}" });
        }

        string decodedContent;
        try
        {
            decodedContent = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(request.Content));
        }
        catch (FormatException)
        {
            return BadRequest(new ParseContentResponse
            {
                Success = false,
                ErrorMessage = "Invalid Base64 string format."
            });
        }
        var result = parser.Parse(decodedContent);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}

