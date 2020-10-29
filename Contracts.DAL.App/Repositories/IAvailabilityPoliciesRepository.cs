using System;

using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IAvailabilityPoliciesRepository : IAvailabilityPoliciesRepository<Guid, AvailabilityPolicies>,
        IBaseRepository<AvailabilityPolicies>
    {
    }
    
    public interface IAvailabilityPoliciesRepository<TKey, TDALEntity> 
        : IBaseRepository<TKey,TDALEntity> 
        where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
    }
}