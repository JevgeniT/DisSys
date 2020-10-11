using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IAvailabilityPoliciesRepository : IAvailabilityPoliciesRepository<Guid, AvailabilityPolicies>
        ,
        IBaseRepository<AvailabilityPolicies>
    {
    }
    
    public interface IAvailabilityPoliciesRepository<TKey, TDALEntity> 
        : IBaseRepository<TKey,TDALEntity> 
        where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
  
        //
        // Task<IEnumerable<TDALEntity>> AllAsync(Guid? availabilityId = null);
        //
        // Task<IEnumerable<TDALEntity>> AddRangeAsync(Guid? roomId = null);
        // Task<TDALEntity> FirstOrDefaultAsync(Guid id);
        // Task<bool> ExistsAsync(Guid id);
        //
        // Task DeleteAsync(Guid id);
        
        
    }
}