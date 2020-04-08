using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IPriceRepository : IBaseRepository<Price>
 {
  Task<IEnumerable<Price>> AllAsync(int? userId = null);
  Task<Price> FirstOrDefaultAsync(int id, int? userId = null);

  Task<bool> ExistsAsync(int id, int? userId = null);
  Task DeleteAsync(int id, int? userId = null);
        
  // DTO methods
  // Task<IEnumerable<PriceDTO>> DTOAllAsync(int? userId = null);
  // Task<PriceDTO> DTOFirstOrDefaultAsync(int id, int? userId = null);
 }
}