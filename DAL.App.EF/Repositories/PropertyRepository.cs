using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Public.DTO;
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
            var entities = await query.Include(property => property.PropertyRooms).ToListAsync();
            return entities.Select(domainEntity => Mapper.Map(domainEntity));
        }


        public  async Task<IEnumerable<DAL.App.DTO.Property>> FindAsync(DateTime? from, DateTime? to, string input)
        {
            // if (from == null && to == null )
            // {
                return (await RepoDbSet
                    .Where(o => o.Country!.Contains(input) 
                                || o.Name!.Contains(input)
                                || o.Address.Contains(input))
                    .Include(p=>p.Reviews)
                    .ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
            // }
            //
            // return (await RepoDbSet
            //         .Include(p=>p.Reviews)
            //         // .Include(p=>p.PropertyRooms)
            //         // .ThenInclude(r => r.RoomAvailabilities)
            //         .Where(o => o.Country!.Contains(input) || o.Name!.Contains(input) 
            //             && o.PropertyRooms.Any(room => room.RoomAvailabilities.Any(availability => 
            //                             availability.From >= from 
            //                             && availability.To >= to
            //                             && availability.From.Month == from.Value.Month))) // todo fix upper bound
            //         .ToListAsync())
            //     .Select(domainEntity => Mapper.Map(domainEntity));

        }
        
        public override async Task<DAL.App.DTO.Property> FirstOrDefaultAsync(Guid id, object? userId = null)
        {
            var query = (await RepoDbSet
                    .AsNoTracking()
                    .Include(p => p.Reviews)
                    .Include(property => property.PropertyRooms)
                    .ThenInclude(room => room.RoomFacilities)
                    .FirstOrDefaultAsync(a => a.Id == id));
            return Mapper.Map(query);
        }
    }
       
}