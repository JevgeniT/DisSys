using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class PropertyRoomsRepository : 
        EFBaseRepository<AppDbContext,PropertyRooms,  DAL.App.DTO.PropertyRooms>,  IPropertyRoomsRepository
    
    {
        
        public PropertyRoomsRepository(AppDbContext dbContext) 
            :base(dbContext, new BaseDALMapper<PropertyRooms,  DAL.App.DTO.PropertyRooms>())
        {
        }
        public async Task<IEnumerable< DAL.App.DTO.PropertyRooms>> AllAsync(Guid? userId = null)
        {
           
            return await base.AllAsync();
         }
        
        public async Task< DAL.App.DTO.PropertyRooms> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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
            var availability = await FirstOrDefaultAsync(id, userId);
            base.Remove(availability);
        }
        
        public async Task<IEnumerable<PropertyRooms>> DTOAllAsync(Guid? userId = null)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<PropertyRooms> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            throw new System.NotImplementedException();
        }
    }
}