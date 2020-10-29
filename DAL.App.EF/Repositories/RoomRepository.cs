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
                var query = RepoDbSet.Include(room => room.RoomFacilities).
                    ThenInclude(rf => rf.Facility)
                    .Where(room => room.PropertyId == propertyId);
                
                return (await  query.ToListAsync()).Select(r=>Mapper.Map(r));
            }
            return await base.AllAsync();
         }
        
    }
}