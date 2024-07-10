using MyShopPay.DataAccessLayer.Models.Interfaces;

namespace MyShopPay.DataAccessLayer.Models;

internal abstract class BaseEntity : IEntity
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
