namespace MyShopPay.ValueObjects;

internal sealed record PaymentStatus
{
    public static IReadOnlyCollection<string> AllowedValues =>
    [
        Paid,
        WaitingForPayment,
        Failed
    ];
    public string Value { get; }

    public const string Paid = nameof(Paid);
    public const string WaitingForPayment = nameof(WaitingForPayment);
    public const string Failed = nameof(Failed);

    public PaymentStatus(string value)
    {
        if (!AllowedValues.Contains(value))
            throw new ArgumentException(value);
        Value = value;
    }

    public static implicit operator string(PaymentStatus value)
        => value.Value;

    public static implicit operator PaymentStatus(string value)
        => new(value);

    public override string ToString()
        => Value;
}
