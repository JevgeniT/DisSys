using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IPolicyRepository : IBaseRepository<Policy>
 {
  Task<IEnumerable<Policy>> AllAsync(int? userId = null);
  Task<Policy> FirstOrDefaultAsync(int id, int? userId = null);

  Task<bool> ExistsAsync(int id, int? userId = null);
  Task DeleteAsync(int id, int? userId = null);
        
  // DTO methods
  // Task<IEnumerable<PolicyDTO>> DTOAllAsync(int? userId = null);
  // Task<PolicyDTO> DTOFirstOrDefaultAsync(int id, int? userId = null);
 }
}