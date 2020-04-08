using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IPropertyRepository : IBaseRepository<Property>
 {
  Task<IEnumerable<Property>> AllAsync(int? userId = null);
  Task<Property> FirstOrDefaultAsync(int id, int? userId = null);

  Task<bool> ExistsAsync(int id, int? userId = null);
  Task DeleteAsync(int id, int? userId = null);
        
  // DTO methods
  Task<IEnumerable<PropertyDTO>> DTOAllAsync(int? userId = null);
  Task<PropertyDTO> DTOFirstOrDefaultAsync(int id, int? userId = null);

  Task<Location> PropertyLocations();

 }
}