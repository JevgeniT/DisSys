
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class RoomRepository : BaseRepository<Room>,  IRoomRepository
    {
        public RoomRepository(DbContext dbContext) : base(dbContext)
        {
        }

 

        public IEnumerable<Room> All()
        {
            return   RepoDbSet.ToList();
        }

        public override async Task<IEnumerable<Room>> AllAsync()
        {
            return await RepoDbSet.ToListAsync();
        }

        public override Room Find(params object[] id)
        {
            return RepoDbSet.Find(id);
        }

        public override async Task<Room> FindAsync(params object[] id)
        {
            return await RepoDbSet.FindAsync(id);

        }

        public override Room Add(Room entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public override Room Update(Room entity)
        {
            return RepoDbSet.Update(entity).Entity;

        }

        public override Room Remove(Room entity)
        {
            return RepoDbSet.Remove(entity).Entity;
        }

        public override Room Remove(params object[] id)
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