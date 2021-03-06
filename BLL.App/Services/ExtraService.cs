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
    public class ExtraService : 
    BaseEntityService<IExtraRepository, IAppUnitOfWork, DAL.App.DTO.Extra, Extra>, IExtraService
    {
        public ExtraService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Extra, Extra>(), unitOfWork.Extras)
        {
        }

        public async Task<IEnumerable<Extra>> AllAsync(Guid propertyId)
        {
            return (await ServiceRepository.AllAsync(propertyId)).Select(e => Mapper.Map(e));
        }
    }
}