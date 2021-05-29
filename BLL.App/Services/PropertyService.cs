using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Exceptions;
using Property = BLL.App.DTO.Property;

namespace BLL.App.Services
{
    public class PropertyService :
        BaseEntityService<IPropertyRepository, IAppUnitOfWork, DAL.App.DTO.Property, Property>, IPropertyService
    {
        public PropertyService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Property, Property>(), unitOfWork.Properties)
        {
        }

        public async Task<IEnumerable<Property>> FindAsync(DateTime? from, DateTime? to, string input)
        {
            var properties = await ServiceRepository.FindAsync(from, to, input);
            
            if (properties is null || properties.Count() is 0) throw new NotFoundException();
            
            return properties.Select(dalEntity => Mapper.Map(dalEntity));
        }
    }
}