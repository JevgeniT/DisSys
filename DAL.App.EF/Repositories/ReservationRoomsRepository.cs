using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ReservationRoomsRepository:
        EFBaseRepository<AppDbContext,Domain.Identity.AppUser,ReservationRooms,  DAL.App.DTO.ReservationRooms>,  IReservationRoomsRepository

    {
        public ReservationRoomsRepository(AppDbContext dbContext) : 
            base(dbContext, new DALMapper<ReservationRooms, DTO.ReservationRooms>())
        {
        }

    }
}