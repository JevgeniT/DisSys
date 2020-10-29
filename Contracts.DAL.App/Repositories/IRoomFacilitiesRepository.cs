using System;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IRoomFacilitiesRepository : IRoomFacilitiesRepository<Guid, RoomFacilities>,
        IBaseRepository<RoomFacilities>
    {
    }
    public interface IRoomFacilitiesRepository<TKey, TDALEntity> : IBaseRepository<TKey,TDALEntity> 
        where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
        where TKey : IEquatable<TKey>
    {

    }
}