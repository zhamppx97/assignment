using NLog;
using System.Text;
using System.Text.Json;

namespace api.Middleware;

public class RequestResponseLogHandling
{
    private readonly RequestDelegate _next;
    private readonly StringBuilder _log = new();

    public RequestResponseLogHandling(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();
        await FormatRequest(context.Request, context);
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;
        await _next(context);
        await FormatResponse(context.Response, context);
        LoggerAsync(_log);
        await responseBody.CopyToAsync(originalBodyStream);
    }

    private async Task FormatRequest(HttpRequest request, HttpContext context)
    {
        var header = JsonSerializer.Serialize(context.Request.Headers);
        using var reader = new StreamReader(request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
        var requestContentBody = await reader.ReadToEndAsync();
        request.Body.Position = 0;
        _log.AppendLine($"RequestHeader: {header}, ");
        _log.AppendLine($"Path: {request.Scheme}://{request.Host}{request.Path}{request.QueryString}, ");
        _log.AppendLine($"ContentBody: {requestContentBody}");
    }

    private async Task FormatResponse(HttpResponse response, HttpContext context)
    {
        var header = JsonSerializer.Serialize(context.Response.Headers);
        response.Body.Seek(0, SeekOrigin.Begin);
        string responseContentBody = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);
        _log.AppendLine($"ResponseHeader: {header}, ");
        _log.AppendLine($"StatusCode: {response.StatusCode}, ");
        _log.AppendLine($"ContentBody: {responseContentBody}, ");
    }

    private void LoggerAsync(StringBuilder log)
    {
        Logger logger = LogManager.GetLogger("APIRequestResponse");
        MappedDiagnosticsContext.Set("API", log);
        logger.Log(NLog.LogLevel.Trace, "APIRequestResponseLogger");
    }
}
