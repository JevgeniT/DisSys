using System;
using Contracts.DAL.App.Repositories;
using BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IRoomFacilitiesService : IRoomFacilitiesRepository<Guid, RoomFacilities>
    {
        
    }
}