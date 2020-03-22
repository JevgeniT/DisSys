
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class ReservationRepository : BaseRepository<Reservation>,  IReservationRepository
    {
        public ReservationRepository(DbContext dbContext) : base(dbContext)
        {
        }

 

        public IEnumerable<Reservation> All()
        {
            return   RepoDbSet.ToList();
        }

        public override async Task<IEnumerable<Reservation>> AllAsync()
        {
            return await RepoDbSet.ToListAsync();
        }

        public override Reservation Find(params object[] id)
        {
            return RepoDbSet.Find(id);
        }

        public override async Task<Reservation> FindAsync(params object[] id)
        {
            return await RepoDbSet.FindAsync(id);

        }

        public override Reservation Add(Reservation entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public override Reservation Update(Reservation entity)
        {
            return RepoDbSet.Update(entity).Entity;

        }

        public override Reservation Remove(Reservation entity)
        {
            return RepoDbSet.Remove(entity).Entity;
        }

        public override Reservation Remove(params object[] id)
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