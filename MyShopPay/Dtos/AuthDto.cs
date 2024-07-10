namespace MyShopPay.Dtos;

internal sealed record AuthDto
{
    public required string AccessToken { get; init; }
    public required DateTime ExpiryAccessTokenDate { get; init; }
}
