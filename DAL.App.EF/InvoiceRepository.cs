
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class InvoiceRepository : BaseRepository<Invoice>,  IInvoiceRepository
    {
        public InvoiceRepository(DbContext dbContext) : base(dbContext)
        {
        }

 

        public IEnumerable<Invoice> All()
        {
            return   RepoDbSet.ToList();
        }

        public override async Task<IEnumerable<Invoice>> AllAsync()
        {
            return await RepoDbSet.ToListAsync();
        }

        public override Invoice Find(params object[] id)
        {
            return RepoDbSet.Find(id);
        }

        public override async Task<Invoice> FindAsync(params object[] id)
        {
            return await RepoDbSet.FindAsync(id);

        }

        public override Invoice Add(Invoice entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public override Invoice Update(Invoice entity)
        {
            return RepoDbSet.Update(entity).Entity;

        }

        public override Invoice Remove(Invoice entity)
        {
            return RepoDbSet.Remove(entity).Entity;
        }

        public override Invoice Remove(params object[] id)
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