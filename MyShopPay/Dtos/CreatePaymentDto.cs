namespace MyShopPay.Dtos;

internal sealed record CreatePaymentDto
{
    public required decimal Price { get; init; }
    public required Uri ContinueUri { get; init; }
}
