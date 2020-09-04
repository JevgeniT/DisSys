using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Mappers;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class AvailabilityPoliciesService : 
        BaseEntityService<IAvailabilityPoliciesRepository, IAppUnitOfWork, DAL.App.DTO.AvailabilityPolicies, AvailabilityPolicies>, IAvailabilityPoliciesService
    {
        public AvailabilityPoliciesService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.AvailabilityPolicies, AvailabilityPolicies>(), unitOfWork.AvailabilityPolicies)
        {
        }

        public Task<IEnumerable<AvailabilityPolicies>> AllAsync(Guid? roomId = null)
        {
            throw new NotImplementedException();
        }

        public Task<AvailabilityPolicies> FirstOrDefaultAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}