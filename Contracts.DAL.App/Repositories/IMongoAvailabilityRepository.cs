using System;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    
    
    public interface IMongoAvailabilityRepository 
        : IBaseRepository<Availability>,IMongoAvailabilityRepository<Guid, Availability>
    {
    }
    
    
    public interface IMongoAvailabilityRepository<TKey, TDALEntity> 
        : IBaseRepository<TKey,TDALEntity> 
        where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
        Task CreateNew(Guid propertyId);

        // Task<IEnumerable<TDALEntity>> AllAsync(Guid? roomId = null);
        // Task<bool> ExistsAsync(DateTime from, DateTime to);
        // Task<bool> ExistsAsync(DateTime from, DateTime to, Guid propertyId);
        // Task<IEnumerable<TDALEntity>> FindAvailableDates(DateTime from, DateTime to, Guid propertyId);
    }
}