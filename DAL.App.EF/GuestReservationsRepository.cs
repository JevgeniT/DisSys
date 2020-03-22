
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class GuestReservationsRepository : BaseRepository<GuestReservations>,  IGuestReservationsRepository
    {
        public GuestReservationsRepository(DbContext dbContext) : base(dbContext)
        {
        }

 

        public IEnumerable<GuestReservations> All()
        {
            return   RepoDbSet.ToList();
        }

        public override async Task<IEnumerable<GuestReservations>> AllAsync()
        {
            return await RepoDbSet.ToListAsync();
        }

        public override GuestReservations Find(params object[] id)
        {
            return RepoDbSet.Find(id);
        }

        public override async Task<GuestReservations> FindAsync(params object[] id)
        {
            return await RepoDbSet.FindAsync(id);

        }

        public override GuestReservations Add(GuestReservations entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public override GuestReservations Update(GuestReservations entity)
        {
            return RepoDbSet.Update(entity).Entity;

        }

        public override GuestReservations Remove(GuestReservations entity)
        {
            return RepoDbSet.Remove(entity).Entity;
        }

        public override GuestReservations Remove(params object[] id)
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