using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
 
namespace Contracts.DAL.App.Repositories
{
 
  public interface IPropertyRepository : IPropertyRepository<Guid, Property>, IBaseRepository<Property>
  {
  }
  public interface IPropertyRepository<TKey, TDALEntity> : IBaseRepository<TKey,TDALEntity> 
     where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
     where TKey : IEquatable<TKey>
  {

        Task<IEnumerable<TDALEntity>> FindAsync(DateTime? from, DateTime? to, string input);
  }
}