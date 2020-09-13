using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ReservationRoomsService : 
        BaseEntityService<IReservationRoomsRepository, IAppUnitOfWork, DAL.App.DTO.ReservationRooms, ReservationRooms>, IReservationRoomsService
    {
        public ReservationRoomsService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.ReservationRooms, ReservationRooms>(), unitOfWork.ReservationRooms)
        {
        }


    }
}