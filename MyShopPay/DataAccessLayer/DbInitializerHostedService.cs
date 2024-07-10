using Microsoft.EntityFrameworkCore;
using MyShopPay.Security;

namespace MyShopPay.DataAccessLayer;

internal sealed class DbInitializerHostedService(
    IServiceProvider serviceProvider
    ) : IHostedService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MyShopPayDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        if (!await dbContext.Users.AnyAsync(cancellationToken))
        {
            var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();

            await authService.SignUpAsync(new("myShop", "myShopPass"), cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
