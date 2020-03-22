using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class LocationRepository : BaseRepository<Location>,  ILocationRepository
    {
        public LocationRepository(DbContext dbContext) : base(dbContext)
        {
        }

 

        public IEnumerable<Location> All()
        {
            return   RepoDbSet.ToList();
        }

        public override async Task<IEnumerable<Location>> AllAsync()
        {
            return await RepoDbSet.ToListAsync();
        }

        public override Location Find(params object[] id)
        {
            return RepoDbSet.Find(id);
        }

        public override async Task<Location> FindAsync(params object[] id)
        {
            return await RepoDbSet.FindAsync(id);

        }

        public override Location Add(Location entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public override Location Update(Location entity)
        {
            return RepoDbSet.Update(entity).Entity;

        }

        public override Location Remove(Location entity)
        {
            return RepoDbSet.Remove(entity).Entity;
        }

        public override Location Remove(params object[] id)
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