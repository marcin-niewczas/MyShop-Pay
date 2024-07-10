using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyShopPay.DataAccessLayer;
using MyShopPay.DataAccessLayer.Models;
using MyShopPay.Dtos;
using MyShopPay.Exceptions;
using MyShopPay.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyShopPay.Security;

internal sealed class AuthService(
    TimeProvider timeProvider,
    IOptions<AuthOptions> options,
    IPasswordHasher<User> passwordHasher,
    IDbContextFactory<MyShopPayDbContext> dbContextFactory
    ) : IAuthService
{
    private readonly string _issuer = options.Value.Issuer;
    private readonly TimeSpan _expiryAccessToken = options.Value.ExpiryAccessToken ?? TimeSpan.FromHours(1);
    private readonly string _audience = options.Value.Audience;
    private readonly SigningCredentials _signingCredentials = new(
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SigningKey)),
        SecurityAlgorithms.HmacSha256
        );
    private readonly JwtSecurityTokenHandler _jwtSecurityToken = new();

    public async Task<AuthDto> SignInAsync(
        SignInDto signIn,
        CancellationToken cancellationToken = default
        )
    {
        ArgumentNullException.ThrowIfNull(nameof(signIn));

        using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        var user = await dbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Username == signIn.Username, cancellationToken)
            ?? throw new BadRequestException();

        var result = passwordHasher.VerifyHashedPassword(default!, user.SecuredPassword, signIn.Password) == PasswordVerificationResult.Success;

        if (!result)
        {
            throw new BadRequestException();
        }

        var (accessToken, expiryAccessTokenDate) = GenerateToken(user);

        return new()
        {
            AccessToken = accessToken,
            ExpiryAccessTokenDate = expiryAccessTokenDate
        };
    }

    public async Task SignUpAsync(
        SignUpDto signUp,
        CancellationToken cancellationToken = default
        )
    {
        ArgumentNullException.ThrowIfNull(nameof(signUp));

        using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        var user = await dbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Username == signUp.Username, cancellationToken);

        if (user is not null)
        {
            throw new BadRequestException();
        }

        var securedPassword = passwordHasher.HashPassword(default!, signUp.Password);

        user = new User(signUp.Username, securedPassword);

        await dbContext.Users.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private (string Token, DateTime ExpiryAccessTokenDate) GenerateToken(User user)
    {
        ArgumentNullException.ThrowIfNull(nameof(user));

        var dateTimeNow = timeProvider.GetUtcNow().UtcDateTime;

        var claims = GetClaims(user);

        var expiryAccessTokenDate = dateTimeNow.Add(_expiryAccessToken);
        var jwt = new JwtSecurityToken(_issuer, _audience, claims, dateTimeNow, expiryAccessTokenDate, _signingCredentials);


        return (_jwtSecurityToken.WriteToken(jwt), expiryAccessTokenDate);
    }

    private static Claim[] GetClaims(User user)
        => [new(JwtRegisteredClaimNames.NameId, user.Id.ToString())];
}
