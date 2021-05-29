using System;
using System.Threading.Tasks;
using BLL.Base.Services;
using BLL.App.DTO;
using BLL.Base.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class MongoAvailabilityService
        : BaseEntityService<IMongoAvailabilityRepository, IAppUnitOfWork, DAL.App.DTO.Availability, BLL.App.DTO.Availability>, IMongoAvailabilityService

    {
        public MongoAvailabilityService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Availability, BLL.App.DTO.Availability>(), unitOfWork.MongoAvailabilities)
        {
            
        }

        public async Task CreateNew(Guid propertyId)
        {
            await  ServiceRepository.CreateNew(propertyId);

        }

        public  Availability Add(Availability entity)
        {
            var dal = Mapper.Map<Availability,DAL.App.DTO.Availability>(entity);
            ServiceRepository.Add(dal); 
            return entity;
        }
    }

   
}