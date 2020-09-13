using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{
    public interface IReservationRoomsRepository : IReservationRoomsRepository<Guid, ReservationRooms>,
        IBaseRepository<ReservationRooms>
    {
    }
    
    public interface IReservationRoomsRepository <TKey, TDALEntity> : IBaseRepository<TKey,TDALEntity> 
        where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
        
    }
}