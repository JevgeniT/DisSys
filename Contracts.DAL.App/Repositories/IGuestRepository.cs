using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IGuestRepository : IBaseRepository<Guest>
 {
  
  Task<IEnumerable<Guest>> AllAsync(Guid? userId = null);
  Task<Guest> FirstOrDefaultAsync(Guid id, Guid? userId = null);

  Task<bool> ExistsAsync(Guid id, Guid? userId = null);
  Task DeleteAsync(Guid id, Guid? userId = null);
        
  // DTO methods
  // Task<IEnumerable<GuestDTO>> DTOAllAsync(Guid? userId = null);
  // Task<GuestDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
 }
}