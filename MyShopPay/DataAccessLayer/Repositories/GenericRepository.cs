using Microsoft.EntityFrameworkCore;
using MyShopPay.DataAccessLayer.Models.Interfaces;
using MyShopPay.DataAccessLayer.Repositories.Interfaces;

namespace MyShopPay.DataAccessLayer.Repositories;

internal sealed class GenericRepository<TEntity>(
    IDbContextFactory<MyShopPayDbContext> dbContextFactory
    ) : IGenericRepository<TEntity> where TEntity : class, IEntity
{
    public async Task<TEntity?> GetByIdAsync(Guid id, bool withTracking = false, CancellationToken cancellationToken = default)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await (withTracking switch
        {
            true => dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken),
            _ => dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
        });
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        var entityEntry = await dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return entityEntry.Entity;
    }


    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        var updatedEntity = dbContext.Update(entity).Entity;
        await dbContext.SaveChangesAsync(cancellationToken);
        return updatedEntity;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}
