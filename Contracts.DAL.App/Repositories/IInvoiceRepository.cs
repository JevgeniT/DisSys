using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 public interface IInvoiceRepository : IBaseRepository<Invoice>
 {
  Task<IEnumerable<Invoice>> AllAsync(int? userId = null);
  Task<Invoice> FirstOrDefaultAsync(int id, int? userId = null);

  Task<bool> ExistsAsync(int id, int? userId = null);
  Task DeleteAsync(int id, int? userId = null);
        
  // DTO methods
  // Task<IEnumerable<InvoiceDTO>> DTOAllAsync(int? userId = null);
  // Task<InvoiceDTO> DTOFirstOrDefaultAsync(int id, int? userId = null);
 }
}