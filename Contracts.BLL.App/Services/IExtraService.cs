using System;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IExtraService: IExtraRepository<Guid,Extra>
    {
        
    }
}