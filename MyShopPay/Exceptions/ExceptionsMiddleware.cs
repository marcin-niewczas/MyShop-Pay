namespace MyShopPay.Exceptions;

public sealed class ExceptionsMiddleware(
    IWebHostEnvironment environment
    ) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var (statusCode, error) = exception switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, new Error("Not Found", null)),
                BadRequestException => (StatusCodes.Status400BadRequest, new Error("Bad Request", null)),
                _ => (StatusCodes.Status500InternalServerError,
                environment.IsDevelopment()
                    ? new Error(exception.Source, exception.Message)
                    : new Error("Internal Server Error", "Something went wrong."))
            };

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(error);
        }
    }

    private sealed record Error(string? ErrorTitle, string? ErrorMessage);
}
