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
            MapperConfigurationExpression.CreateMap<Domain.PropertyRules,  PropertyRules>();
            MapperConfigurationExpression.CreateMap<Domain.Availability, Availability>();
            MapperConfigurationExpression.CreateMap<Domain.RoomFacilities, RoomFacilities>();
            MapperConfigurationExpression.CreateMap<Domain.Facility,  Facility>();
            MapperConfigurationExpression.CreateMap<Domain.Extra,  Extra>();

            MapperConfigurationExpression.CreateMap<Domain.Room ,  Room>()
                .ForMember(room => room.RoomFacilities, opt => opt.MapFrom(rf=> rf.RoomFacilities!.Select(f=> f.Facility)));
            MapperConfigurationExpression.CreateMap<Domain.Availability ,  Availability>();
            MapperConfigurationExpression.CreateMap<Domain.Policy,  Policy>();
            MapperConfigurationExpression.CreateMap<Domain.PropertyRules,  PropertyRules>();
            MapperConfigurationExpression.CreateMap<Domain.Review ,  Review>();
            MapperConfigurationExpression.CreateMap<Domain.Reservation,  Reservation>();
            MapperConfigurationExpression.CreateMap<Domain.ReservationRooms,  ReservationRooms>();
            MapperConfigurationExpression.CreateMap<Domain.ReservationExtras,  ReservationExtras>();

            MapperConfigurationExpression.CreateMap<ReservationRooms,  Domain.ReservationRooms>();
            MapperConfigurationExpression.CreateMap<ReservationExtras,  Domain.ReservationExtras>();

            MapperConfigurationExpression.CreateMap<Domain.Identity.AppUser,  AppUser>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}