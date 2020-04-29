using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IRoomRepository : IBaseRepository<Room>
 {
  Task<IEnumerable<Room>> AllAsync(Guid? userId = null);
  Task<Room> FirstOrDefaultAsync(Guid id, Guid? userId = null);

  Task<bool> ExistsAsync(Guid id, Guid? userId = null);
  Task DeleteAsync(Guid id, Guid? userId = null);
        
  // DTO methods
  Task<IEnumerable<RoomDTO>> DTOAllAsync(Guid? userId = null);
  Task<RoomDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
 }
}