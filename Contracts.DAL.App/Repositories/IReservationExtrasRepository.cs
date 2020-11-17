using System;

using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IReservationExtrasRepository : IReservationExtrasRepository<Guid, ReservationExtras>,
        IBaseRepository<ReservationExtras>
    {
    }
    
    public interface IReservationExtrasRepository<TKey, TDALEntity> 
        : IBaseRepository<TKey,TDALEntity> 
        where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
    }
}