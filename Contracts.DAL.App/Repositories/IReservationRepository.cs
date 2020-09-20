using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
 
namespace Contracts.DAL.App.Repositories
{


 public interface IReservationRepository : IReservationRepository<Guid, Reservation>,
  IBaseRepository<Reservation>
 {
  
 }


  public interface IReservationRepository <TKey, TDALEntity> : IBaseRepository<TKey,TDALEntity> 
   where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
   where TKey : IEquatable<TKey>
  {

   Task<IEnumerable<TDALEntity>> AllAsync(Guid? userId = null, Guid? propertyId = null);
   
  }
}