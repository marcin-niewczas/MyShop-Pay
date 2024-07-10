using MyShopPay.DataAccessLayer.Models;
using MyShopPay.DataAccessLayer.Repositories.Interfaces;
using MyShopPay.Dtos;
using MyShopPay.Exceptions;
using MyShopPay.ValueObjects;

namespace MyShopPay.Services;

internal sealed class PaymentService(
    IGenericRepository<Payment> repository
    ) : IPaymentService
{
    public async Task<PaymentDto> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
        )
    {
        var entity = await repository.GetByIdAsync(
            id: id,
            cancellationToken: cancellationToken
            ) ?? throw new NotFoundException();

        return new(
            entity.Id,
            entity.Price,
            entity.ContinueUri,
            entity.Status
            );
    }

    public async Task<Guid> CreateAsync(
        CreatePaymentDto dto,
        CancellationToken cancellationToken = default
        )
    {
        var entity = new Payment(dto.Price, dto.ContinueUri);

        await repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }

    public async Task<Guid> UpdateAsync(
        Guid id,
        CancellationToken cancellationToken = default
        )
    {
        var entity = await repository.GetByIdAsync(
            id: id,
            cancellationToken: cancellationToken
            ) ?? throw new NotFoundException();

        entity.UpdateStatus(PaymentStatus.Paid);

        await repository.UpdateAsync(entity, cancellationToken);

        return entity.Id;
    }
}
