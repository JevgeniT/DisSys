using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IPriceRepository : IBaseRepository<Price>
 {
  Task<IEnumerable<Price>> AllAsync(Guid? userId = null);
  Task<Price> FirstOrDefaultAsync(Guid id, Guid? userId = null);

  Task<bool> ExistsAsync(Guid id, Guid? userId = null);
  Task DeleteAsync(Guid id, Guid? userId = null);
        
  // DTO methods
  // Task<IEnumerable<PriceDTO>> DTOAllAsync(Guid? userId = null);
  // Task<PriceDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
 }
}