using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
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
        
        public async Task<IEnumerable< DAL.App.DTO.Availability>> AllAsync(Guid? roomId = null)
        {
            return (await RepoDbContext.Availabilities.Include(availability => availability.Room)
                .Include(availability => availability.AvailabilityPolicies)
                .ThenInclude(policies => policies.Policy)
                .Where(availability => availability.RoomId == roomId)
                .ToListAsync()).Select(a => Mapper.Map(a));
        }
        
        public async Task< DAL.App.DTO.Availability> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }
        
        public async Task<IEnumerable< DAL.App.DTO.Availability>> FindAvailableDates(DateTime from, DateTime to, Guid? propertyId = null)
        {
            var dates = $"'{from}' and '{to}'";
            
            var query =   RepoDbSet.FromSqlRaw("select * from Availabilities where [FROM] between " + dates + " or [To] between " + dates)
                .Include(availability => availability.Room)
                // .Include(availability => availability.Policy)
                .Where(availability => availability.Room.PropertyId == propertyId);
            
            query.AsNoTracking();
            
            return  (query.Where(a=> !a.IsUsed).AsNoTracking().Select(e => Mapper.Map(e)));
        }
        

        public async Task<bool> ExistsAsync(Guid id )
        {
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var availability = await FirstOrDefaultAsync(id);
            base.Remove(availability);
        }

    }
}