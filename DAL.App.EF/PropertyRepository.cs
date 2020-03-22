
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class PropertyRepository : BaseRepository<Property>,  IPropertyRepository
    {
        public PropertyRepository(DbContext dbContext) : base(dbContext)
        {
        }

 

        public IEnumerable<Property> All()
        {
            return   RepoDbSet.ToList();
        }

        public override async Task<IEnumerable<Property>> AllAsync()
        {
            return await RepoDbSet.ToListAsync();
        }

        public override Property Find(params object[] id)
        {
            return RepoDbSet.Find(id);
        }

        public override async Task<Property> FindAsync(params object[] id)
        {
            return await RepoDbSet.FindAsync(id);

        }

        public override Property Add(Property entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public override Property Update(Property entity)
        {
            return RepoDbSet.Update(entity).Entity;

        }

        public override Property Remove(Property entity)
        {
            return RepoDbSet.Remove(entity).Entity;
        }

        public override Property Remove(params object[] id)
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