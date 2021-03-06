using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IReviewRepository : IReviewRepository<Guid, Review>,
        IBaseRepository<Review>
    {
    }
    public interface IReviewRepository<TKey, TDALEntity> : IBaseRepository<TKey,TDALEntity> 
        where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TDALEntity>> PropertyReviews(Guid? propertyId);
        Task<bool> ExistsAsync(Guid reservationId, object? userId = null);
    }
    
  
}