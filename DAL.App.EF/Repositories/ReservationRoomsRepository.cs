using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ReservationRoomsRepository:
        EFBaseRepository<AppDbContext, ReservationRooms,  DAL.App.DTO.ReservationRooms>,  IReservationRoomsRepository

    {
        public ReservationRoomsRepository(AppDbContext dbContext) : 
            base(dbContext, new DALMapper<ReservationRooms, DTO.ReservationRooms>())
        {
        }

        public Task<IEnumerable<DTO.ReservationRooms>> AllAsync(Guid? availabilityId = null)
        {
            throw new NotImplementedException();
        }

        public Task<DTO.ReservationRooms> FirstOrDefaultAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var availabilityPolicy = await FirstOrDefaultAsync(id);
            base.Remove(availabilityPolicy);
        }
    }
}