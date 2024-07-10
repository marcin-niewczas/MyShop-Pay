using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyShopPay.DataAccessLayer.Repositories;
using MyShopPay.DataAccessLayer.Repositories.Interfaces;
using MyShopPay.Options;

namespace MyShopPay.DataAccessLayer;

internal static class Extensions
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        services.AddDbContextFactory<MyShopPayDbContext>((provider, options) =>
        {
            options.UseSqlServer(provider.GetRequiredService<IOptions<DbOptions>>().Value.ConnectionString);
        });

        services.AddHostedService<DbInitializerHostedService>();
        services.AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        return services;
    }
}
