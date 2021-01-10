using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IAvailabilityRepository : IAvailabilityRepository<Guid, Availability>,
        IBaseRepository<Availability>
    {
    }
    
    
    public interface IAvailabilityRepository<TKey, TDALEntity> : IBaseRepository<TKey,TDALEntity> 
        where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TDALEntity>> AllAsync(Guid? roomId);
        Task<bool> ExistsAsync(TDALEntity entity);
        Task<bool> ExistsAsync(DateTime from, DateTime to, List<TKey> roomIds);
        Task<IEnumerable<TDALEntity>> FindAvailableDates(DateTime from, DateTime to, TKey propertyId);
    }
}