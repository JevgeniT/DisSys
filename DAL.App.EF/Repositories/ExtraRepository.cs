using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ExtraRepository : 
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Extra, DAL.App.DTO.Extra>,  IExtraRepository
    {
        public ExtraRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Extra, DAL.App.DTO.Extra>())
        {
        }

        public async Task<IEnumerable<DTO.Extra>> AllAsync(Guid propertyId)
        {
            var query = await RepoDbSet.Where(extra => extra.PropertyId == propertyId).ToListAsync();
            return query.Select( e=> Mapper.Map(e));
        }
    }
}