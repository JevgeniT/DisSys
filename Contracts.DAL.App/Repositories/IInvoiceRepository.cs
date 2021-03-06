using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IInvoiceRepository : IInvoiceRepository<Guid, Invoice>,
     IBaseRepository<Invoice>
    {
    }
    public interface IInvoiceRepository<TKey, TDALEntity> : IBaseRepository<TKey,TDALEntity> 
     where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
     where TKey : IEquatable<TKey>
    {

    }
    
  
}