namespace TallerMecanico.Api.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext ctx, RequestDelegate next)
    {
        try
        {
            await next(ctx);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "NOT_FOUND");
            ctx.Response.StatusCode = StatusCodes.Status404NotFound;
            await ctx.Response.WriteAsJsonAsync(new
            {
                success = false,
                message = ex.Message,
                errorCode = "NOT_FOUND"
            });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "BUSINESS_VALIDATION");
            ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
            await ctx.Response.WriteAsJsonAsync(new
            {
                success = false,
                message = ex.Message,
                errorCode = "BUSINESS_VALIDATION"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "UNHANDLED");
            ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await ctx.Response.WriteAsJsonAsync(new
            {
                success = false,
                message = "Error interno",
                errorCode = "UNHANDLED"
            });
        }
    }
}
