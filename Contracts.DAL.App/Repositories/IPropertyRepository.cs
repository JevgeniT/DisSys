using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IPropertyRepository : IBaseRepository<Property>
 {
  Task<IEnumerable<Property>> AllAsync(Guid? userId = null);
  Task<Property> FirstOrDefaultAsync(Guid id, Guid? userId = null);

  Task<bool> ExistsAsync(Guid id, Guid? userId = null);
  Task DeleteAsync(Guid id, Guid? userId = null);
        
  // DTO methods
  Task<IEnumerable<PropertyDTO>> DTOAllAsync(Guid? userId = null);
  Task<PropertyDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);

  Task<Location> PropertyLocations();

 }
}