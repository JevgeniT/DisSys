using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class AvailabilityPoliciesRepository : 
        EFBaseRepository<AppDbContext,AvailabilityPolicies,  DAL.App.DTO.AvailabilityPolicies>,  IAvailabilityPoliciesRepository
    {
        public AvailabilityPoliciesRepository(AppDbContext dbContext) 
            :base(dbContext, new DALMapper<AvailabilityPolicies,  DAL.App.DTO.AvailabilityPolicies>())
        {
        }

     
    }
}


