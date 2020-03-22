
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class FacilityRepository : BaseRepository<Facility>,  IFacilityRepository
    {
        public FacilityRepository(DbContext dbContext) : base(dbContext)
        {
        }

 

        public IEnumerable<Facility> All()
        {
            return   RepoDbSet.ToList();
        }

        public override async Task<IEnumerable<Facility>> AllAsync()
        {
            return await RepoDbSet.ToListAsync();
        }

        public override Facility Find(params object[] id)
        {
            return RepoDbSet.Find(id);
        }

        public override async Task<Facility> FindAsync(params object[] id)
        {
            return await RepoDbSet.FindAsync(id);

        }

        public override Facility Add(Facility entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public override Facility Update(Facility entity)
        {
            return RepoDbSet.Update(entity).Entity;

        }

        public override Facility Remove(Facility entity)
        {
            return RepoDbSet.Remove(entity).Entity;
        }

        public override Facility Remove(params object[] id)
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