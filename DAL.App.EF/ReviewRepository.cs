
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class ReviewRepository : BaseRepository<Review>,  IReviewRepository
    {
        public ReviewRepository(DbContext dbContext) : base(dbContext)
        {
        }

 

        public IEnumerable<Review> All()
        {
            return   RepoDbSet.ToList();
        }

        public override async Task<IEnumerable<Review>> AllAsync()
        {
            return await RepoDbSet.ToListAsync();
        }

        public override Review Find(params object[] id)
        {
            return RepoDbSet.Find(id);
        }

        public override async Task<Review> FindAsync(params object[] id)
        {
            return await RepoDbSet.FindAsync(id);

        }

        public override Review Add(Review entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public override Review Update(Review entity)
        {
            return RepoDbSet.Update(entity).Entity;

        }

        public override Review Remove(Review entity)
        {
            return RepoDbSet.Remove(entity).Entity;
        }

        public override Review Remove(params object[] id)
        {
            return RepoDbSet.Remove(Find(id)).Entity;
        }

        public override int SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public override  Task<int> SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}