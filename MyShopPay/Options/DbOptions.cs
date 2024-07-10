namespace MyShopPay.Options;

internal sealed record DbOptions
{
    public const string Section = nameof(DbOptions);
    public required string ConnectionString { get; init; }
}
