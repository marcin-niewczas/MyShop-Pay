using MyShopPay.Dtos;

namespace MyShopPay.Security;

internal interface IAuthService
{
    Task<AuthDto> SignInAsync(SignInDto signIn, CancellationToken cancellationToken = default);
    Task SignUpAsync(SignUpDto signUp, CancellationToken cancellationToken = default);
}
