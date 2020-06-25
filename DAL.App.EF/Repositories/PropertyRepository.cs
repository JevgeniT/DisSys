using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Public.DTO;

namespace DAL.App.EF.Repositories
{
    public class PropertyRepository :
        EFBaseRepository<AppDbContext, Property, DAL.App.DTO.Property>,  IPropertyRepository
    {
        public PropertyRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<Property, DAL.App.DTO.Property>())
        {
        }     

        public async Task<IEnumerable<DAL.App.DTO.Property>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync();
            }
            return (await RepoDbSet.Where(o => o.AppUserId == userId)
                .Include(property => property.PropertyRooms)
                .ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));

        }
 
        public  async Task<IEnumerable<DAL.App.DTO.Property>> FindAsync(SearchDTO? param)
        {
            return (await RepoDbSet
                .Where(o => o.Country!.Contains(param!.Input) 
                            || o.Name!.Contains(param.Input))
                .Include(p=>p.PropertyRooms)
                .ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DAL.App.DTO.Property> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsNoTracking();
            
            if (userId != null)
            {
                query = query.Where(a => a.Id == userId);
            }
            var a = (await query.FirstOrDefaultAsync());
            a.PropertyRooms = (await RepoDbContext.Rooms.Where(room => room.PropertyId == id)
                .Include(room => room.RoomFacilities).AsNoTracking()
                .ToListAsync());
            return Mapper.Map(a);
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
            var property = await FirstOrDefaultAsync(id, userId);
            base.Remove(property);
        }
        
        
    }
}