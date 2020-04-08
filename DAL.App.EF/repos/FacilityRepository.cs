
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class FacilityRepository : EFBaseRepository<Facility,AppDbContext>,  IFacilityRepository
    {
        public FacilityRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Facility>> AllAsync(int? userId = null)
        {
            if (userId == null)
            {
                // base is not actually needed, using it for clarity
            }
            return await base.AllAsync();
            // return await RepoDbSet.Where(o => o.AppUserId == userId).ToListAsync();
        }
        
        public async Task<Facility> FirstOrDefaultAsync(int id, int? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.Id == userId);
            }

            return await query.FirstOrDefaultAsync();
        }
        
        public async Task<bool> ExistsAsync(int id, int? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }

            return await RepoDbSet.AnyAsync(a => a.Id == id && a.Id == userId);
        }
        
        public async Task DeleteAsync(int id, int? userId = null)
        {
            var owner = await FirstOrDefaultAsync(id, userId);
            base.Remove(owner);
        }
        
        public async Task<IEnumerable<Facility>> DTOAllAsync(int? userId = null)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<Facility> DTOFirstOrDefaultAsync(int id, int? userId = null)
        {
            throw new System.NotImplementedException();
        }
    }
}