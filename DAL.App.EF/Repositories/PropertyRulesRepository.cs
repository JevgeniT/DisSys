using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class PropertyRulesRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser,  PropertyRules, DAL.App.DTO. PropertyRules>,  IPropertyRulesRepository
    {
        public PropertyRulesRepository(AppDbContext dbContext) : 
            base(dbContext, new DALMapper<PropertyRules, DAL.App.DTO.PropertyRules>())
        {
        }
        
      
    }
}