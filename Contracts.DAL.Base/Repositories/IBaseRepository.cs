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
        // crud
        IEnumerable<TDALEntity> All();
        Task<IEnumerable<TDALEntity>> AllAsync();

        TDALEntity Find(params object[] id);
        Task<TDALEntity> FindAsync(params object[] id);
        TDALEntity Add(TDALEntity entity);
        TDALEntity Update(TDALEntity entity);
        TDALEntity Remove(TDALEntity entity);
        TDALEntity Remove(params object[] id);
    }
}