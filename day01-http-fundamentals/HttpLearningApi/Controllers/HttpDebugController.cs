using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HttpLearningApi.Controllers;

[ApiController]
[Route("debug")]
public class HttpDebugController : ControllerBase
{
    [HttpGet]
    public IActionResult InspectRequest()
    {
        var result = new
        {
            Method = Request.Method,
            Path = Request.Path,
            Query = Request.Query.ToDictionary(q => q.Key, q => q.Value.ToString()),
            Headers = Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
            ClientIP = HttpContext.Connection.RemoteIpAddress?.ToString()
        };

        return Ok(result);
    }
    [HttpPost("body")]
    public IActionResult RepostBody([FromBody] JsonElement body)
    {
        var result = new
        {
            receivedBody = body
        };

        return Ok(result);
    }
}
