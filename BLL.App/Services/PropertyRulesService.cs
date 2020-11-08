using BLL.App.DTO;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class PropertyRulesService :
        BaseEntityService<IPropertyRulesRepository, IAppUnitOfWork, DAL.App.DTO.PropertyRules, PropertyRules>, IPropertyRulesService
    {
        public PropertyRulesService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.PropertyRules, PropertyRules>(), unitOfWork.PropertyRules)
        {
        }
    }
}