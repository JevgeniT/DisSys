
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class ExtraRepository : BaseRepository<Extra>,  IExtraRepository
    {
        public ExtraRepository(DbContext dbContext) : base(dbContext)
        {
        }

 

        public IEnumerable<Extra> All()
        {
            return   RepoDbSet.ToList();
        }

        public override async Task<IEnumerable<Extra>> AllAsync()
        {
            return await RepoDbSet.ToListAsync();
        }

        public override Extra Find(params object[] id)
        {
            return RepoDbSet.Find(id);
        }

        public override async Task<Extra> FindAsync(params object[] id)
        {
            return await RepoDbSet.FindAsync(id);

        }

        public override Extra Add(Extra entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public override Extra Update(Extra entity)
        {
            return RepoDbSet.Update(entity).Entity;

        }

        public override Extra Remove(Extra entity)
        {
            return RepoDbSet.Remove(entity).Entity;
        }

        public override Extra Remove(params object[] id)
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