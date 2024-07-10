namespace MyShopPay.Services;

internal static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IPaymentService, PaymentService>();

        return services;
    }
}
