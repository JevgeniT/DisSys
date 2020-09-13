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
        Task<IEnumerable<TDALEntity>> AllAsync(object? userId = null);
        TDALEntity Add(TDALEntity entity);
        Task<TDALEntity> FirstOrDefaultAsync(TKey id, object? userId = null);
        TDALEntity Update(TDALEntity entity);
        TDALEntity Remove(TDALEntity entity);
        Task <TDALEntity> RemoveAsync(params object[] id);
        Task<bool> ExistsAsync(TKey id, object? userId = null);
    }
}