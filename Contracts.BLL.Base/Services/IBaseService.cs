using System;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;

namespace Contracts.BLL.Base.Services
{
    public interface IBaseService
    {
        
    }

    // public interface IBaseEntityService<TBLLEntity> : IBaseService, IBaseRepository<Guid, TBLLEntity> 
    //     where TBLLEntity : class, IDomainBaseEntity<Guid>, new()
    // {
    // }
    
    public interface IBaseEntityService<TEntity> : IBaseEntityService<Guid, TEntity>
        where TEntity : class, IDomainBaseEntity<Guid>, new()
    {
    }
    public interface IBaseEntityService< TKey, TEntity> : IBaseService, IBaseRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IDomainBaseEntity<TKey>, new()
    {
        
    }
}