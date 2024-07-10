using MyShopPay.DataAccessLayer.Models.Interfaces;

namespace MyShopPay.DataAccessLayer.Models;

internal abstract class BaseEntity : IEntity
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; protected set; }
}
