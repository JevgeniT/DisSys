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
    public class ExtraRepository : EFBaseRepository<AppDbContext,Extra, DAL.App.DTO.Extra>,  IExtraRepository
    {
        public ExtraRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Extra, DAL.App.DTO.Extra>())
        {
        }


        public async Task<IEnumerable<DAL.App.DTO.Extra>> AllAsync(Guid? userId = null)
        {
             
            return await base.AllAsync();
         }
        
        public async Task<DAL.App.DTO.Extra> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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
            var extra = await FirstOrDefaultAsync(id, userId);
            base.Remove(extra);
        }
    }
}