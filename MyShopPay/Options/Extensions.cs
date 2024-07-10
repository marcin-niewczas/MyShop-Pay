namespace MyShopPay.Options;

internal static class Extensions
{
    public static IServiceCollection AddAppSettingsOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DbOptions>(configuration.GetRequiredSection(DbOptions.Section));
        services.Configure<AuthOptions>(configuration.GetRequiredSection(AuthOptions.Section));

        return services;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
}
