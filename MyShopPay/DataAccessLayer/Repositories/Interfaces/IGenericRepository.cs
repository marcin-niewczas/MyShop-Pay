using MyShopPay.DataAccessLayer.Models.Interfaces;

namespace MyShopPay.DataAccessLayer.Repositories.Interfaces;

internal interface IGenericRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(Guid id, bool withTracking = false, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
