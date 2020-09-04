using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class FacilityRepository : EFBaseRepository<AppDbContext, Facility, DAL.App.DTO.Facility>,  IFacilityRepository
    {
        public FacilityRepository(AppDbContext dbContext) :  base(dbContext, new BaseDALMapper<Facility, DAL.App.DTO.Facility>())
        {
        }

        public async Task<IEnumerable<DAL.App.DTO.Facility>> AllAsync(Guid? userId = null)
        {
            return await base.AllAsync();
        }
        
        public async Task<DAL.App.DTO.Facility> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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
            var facility = await FirstOrDefaultAsync(id, userId);
            base.Remove(facility);
        }
    }
}