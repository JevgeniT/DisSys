using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;
using Public.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPropertyCustomRepository : IPropertyCustomRepository<PropertyView>
    {
        
    }
    
    public interface IPropertyCustomRepository<TPropertyView>
    {
        Task<IEnumerable<TPropertyView>> FindForViewAsync(SearchDTO searchDTO);
    }
}