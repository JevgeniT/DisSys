using System;
using System.Collections.Generic;
 using System.Threading.Tasks;

namespace Contracts.DAL.Base.Repositories
{
    public interface IBaseRepository<TDALEntity> : IBaseRepository<Guid, TDALEntity>
        where TDALEntity : class, IDomainBaseEntity<Guid>, new() 
    {
    }

    public interface IBaseRepository<TKey, TDALEntity>
        where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
        TDALEntity Add(TDALEntity entity);
        Task<IEnumerable<TDALEntity>> AllAsync(object? userId = null);
        Task<TDALEntity> FirstOrDefaultAsync(TKey id, object? userId = null);
        Task<TDALEntity>  UpdateAsync(TDALEntity entity, object? userId = null);
        Task<TDALEntity>  RemoveAsync(TDALEntity entity, object? userId = null);
        Task <TDALEntity> RemoveAsync(TKey id, object? userId = null);
        Task<bool> ExistsAsync(TKey id, object? userId = null);
    }
}