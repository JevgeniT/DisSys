using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IFacilityRepository : IBaseRepository<Facility>
 {
  Task<IEnumerable<Facility>> AllAsync(Guid? userId = null);
  Task<Facility> FirstOrDefaultAsync(Guid id, Guid? userId = null);

  Task<bool> ExistsAsync(Guid id, Guid? userId = null);
  Task DeleteAsync(Guid id, Guid? userId = null);
        
  // DTO methods
  // Task<IEnumerable<FacilityDTO>> DTOAllAsync(Guid? userId = null);
  // Task<FacilityDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
 }
}