using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IRoomRepository : IBaseRepository<Room>
 {
  Task<IEnumerable<Room>> AllAsync(int? userId = null);
  Task<Room> FirstOrDefaultAsync(int id, int? userId = null);

  Task<bool> ExistsAsync(int id, int? userId = null);
  Task DeleteAsync(int id, int? userId = null);
        
  // DTO methods
  Task<IEnumerable<RoomDTO>> DTOAllAsync(int? userId = null);
  Task<RoomDTO> DTOFirstOrDefaultAsync(int id, int? userId = null);
 }
}