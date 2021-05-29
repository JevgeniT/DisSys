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
using Property = Domain.Property;

namespace DAL.App.EF.Repositories
{
    public class PropertyRepository :
        EFBaseRepository<AppDbContext,Domain.Identity.AppUser, Property, DAL.App.DTO.Property>,  IPropertyRepository
    {
        public PropertyRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<Property, DAL.App.DTO.Property>())
        {
        }     

        public override async Task<IEnumerable<DAL.App.DTO.Property>> AllAsync(object? userId = null)
        {
            var query = PrepareQuery(userId);
            var entities = await query.Include(property => property.PropertyRooms)
                .ThenInclude(room => room.RoomFacilities)
                .ThenInclude(rf=> rf.Facility)
                .ToListAsync();
            return entities.Select(domainEntity => Mapper.Map(domainEntity));
        }


        public async Task<IEnumerable<DAL.App.DTO.Property>> FindAsync(DateTime? from, DateTime? to, string input)
        {
            var query = RepoDbSet.Include(p => p.Reviews);

            if (from is null && to is null)
            {
                return (await query.Where(HasMatchWithInput(input)).ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
            }
            
            return (await query.Include(p=>p.PropertyRooms)
                .ThenInclude(r => r.RoomAvailabilities)
                .Where(HasMatchWithInput(input))
                .Where(p => p.PropertyRooms!.Any(room => room.RoomAvailabilities!
                    .Any(a => (from >= a.From && to<=a.To)
                              || 
                              (from>=a.From && to<= a.To))))
                .ToListAsync())
                .Select(domainEntity => Mapper.Map(domainEntity));
        }
        
        public override async Task<DAL.App.DTO.Property> FirstOrDefaultAsync(Guid id, object? userId = null)
        {
            var query = (await RepoDbSet.AsNoTracking()
                    .Include(p => p.Reviews)
                    .Include(p => p.PropertyRules)
                    .Include(p => p.Extras)
                    .Include(property => property.PropertyRooms)
                    .ThenInclude(room => room.RoomFacilities)
                    .ThenInclude(rf => rf.Facility)
                    .FirstOrDefaultAsync(a => a.Id == id));
            
             return Mapper.Map(query);
        }


        private static Expression<Func<Property, bool>> HasMatchWithInput(string input)
            => x => x.Address!.Contains(input) || x.Name!.Contains(input) || x.Address!.Contains(input);
    }
}