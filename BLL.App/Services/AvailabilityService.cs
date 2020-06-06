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
    public class AvailabilityService : 
        BaseEntityService<IAvailabilityRepository, IAppUnitOfWork, DAL.App.DTO.Availability, Availability>, IAvailabilityService
    {
        public AvailabilityService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Availability, Availability>(), unitOfWork.Availabilities)
        {
        }

        public async  Task<IEnumerable<Availability>> AllAsync(Guid? userId = null)
        {
            return (await ServiceRepository.AllAsync(userId)).Select( dalEntity => Mapper.Map(dalEntity) );
        }

        public async Task<Availability> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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