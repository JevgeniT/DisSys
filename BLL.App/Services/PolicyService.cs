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
    public class PolicyService:
        BaseEntityService<IPolicyRepository, IAppUnitOfWork, DAL.App.DTO.Policy, Policy>, IPolicyService
    {
        public PolicyService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Policy, Policy>(), unitOfWork.Policies)
        {
        }

        public async  Task<IEnumerable<Policy>> AllAsync(Guid? propertyId)
        {
            return (await ServiceRepository.AllAsync(propertyId)).Select( dalEntity => Mapper.Map(dalEntity) );
        }

        public async Task<Policy> FirstOrDefaultAsync(Guid id, Guid? propertyId = null)
        {
            return    Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id, propertyId));        
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return  await ServiceRepository.ExistsAsync(id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            await ServiceRepository.DeleteAsync(id, userId);
           
        }
    }
}