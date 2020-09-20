using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ReservationRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser,Reservation, DAL.App.DTO.Reservation>,  IReservationRepository
    {
        public ReservationRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Reservation, DAL.App.DTO.Reservation>())
        {
        }

        public async Task<IEnumerable<DAL.App.DTO.Reservation>> AllAsync(Guid? userId = null, Guid? propertyId = null)
        {
            return (await RepoDbContext.Reservations
                   .Include(reservation => reservation.AppUser)
                   .Include(reservation => reservation.ReservationRooms)
                   .ThenInclude(rooms =>  rooms.Room)
                   .Where(reservation => reservation.PropertyId == propertyId || reservation.AppUserId == userId)
                   .ToListAsync())
                   .Select(reservation => Mapper.Map(reservation));
        }
        
    }
}