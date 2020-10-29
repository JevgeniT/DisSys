
using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class RoomFacilitiesService :
        BaseEntityService<IRoomFacilitiesRepository, IAppUnitOfWork, DAL.App.DTO.RoomFacilities, RoomFacilities>, IRoomFacilitiesService
    {
        public RoomFacilitiesService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.RoomFacilities, RoomFacilities>(),
                unitOfWork.RoomFacilities)
        {
        }
        
    }
}