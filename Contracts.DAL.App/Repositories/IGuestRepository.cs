using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IGuestRepository : IBaseRepository<Guest>
 {
  
  Task<IEnumerable<Guest>> AllAsync(int? userId = null);
  Task<Guest> FirstOrDefaultAsync(int id, int? userId = null);

  Task<bool> ExistsAsync(int id, int? userId = null);
  Task DeleteAsync(int id, int? userId = null);
        
  // DTO methods
  // Task<IEnumerable<GuestDTO>> DTOAllAsync(int? userId = null);
  // Task<GuestDTO> DTOFirstOrDefaultAsync(int id, int? userId = null);
 }
}