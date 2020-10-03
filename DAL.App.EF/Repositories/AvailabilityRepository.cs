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
        EFBaseRepository<AppDbContext,Domain.Identity.AppUser, Availability,  DAL.App.DTO.Availability>,  IAvailabilityRepository
    {
        public AvailabilityRepository(AppDbContext dbContext) 
            :base(dbContext, new DALMapper<Availability,  DAL.App.DTO.Availability>())
        {
        }
        
        public async Task<IEnumerable< DAL.App.DTO.Availability>> AllAsync(Guid? roomId = null)
        {
           // return (await RepoDbContext.Availabilities.Include(availability => availability.Room)
           //     .Where(availability => availability.RoomId == roomId)
           //      .ToListAsync()).Select(a => Mapper.Map(a));
           throw new NotImplementedException();
        }
    
        
        public async Task<IEnumerable< DAL.App.DTO.Availability>> FindAvailableDates(DateTime from, DateTime to, Guid propertyId)
        {
            // var query = await RepoDbContext.Availabilities.AsNoTracking()
            //     .Include(a => a.Room)
            //     .Where(a => a.Active && a.Room.PropertyId == propertyId &&
            //                 ((a.From >= from && a.From <= to) || (a.To >= from && a.To <= to)))
            //     .ToListAsync();
            // return query.Select(e => Mapper.Map(e));
            throw new NotImplementedException();

        }

        
        public  async Task<bool> ExistsAsync(DateTime from, DateTime to)
        {
            return await RepoDbSet.AnyAsync(a => a.Active &&
                                                 ((a.From >= from && a.From <= to)
                                                  || (a.To >= from && a.To <= to)));
        }

        public  async Task<bool> ExistsAsync(DateTime from, DateTime to, Guid propertyId)
        {
            return await RepoDbSet.AnyAsync(a => a.Active && 
                                                 a.Room.PropertyId == propertyId 
                                                 && ((a.From >= from && a.From <= to)
                                                     || (a.To >= from && a.To <= to)));
        }
    }
}