using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyShopPay.DataAccessLayer.Models;
using MyShopPay.Options;
using System.Text;

namespace MyShopPay.Security;

internal static class Extensions
{
    public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        var authOptions = configuration.GetOptions<AuthOptions>(AuthOptions.Section);

        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
         .AddJwtBearer(o =>
         {
             o.Audience = authOptions.Audience;
             o.IncludeErrorDetails = true;
             o.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidIssuer = authOptions.Issuer,
                 ClockSkew = TimeSpan.Zero,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SigningKey))
             };
         });


        services
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IAuthService, AuthService>();

        return services;
    }

    public static WebApplication UseSecurity(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}
