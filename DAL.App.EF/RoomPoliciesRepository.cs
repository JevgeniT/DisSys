
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class RoomPoliciesRepository : BaseRepository<RoomPolicies>,  IRoomPoliciesRepository
    {
        public RoomPoliciesRepository(DbContext dbContext) : base(dbContext)
        {
        }

 

        public IEnumerable<RoomPolicies> All()
        {
            return   RepoDbSet.ToList();
        }

        public override async Task<IEnumerable<RoomPolicies>> AllAsync()
        {
            return await RepoDbSet.ToListAsync();
        }

        public override RoomPolicies Find(params object[] id)
        {
            return RepoDbSet.Find(id);
        }

        public override async Task<RoomPolicies> FindAsync(params object[] id)
        {
            return await RepoDbSet.FindAsync(id);

        }

        public override RoomPolicies Add(RoomPolicies entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public override RoomPolicies Update(RoomPolicies entity)
        {
            return RepoDbSet.Update(entity).Entity;

        }

        public override RoomPolicies Remove(RoomPolicies entity)
        {
            return RepoDbSet.Remove(entity).Entity;
        }

        public override RoomPolicies Remove(params object[] id)
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