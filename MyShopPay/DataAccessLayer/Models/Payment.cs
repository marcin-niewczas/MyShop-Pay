using MyShopPay.ValueObjects;

namespace MyShopPay.DataAccessLayer.Models;

internal sealed class Payment : BaseEntity
{
    public decimal Price { get; private set; }
    public Uri ContinueUri { get; private set; }
    public PaymentStatus Status { get; private set; } = PaymentStatus.WaitingForPayment;

    public Payment(decimal price, Uri continueUri) : base()
    {
        if (price <= 0)
        {
            throw new ArgumentException(price.ToString(), nameof(price));
        }

        ArgumentNullException.ThrowIfNull(nameof(continueUri));

        Price = price;
        ContinueUri = continueUri;
    }

    public void UpdateStatus(PaymentStatus status)
    {
        Status = status;
    }
}
