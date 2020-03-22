
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class PolicyRepository : BaseRepository<Policy>,  IPolicyRepository
    {
        public PolicyRepository(DbContext dbContext) : base(dbContext)
        {
        }

 

        public IEnumerable<Policy> All()
        {
            return   RepoDbSet.ToList();
        }

        public override async Task<IEnumerable<Policy>> AllAsync()
        {
            return await RepoDbSet.ToListAsync();
        }

        public override Policy Find(params object[] id)
        {
            return RepoDbSet.Find(id);
        }

        public override async Task<Policy> FindAsync(params object[] id)
        {
            return await RepoDbSet.FindAsync(id);

        }

        public override Policy Add(Policy entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public override Policy Update(Policy entity)
        {
            return RepoDbSet.Update(entity).Entity;

        }

        public override Policy Remove(Policy entity)
        {
            return RepoDbSet.Remove(entity).Entity;
        }

        public override Policy Remove(params object[] id)
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