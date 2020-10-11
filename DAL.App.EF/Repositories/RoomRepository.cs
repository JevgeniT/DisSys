using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Public.DTO;

namespace DAL.App.EF.Repositories
{
    public class  RoomRepository : EFBaseRepository<AppDbContext,Domain.Identity.AppUser,Room, DAL.App.DTO.Room>,  IRoomRepository
    {
        public RoomRepository(AppDbContext dbContext) :base(dbContext, new DALMapper<Room, DAL.App.DTO.Room>())
        {
        }
        
        public async Task<IEnumerable<DAL.App.DTO.Room>> AllAsync(Guid propertyId)
        {
            if (propertyId != null)
            {
                var query = RepoDbSet.Where(room => room.PropertyId == propertyId);
                
               return (await  query.ToListAsync()).Select(r=>Mapper.Map(r));
            }
            return await base.AllAsync();
         }
        
        public async Task<DAL.App.DTO.Room> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsNoTracking();
            if (userId != null)
            {
                query = query.Where(a => a.Id == userId);
            }

            var room = (await query.FirstOrDefaultAsync());
            room.RoomFacilities = (await RepoDbContext.Facilities.Where(facility => facility.RoomId == id).AsNoTracking()
                .ToListAsync());
            // room.RoomAvailabilities = (await RepoDbContext.Availabilities.Where(availability => availability.RoomId == id)
            //     .AsNoTracking().ToListAsync());
            return Mapper.Map(room);
        }
        
        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }

            return await RepoDbSet.AnyAsync(a => a.Id == id && a.Id == userId);
        }
        
        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var room = await FirstOrDefaultAsync(id, userId);
            base.RemoveAsync(room);
        }
       
    }
}