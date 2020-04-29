using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IReservationRepository : IBaseRepository<Reservation>
 {
  Task<IEnumerable<Reservation>> AllAsync(Guid? userId = null);
  Task<Reservation> FirstOrDefaultAsync(Guid id, Guid? userId = null);

  Task<bool> ExistsAsync(Guid id, Guid? userId = null);
  Task DeleteAsync(Guid id, Guid? userId = null);
        
  // DTO methods
  // Task<IEnumerable<ReservationDTO>> DTOAllAsync(Guid? userId = null);
  // Task<ReservationDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
  
 }
}