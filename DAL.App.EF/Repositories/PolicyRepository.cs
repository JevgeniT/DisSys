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
    public class PolicyRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser,Policy, DAL.App.DTO.Policy>,  IPolicyRepository
    {
        public PolicyRepository(AppDbContext dbContext) : base(dbContext, new DALMapper<Policy, DAL.App.DTO.Policy>())
        {
        }

 
        public async Task<IEnumerable<DAL.App.DTO.Policy>> AllAsync(Guid? propertyId)
        {
            return (await RepoDbSet.Where(policy => policy.PropertyId == propertyId).ToListAsync()).Select(policy => Mapper.Map(policy));
         
        }
        
        public async Task<DAL.App.DTO.Policy> FirstOrDefaultAsync(Guid id, Guid? propertyId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (propertyId != null)
            {
                query = query.Where(a => a.PropertyId == propertyId);
            }
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }
 
   
    }
}