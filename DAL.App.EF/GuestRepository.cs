
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class GuestRepository : BaseRepository<Guest>,  IGuestRepository
    {
        public GuestRepository(DbContext dbContext) : base(dbContext)
        {
        }

 

        public IEnumerable<Guest> All()
        {
            return   RepoDbSet.ToList();
        }

        public override async Task<IEnumerable<Guest>> AllAsync()
        {
            return await RepoDbSet.ToListAsync();
        }

        public override Guest Find(params object[] id)
        {
            return RepoDbSet.Find(id);
        }

        public override async Task<Guest> FindAsync(params object[] id)
        {
            return await RepoDbSet.FindAsync(id);

        }

        public override Guest Add(Guest entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public override Guest Update(Guest entity)
        {
            return RepoDbSet.Update(entity).Entity;

        }

        public override Guest Remove(Guest entity)
        {
            return RepoDbSet.Remove(entity).Entity;
        }

        public override Guest Remove(params object[] id)
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