
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
    public class ReviewRepository 
        : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Review, DAL.App.DTO.Review>,  IReviewRepository
    {
        public ReviewRepository(AppDbContext dbContext) 
            : base(dbContext, new DALMapper<Review, DAL.App.DTO.Review>())
        {
        }
        

        public async Task<IEnumerable<DTO.Review>> PropertyReviews(Guid? propertyId)
        {
            var query = RepoDbSet.Where(a => a.PropertyId == propertyId)
                .Include(r=>r.AppUser).AsQueryable();
            
            return await query.Select(entity=> Mapper.Map(entity)).ToListAsync();
        }

        public override async Task<bool> ExistsAsync(Guid id, object? userId = null)
        {
            return await RepoDbSet.AnyAsync(review => review.ReservationId == id && review.AppUserId == (Guid) userId!);
        }
    }
}