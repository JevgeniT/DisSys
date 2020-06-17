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
            
            return (await RepoDbSet.Where(o => o.AppUserId == userId).Include(property => property.PropertyRooms)
                .ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));

 
        }
 
        public  async Task<IEnumerable<DAL.App.DTO.Property>> FindAsync(SearchDTO? param)
        {
            
            return (await RepoDbSet
                
                .Where(o => o.Country!.Contains(param!.Input) 
                            || o.PropertyName!.Contains(param.Input)).Include(p=>p.PropertyRooms)
                            
                .ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
            
            // return (await RepoDbSet.Include(property => property.PropertyRooms)
            //     .ThenInclude(room => room.Availabilities)
            //     .Where(o => o.PropertyLocation.Contains(param.Input) 
            //                 || o.PropertyName.Contains(param.Input)
            //                 && o.PropertyRooms.Any(room => room.RoomCapacity>=param.Adults
            //                                                && room.Availabilities.Any(a => a.From >= param.From)))
            //     .ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DAL.App.DTO.Property> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
  
            if (userId != null)
            {
                query = query.Where(a => a.Id == userId);
            }
            return Mapper.Map(await query.FirstOrDefaultAsync());
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
            var owner = await FirstOrDefaultAsync(id, userId);
            base.Remove(owner);
        }
        
        
        
        public async Task<IEnumerable<PropertyDTO>> DTOAllAsync(Guid? userId = null)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<PropertyDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            throw new System.NotImplementedException();
        }
        
    }
}