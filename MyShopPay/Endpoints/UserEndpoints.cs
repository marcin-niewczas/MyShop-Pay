using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyShopPay.Dtos;
using MyShopPay.Security;

namespace MyShopPay.Endpoints;

internal static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("sign-in", SignInAsync);
        app.MapPost("sign-up", SignUpAsync);

        return app;
    }

    private static async Task<Results<Ok<AuthDto>, BadRequest>> SignInAsync(
        [FromBody] SignInDto signIn,
        [FromServices] IAuthService service,
        CancellationToken cancellationToken
        )
    {
        var auth = await service.SignInAsync(signIn, cancellationToken);

        return TypedResults.Ok(auth);
    }

    private static async Task<Results<Ok, BadRequest>> SignUpAsync(
        [FromBody] SignUpDto dto,
        [FromServices] IAuthService service,
        CancellationToken cancellationToken
        )
    {
        await service.SignUpAsync(dto, cancellationToken);

        return TypedResults.Ok();
    }
}
