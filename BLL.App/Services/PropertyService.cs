using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using Public.DTO;
using Property = BLL.App.DTO.Property;
using PropertyView = BLL.App.DTO.PropertyView;

namespace BLL.App.Services
{
    public class PropertyService :
        BaseEntityService<IPropertyRepository, IAppUnitOfWork, DAL.App.DTO.Property, Property>, IPropertyService
    {
        public PropertyService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Property, Property>(), unitOfWork.Properties)
        {
        }
       
        public async  Task<IEnumerable<Property>> AllAsync(Guid? userId = null)
        {
            return (await ServiceRepository.AllAsync(userId)).Select(dalEntity => Mapper.Map(dalEntity));
        }

        public async Task<IEnumerable<Property>> FindAsync(SearchDTO? search)
        {
            return (await ServiceRepository.FindAsync(search)).Select( dalEntity => Mapper.Map(dalEntity) );
        }

        public virtual async Task<IEnumerable<PropertyView>> FindForViewAsync(SearchDTO searchDTO)
        {
            BaseBLLMapper<DAL.App.DTO.PropertyView, PropertyView> mapper = new BaseBLLMapper<DAL.App.DTO.PropertyView, PropertyView>(); // TODO fix mapper 
            
            return (await ServiceRepository.FindForViewAsync(searchDTO)).Select(e => mapper.Map(e));
        }


        public async Task<Property> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            return  Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id, userId));        
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