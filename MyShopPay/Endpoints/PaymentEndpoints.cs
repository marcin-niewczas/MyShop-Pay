using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyShopPay.Dtos;
using MyShopPay.Exceptions;
using MyShopPay.Services;

namespace MyShopPay.Endpoints;

internal static class PaymentEndpoints
{
    public static IEndpointRouteBuilder MapPaymentEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("{id:Guid}", GetByIdAsync);
        app.MapPost("", CreateAsync).RequireAuthorization();

        return app;
    }

    private static async Task<Results<Ok<PaymentDto>, NotFound>> GetByIdAsync(
        [FromRoute] Guid id,
        [FromServices] IPaymentService service,
        CancellationToken cancellationToken
        )
    {
        var paymentDto = await service.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException();

        return TypedResults.Ok(paymentDto);
    }

    private static async Task<Ok<CreatedPaymentResponseDto>> CreateAsync(
        [FromBody] CreatePaymentDto dto,
        [FromServices] IPaymentService service,
        HttpContext httpContext,
        CancellationToken cancellationToken
        )
    {
        var id = await service.CreateAsync(dto, cancellationToken);

        var uriBuilder = httpContext.Request.Host.Port switch
        {
            int port => new UriBuilder(httpContext.Request.Scheme, httpContext.Request.Host.Host, port),
            null => new UriBuilder(httpContext.Request.Scheme, httpContext.Request.Host.Host),
        };

        uriBuilder.Path = $"payments/{id}";
        var redirectUri = uriBuilder.Uri;



        return TypedResults.Ok(new CreatedPaymentResponseDto(id, redirectUri));
    }
}
