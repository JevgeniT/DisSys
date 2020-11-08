using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class FacilityService :
        BaseEntityService<IFacilityRepository, IAppUnitOfWork, DAL.App.DTO.Facility, Facility>, IFacilityService
    {
        public FacilityService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Facility, Facility>(), unitOfWork.Facilities)
        {
        }
    }
}