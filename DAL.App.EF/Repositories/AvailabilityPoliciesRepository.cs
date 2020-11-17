using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class ReservationExtrasRepository : 
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser,ReservationExtras,  DAL.App.DTO.ReservationExtras>,  IReservationExtrasRepository
    {
        public ReservationExtrasRepository(AppDbContext dbContext) 
            :base(dbContext, new DALMapper<ReservationExtras,  DAL.App.DTO.ReservationExtras>())
        {
        }

     
    }
}


