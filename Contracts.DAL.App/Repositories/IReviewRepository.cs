using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IReviewRepository : IBaseRepository<Review>
 {
  Task<IEnumerable<Review>> AllAsync(Guid? userId = null);
  Task<Review> FirstOrDefaultAsync(Guid id, Guid? userId = null);

  Task<bool> ExistsAsync(Guid id, Guid? userId = null);
  Task DeleteAsync(Guid id, Guid? userId = null);
        
  // DTO methods
  // Task<IEnumerable<ReviewDTO>> DTOAllAsync(Guid? userId = null);
  // Task<ReviewDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
 }
}