using System;
using System.Linq;
using AutoMapper;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using DAL.Base.EF;

namespace Contracts.DAL.App
{
    public class DALMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DALMapper() : base()
        { 
            MapperConfigurationExpression.CreateMap<Domain.Property,  Property>();
            MapperConfigurationExpression.CreateMap<Domain.Availability, Availability>();
            MapperConfigurationExpression.CreateMap<Domain.Facility,  Facility>();
            MapperConfigurationExpression.CreateMap<Domain.Room ,  Room>();
            MapperConfigurationExpression.CreateMap<Domain.Availability ,  Availability>();
            MapperConfigurationExpression.CreateMap<Domain.AvailabilityPolicies ,  AvailabilityPolicies>();
            MapperConfigurationExpression.CreateMap<Domain.Policy,  Policy>();
            MapperConfigurationExpression.CreateMap<Domain.Review ,  Review>();
            MapperConfigurationExpression.CreateMap<Domain.Reservation,  Reservation>();
            MapperConfigurationExpression.CreateMap<Domain.ReservationRooms,  ReservationRooms>();
            MapperConfigurationExpression.CreateMap<Domain.Identity.AppUser,  AppUser>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}