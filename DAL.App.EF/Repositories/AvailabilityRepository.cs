using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.DAL.App;
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
            return (await RepoDbContext.Availabilities.Include(a => a.Room).Where(a => a.Active && a.RoomId  == roomId)
                .ToListAsync()).Select(a => Mapper.Map(a));
        }
        

        public async Task<IEnumerable< DAL.App.DTO.Availability>> FindAvailableDates(DateTime from, DateTime to, Guid propertyId)
        {
            var query = await RepoDbContext.Availabilities.AsNoTracking()
                .Include(a => a.Room)
                .Where(a => a.Active && a.Room!.PropertyId == propertyId 
                            && ((from >= a.From && to<=a.To) || (from>=a.From && to<= a.To)))
                .ToListAsync();
            return query.Select(e => Mapper.Map(e));
 
        }


        public async Task<bool> ExistsAsync(DAL.App.DTO.Availability availability)
        {
            return await RepoDbSet.AnyAsync(HasMatchingActiveDates(availability.From, availability.To, availability.RoomId));
        }

        public async Task<bool> ExistsAsync(DateTime from, DateTime to, List< Guid> roomIds)
        {
            foreach (var id in roomIds)
            {
                if (!await RepoDbSet.AnyAsync(HasMatchingActiveDates(from, to, id)))
                {
                    return false;
                }
            }
            return true;
        }

        private  Expression<Func<Availability, bool>> HasMatchingActiveDates(DateTime from, DateTime to, Guid roomId)
            => a => a.Active && a.RoomId==roomId  && (a.From >= @from && a.From <= to) || (a.To >= @from && a.To <= to);
    }
}