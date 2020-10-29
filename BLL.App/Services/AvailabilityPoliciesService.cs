using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class AvailabilityPoliciesService : BaseEntityService<IAvailabilityPoliciesRepository, IAppUnitOfWork, DAL.App.DTO.AvailabilityPolicies, AvailabilityPolicies>, IAvailabilityPoliciesService
    {
     
        public AvailabilityPoliciesService(IAppUnitOfWork unitOfWork) 
             : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.AvailabilityPolicies, AvailabilityPolicies>(), unitOfWork.AvailabilityPolicies)
        {
        }

        
    }
}