using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IExtraRepository : IBaseRepository<Extra>
 {
  Task<IEnumerable<Extra>> AllAsync(int? userId = null);
  Task<Extra> FirstOrDefaultAsync(int id, int? userId = null);

  Task<bool> ExistsAsync(int id, int? userId = null);
  Task DeleteAsync(int id, int? userId = null);
        
  // DTO methods
  // Task<IEnumerable<ExtraDTO>> DTOAllAsync(int? userId = null);
  // Task<ExtraDTO> DTOFirstOrDefaultAsync(int id, int? userId = null);
 }
 
}