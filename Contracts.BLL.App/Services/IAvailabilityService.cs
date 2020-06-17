using System;
using System.Collections.Generic;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;
 
namespace Contracts.BLL.App.Services
{
    public interface IAvailabilityService : IAvailabilityRepository<Guid,Availability>
    {
        void ParseDate(List<Availability> list, DateTime from, DateTime to);
        
    }
}