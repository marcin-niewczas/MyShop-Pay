namespace MyShopPay.Dtos;

internal sealed record PaymentDto(
    Guid Id,
    decimal Price,
    Uri ContinueUri,
    string Status
    );
