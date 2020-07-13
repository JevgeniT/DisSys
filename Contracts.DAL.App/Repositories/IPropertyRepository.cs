using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
 
  public interface IPropertyRepository : IPropertyRepository<Guid, Property>, IBaseRepository<Property>, IPropertyCustomRepository
  {
  }
  public interface IPropertyRepository<TKey, TDALEntity> : IBaseRepository<TKey,TDALEntity> 
     where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
     where TKey : IEquatable<TKey>
  {
        Task<IEnumerable<TDALEntity>> AllAsync(Guid? userId = null);
           
        Task<IEnumerable<TDALEntity>> FindAsync(SearchDTO? param );

        Task<TDALEntity> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
    }
}