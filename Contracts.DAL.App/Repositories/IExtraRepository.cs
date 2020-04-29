using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IExtraRepository : IBaseRepository<Extra>
 {
  Task<IEnumerable<Extra>> AllAsync(Guid? userId = null);
  Task<Extra> FirstOrDefaultAsync(Guid id, Guid? userId = null);

  Task<bool> ExistsAsync(Guid id, Guid? userId = null);
  Task DeleteAsync(Guid id, Guid? userId = null);
        
  // DTO methods
  // Task<IEnumerable<ExtraDTO>> DTOAllAsync(Guid? userId = null);
  // Task<ExtraDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
 }
 
}