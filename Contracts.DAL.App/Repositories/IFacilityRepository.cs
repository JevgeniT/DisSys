using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IFacilityRepository : IBaseRepository<Facility>
 {
  Task<IEnumerable<Facility>> AllAsync(int? userId = null);
  Task<Facility> FirstOrDefaultAsync(int id, int? userId = null);

  Task<bool> ExistsAsync(int id, int? userId = null);
  Task DeleteAsync(int id, int? userId = null);
        
  // DTO methods
  // Task<IEnumerable<FacilityDTO>> DTOAllAsync(int? userId = null);
  // Task<FacilityDTO> DTOFirstOrDefaultAsync(int id, int? userId = null);
 }
}