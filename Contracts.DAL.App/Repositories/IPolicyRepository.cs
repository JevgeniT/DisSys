using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IPolicyRepository : IBaseRepository<Policy>
 {
  Task<IEnumerable<Policy>> AllAsync(Guid? userId = null);
  Task<Policy> FirstOrDefaultAsync(Guid id, Guid? userId = null);

  Task<bool> ExistsAsync(Guid id, Guid? userId = null);
  Task DeleteAsync(Guid id, Guid? userId = null);
        
  // DTO methods
  // Task<IEnumerable<PolicyDTO>> DTOAllAsync(Guid? userId = null);
  // Task<PolicyDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
 }
}