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

namespace BLL.App.Services
{
    public class RoomService: 
        BaseEntityService<IRoomRepository, IAppUnitOfWork, DAL.App.DTO.Room, Room>, IRoomService
    {
        public RoomService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Room, Room>(), unitOfWork.Rooms)
        {
        }

        public async  Task<IEnumerable<Room>> AllAsync(Guid? userId = null)
        {
            return (await ServiceRepository.AllAsync()).Select( dalEntity => Mapper.Map(dalEntity) );
        }

        public async Task<Room> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            return    Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id, userId));        
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return  await ServiceRepository.ExistsAsync(id, userId);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            await ServiceRepository.DeleteAsync(id, userId);
           
        }
    }
    
    
}