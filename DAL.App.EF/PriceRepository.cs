
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class PriceRepository : BaseRepository<Price>,  IPriceRepository
    {
        public PriceRepository(DbContext dbContext) : base(dbContext)
        {
        }

 

        public IEnumerable<Price> All()
        {
            return   RepoDbSet.ToList();
        }

        public override async Task<IEnumerable<Price>> AllAsync()
        {
            return await RepoDbSet.ToListAsync();
        }

        public override Price Find(params object[] id)
        {
            return RepoDbSet.Find(id);
        }

        public override async Task<Price> FindAsync(params object[] id)
        {
            return await RepoDbSet.FindAsync(id);

        }

        public override Price Add(Price entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public override Price Update(Price entity)
        {
            return RepoDbSet.Update(entity).Entity;

        }

        public override Price Remove(Price entity)
        {
            return RepoDbSet.Remove(entity).Entity;
        }

        public override Price Remove(params object[] id)
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