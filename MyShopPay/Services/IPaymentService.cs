using MyShopPay.Dtos;

namespace MyShopPay.Services;

internal interface IPaymentService
{
    Task<Guid> CreateAsync(CreatePaymentDto dto, CancellationToken cancellationToken = default);
    Task<PaymentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Guid> UpdateAsync(Guid id, CancellationToken cancellationToken = default);
}
