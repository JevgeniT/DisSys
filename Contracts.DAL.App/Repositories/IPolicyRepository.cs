using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPolicyRepository : IPolicyRepository<Guid, Policy>,
        IBaseRepository<Policy>
    {
    }
    public interface IPolicyRepository<TKey, TDALEntity> : IBaseRepository<TKey,TDALEntity> 
        where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
  
   
        Task<IEnumerable<TDALEntity>> AllAsync(Guid? propertyId = null);
        Task<TDALEntity> FirstOrDefaultAsync(Guid id, Guid? propertyId = null);

    }
    
  
}