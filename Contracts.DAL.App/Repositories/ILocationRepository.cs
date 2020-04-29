using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface ILocationRepository : IBaseRepository<Location>
 {
       Task<IEnumerable<Location>> AllAsync(Guid? userId = null);
       Task<Location> FirstOrDefaultAsync(Guid id, Guid? userId = null);

       Task<bool> ExistsAsync(Guid id, Guid? userId = null);
       Task DeleteAsync(Guid id, Guid? userId = null);
             
       // DTO methods
       Task<IEnumerable<LocationDTO>> DTOAllAsync(Guid? userId = null);
       Task<LocationDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
       
       
       // Task<IEnumerable<Property>> 
 }
}