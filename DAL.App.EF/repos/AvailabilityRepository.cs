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
    public class AvailabilityRepository : 
        EFBaseRepository<AppDbContext,Availability,  DAL.App.DTO.Availability>,  IAvailabilityRepository
    
    {
        
        public AvailabilityRepository(AppDbContext dbContext) 
            :base(dbContext, new BaseDALMapper<Availability,  DAL.App.DTO.Availability>())
        {
        }
        public async Task<IEnumerable< DAL.App.DTO.Availability>> AllAsync(Guid? userId = null)
        {
           
            return await base.AllAsync();
         }
        
        public async Task< DAL.App.DTO.Availability> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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
        
        public async Task<IEnumerable<Availability>> DTOAllAsync(Guid? userId = null)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<Availability> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            throw new System.NotImplementedException();
        }
    }
}