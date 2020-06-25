using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class InvoiceRepository : EFBaseRepository<AppDbContext,Domain.Invoice,DAL.App.DTO.Invoice>,  IInvoiceRepository
    {
        public InvoiceRepository(AppDbContext dbContext) : base(dbContext, new BaseDALMapper<Domain.Invoice, DAL.App.DTO.Invoice>())
        {
        }

 
        public async Task<IEnumerable<Invoice>> AllAsync(Guid? userId = null)
        {
             
            return await base.AllAsync();
         }
        
        public async Task<Invoice> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.Id == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());

        }
        
        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }

            return await RepoDbSet.AnyAsync(a => a.Id == id && a.Id == userId);
        }
        
        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var invoice = await FirstOrDefaultAsync(id, userId);
            base.Remove(invoice);
        }
        
         
    }
}