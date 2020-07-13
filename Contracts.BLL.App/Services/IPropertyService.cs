using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;
using Public.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IPropertyService : IPropertyRepository<Guid,Property>
    {
        Task<IEnumerable<PropertyView>> FindForViewAsync(SearchDTO searchDTO);
    }
}