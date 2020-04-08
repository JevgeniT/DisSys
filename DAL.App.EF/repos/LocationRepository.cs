using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Public.DTO;

namespace DAL.App.EF.repos
{
    public class LocationRepository : EFBaseRepository<Location,AppDbContext>,  ILocationRepository
    {
        public LocationRepository(AppDbContext  dbContext) : base(dbContext)
        {
        }
        
        //
        // public IEnumerable<Location> All()
        // {
        //     return   RepoDbSet.ToList();
        // }
        //
        // public override async Task<IEnumerable<Location>> AllAsync()
        // {
        //     return await RepoDbSet.ToListAsync();
        // }
        //
        // public override Location Find(params object[] id)
        // {
        //     return RepoDbSet.Find(id);
        // }
        //
        // public override async Task<Location> FindAsync(params object[] id)
        // {
        //     return await RepoDbSet.FindAsync(id);
        //
        // }
        //
        // public override Location Add(Location entity)
        // {
        //     return RepoDbSet.Add(entity).Entity;
        // }
        //
        // public override Location Update(Location entity)
        // {
        //     return RepoDbSet.Update(entity).Entity;
        //
        // }
        //
        // public override Location Remove(Location entity)
        // {
        //     return RepoDbSet.Remove(entity).Entity;
        // }
        //
        // public override Location Remove(params object[] id)
        // {
        //     return RepoDbSet.Remove(Find(id)).Entity;
        // }
        //
        // public override int SaveChanges()
        // {
        //     throw new System.NotImplementedException();
        // }
        //
        // public override  Task<int> SaveChangesAsync()
        // {
        //     throw new System.NotImplementedException();
        // }
        public async Task<IEnumerable<Location>> AllAsync(int? userId = null)
        {
            if (userId == null)
            {
                // base is not actually needed, using it for clarity
            }
            return await base.AllAsync();
            // return await RepoDbSet.Where(o => o.AppUserId == userId).ToListAsync();
        }
        
        public async Task<Location> FirstOrDefaultAsync(int id, int? userId = null)
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
        
        public async Task<IEnumerable<LocationDTO>> DTOAllAsync(int? userId = null)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<LocationDTO> DTOFirstOrDefaultAsync(int id, int? userId = null)
        {
            throw new System.NotImplementedException();
        }
    }
}