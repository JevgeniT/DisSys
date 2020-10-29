using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class FacilityRepository : EFBaseRepository<AppDbContext,Domain.Identity.AppUser, Facility, DAL.App.DTO.Facility>,  IFacilityRepository
    {
        public FacilityRepository(AppDbContext dbContext) :  base(dbContext, new DALMapper<Facility, DAL.App.DTO.Facility>())
        {
        }

    
    }
}