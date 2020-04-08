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
       Task<IEnumerable<Location>> AllAsync(int? userId = null);
       Task<Location> FirstOrDefaultAsync(int id, int? userId = null);

       Task<bool> ExistsAsync(int id, int? userId = null);
       Task DeleteAsync(int id, int? userId = null);
             
       // DTO methods
       Task<IEnumerable<LocationDTO>> DTOAllAsync(int? userId = null);
       Task<LocationDTO> DTOFirstOrDefaultAsync(int id, int? userId = null);
       
       
       // Task<IEnumerable<Property>> 
 }
}