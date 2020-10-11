using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IMongoAvailabilityService: IMongoAvailabilityRepository<Guid,Availability>
    {
 
    }
}