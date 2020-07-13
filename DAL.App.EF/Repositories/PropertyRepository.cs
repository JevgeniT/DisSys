using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Public.DTO;
using Property = Domain.Property;

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
                .Include(p=>p.Reviews)
                .ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
            
        }
        
        
        public virtual async Task<IEnumerable<PropertyView>> FindForViewAsync(SearchDTO searchDTO)
        {
            var query = (RepoDbSet
                .Where(o => o.Country!.Contains(searchDTO!.Input)
                            || o.Name!.Contains(searchDTO.Input))
                .Include(p => p.Reviews));
               

           var result = query.Select(a => new PropertyView()
           {
               Id = a.Id,
               Name = a.Name,
               ReviewsCount = a.Reviews.Count(),
               Score = Math.Round(a.Reviews.Average(review => review.Score),1),
               Address = a.Address,
               Country = a.Country,
               Type =  a.Type.ToString(),
               Description = a.Description
           }).ToListAsync();

           return await result;
        }
        
        
        public async Task<DAL.App.DTO.Property> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id)     
                .Include(p => p.Reviews)
                .AsNoTracking();
            
            if (userId != null)
            {
                query = query.Where(a => a.Id == userId);
            }
            
            var a = (await query.FirstOrDefaultAsync());
            a.PropertyRooms = (await RepoDbContext.Rooms
                .Include(room => room.RoomFacilities)
                .AsNoTracking().Where(room => room.PropertyId == id)
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