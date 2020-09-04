using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{
    public interface IAvailabilityPoliciesRepository : IAvailabilityPoliciesRepository<Guid, AvailabilityPolicies>,
        IBaseRepository<AvailabilityPolicies>
    {
    }
    
    public interface IAvailabilityPoliciesRepository <TKey, TDALEntity> : IBaseRepository<TKey,TDALEntity> 
        where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
  
        Task<IEnumerable<TDALEntity>> AllAsync(Guid? availabilityId = null);
        Task<TDALEntity> FirstOrDefaultAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        
    }
}