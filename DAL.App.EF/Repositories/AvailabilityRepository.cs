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
    public class AvailabilityRepository : 
        EFBaseRepository<AppDbContext,Availability,  DAL.App.DTO.Availability>,  IAvailabilityRepository
    {
        public AvailabilityRepository(AppDbContext dbContext) 
            :base(dbContext, new DALMapper<Availability,  DAL.App.DTO.Availability>())
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
        
        public async Task<IEnumerable< DAL.App.DTO.Availability>> FindAvailableDates(DateTime from, DateTime to, Guid? PropertyId = null)
        {
            var dates = $"'{from.ToShortDateString()}' and '{to.ToShortDateString()}'";
            
            var query = RepoDbSet.FromSqlRaw("select * from Availabilities where [FROM] between " + dates + " or [To] between " + dates)
                .Include(availability => availability.Room)
                .Include(availability => availability.Policy)
                .Where(availability => availability.Room.PropertyId==PropertyId);
            query.AsNoTracking();
            return (query.Where(a=> !a.IsUsed).AsNoTracking().Select(e => Mapper.Map(e)));
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

    }
}