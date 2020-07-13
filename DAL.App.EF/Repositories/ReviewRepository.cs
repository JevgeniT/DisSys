
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
    public class ReviewRepository : EFBaseRepository<AppDbContext,Review, DAL.App.DTO.Review>,  IReviewRepository
    {
        public ReviewRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Review, DAL.App.DTO.Review>())
        {
        }

 
        public async Task<IEnumerable<DAL.App.DTO.Review>> AllAsync(Guid? userId = null)
        {
            return await base.AllAsync();
        }

        public async Task<IEnumerable<DTO.Review>> PropertyReviews(Guid? propertyId)
        {
            var query = RepoDbSet.Where(a => a.PropertyId == propertyId).AsQueryable();

            return await query.Select(entity=> Mapper.Map(entity)).ToListAsync();
        }

        public async Task<DAL.App.DTO.Review> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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
            var review = await FirstOrDefaultAsync(id, userId);
            base.Remove(review);
        }
       
    }
}