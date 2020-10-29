using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ReservationRepository 
        : EFBaseRepository<AppDbContext, Domain.Identity.AppUser,Reservation, DAL.App.DTO.Reservation>,  IReservationRepository
    {
        public ReservationRepository(AppDbContext dbContext) 
            : base(dbContext, new DALMapper<Reservation, DAL.App.DTO.Reservation>())
        {
        }

        public async Task<IEnumerable<DAL.App.DTO.Reservation>> AllAsync(Guid? userId = null, Guid? propertyId = null)
        {
            return (await RepoDbContext.Reservations 
                   .Include(res=> res.Property)
                   .Include(res => res.AppUser)
                   .Include(res => res.ReservationRooms)
                   .ThenInclude(rooms => rooms.Room)
                   .Where(res => res.PropertyId == propertyId || res.AppUserId == userId)
                   .ToListAsync()).Select(res => Mapper.Map(res));
        }

        public override async Task<DTO.Reservation> FirstOrDefaultAsync(Guid id, object? userId = null)
        {
            var reservation = (await RepoDbContext.Reservations
                .Include(r=> r.Review)
                .Include(r=> r.Property)
                .Include(r => r.AppUser)
                .Include(r => r.ReservationRooms)
                .ThenInclude(rooms => rooms.Room).FirstOrDefaultAsync(r=> r.Id.Equals(id)));
             return Mapper.Map(reservation);
        }
    }
}