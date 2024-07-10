namespace MyShopPay.Dtos;

public sealed record CreatedPaymentResponseDto(
    Guid Id,
    Uri RedirectUri
    );
