using NLog;

namespace api.Middleware;

public class ExceptionLogHandling
{
    private readonly RequestDelegate _next;

    public ExceptionLogHandling(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var msg = $"IP: {context.Connection.RemoteIpAddress}, " +
            $"Path: {context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}, " +
            $"Headers: {context.Request.Headers}, " +
            $"ErrorMessage: {ex.StackTrace}";
        Logger(msg);
        return context.Response.WriteAsync(msg);
    }

    private void Logger(string msg)
    {
        Logger logger = LogManager.GetLogger("Exception");
        MappedDiagnosticsContext.Set("API", msg);
        logger.Log(NLog.LogLevel.Trace, "ExceptionLogger");
    }
}
