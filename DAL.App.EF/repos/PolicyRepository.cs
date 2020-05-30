
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
    public class PolicyRepository : EFBaseRepository<AppDbContext,Policy,Policy>,  IPolicyRepository
    {
        public PolicyRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Policy, Policy>())
        {
        }

 
        public async Task<IEnumerable<Policy>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                // base is not actually needed, using it for clarity
            }
            return await base.AllAsync();
            // return await RepoDbSet.Where(o => o.AppUserId == userId).ToListAsync();
        }
        
        public async Task<Policy> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.Id == userId);
            }

            return await query.FirstOrDefaultAsync();
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
        
        public async Task<IEnumerable<Policy>> DTOAllAsync(Guid? userId = null)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<Policy> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            throw new System.NotImplementedException();
        }
    }
}