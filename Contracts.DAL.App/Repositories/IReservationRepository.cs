using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IReservationRepository : IBaseRepository<Reservation>
 {
  Task<IEnumerable<Reservation>> AllAsync(int? userId = null);
  Task<Reservation> FirstOrDefaultAsync(int id, int? userId = null);

  Task<bool> ExistsAsync(int id, int? userId = null);
  Task DeleteAsync(int id, int? userId = null);
        
  // DTO methods
  // Task<IEnumerable<ReservationDTO>> DTOAllAsync(int? userId = null);
  // Task<ReservationDTO> DTOFirstOrDefaultAsync(int id, int? userId = null);
  
 }
}