namespace MyShopPay.DataAccessLayer.Models.Interfaces;

internal interface IEntity
{
    public Guid Id { get; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
