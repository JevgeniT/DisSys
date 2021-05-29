using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Public.DTO;

namespace BLL.App.Services
{
    public class RoomService: 
        BaseEntityService<IRoomRepository, IAppUnitOfWork, DAL.App.DTO.Room, Room>, IRoomService
    {
        public RoomService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Room, Room>(), unitOfWork.Rooms)
        {
        }

        public async Task<IEnumerable<Room>> AllAsync(Guid propertyId)
            => (await ServiceRepository.AllAsync(propertyId)).Select( dalEntity => Mapper.Map(dalEntity));
    }
}