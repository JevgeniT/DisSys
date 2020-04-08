using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IReviewRepository : IBaseRepository<Review>
 {
  Task<IEnumerable<Review>> AllAsync(int? userId = null);
  Task<Review> FirstOrDefaultAsync(int id, int? userId = null);

  Task<bool> ExistsAsync(int id, int? userId = null);
  Task DeleteAsync(int id, int? userId = null);
        
  // DTO methods
  // Task<IEnumerable<ReviewDTO>> DTOAllAsync(int? userId = null);
  // Task<ReviewDTO> DTOFirstOrDefaultAsync(int id, int? userId = null);
 }
}