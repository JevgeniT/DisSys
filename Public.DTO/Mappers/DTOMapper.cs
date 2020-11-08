using System;
using System.Linq;
using AutoMapper;
using Public.DTO;
using Public.DTO.Reservation;
using Bll = BLL.App.DTO;

namespace Public.DTO.Mappers
{
    public class DTOMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DTOMapper()   
        {
            MapperConfigurationExpression.CreateMap<Bll.Property, PropertyDTO>();
            MapperConfigurationExpression.CreateMap<Bll.Room, RoomDTO>();
            MapperConfigurationExpression.CreateMap<Bll.Facility, FacilityDTO>();
            MapperConfigurationExpression.CreateMap<Bll.Availability, AvailabilityDTO>()
                 .ForMember(dto => dto.RoomName, opt => opt.MapFrom(r  =>  r.Room.Name));
            
            MapperConfigurationExpression.CreateMap<Bll.AvailabilityPolicies, AvailabilityPoliciesDTO>();
            MapperConfigurationExpression.CreateMap<Bll.Policy, AvailabilityPoliciesDTO>();
            MapperConfigurationExpression.CreateMap<Bll.AvailabilityPolicies, PolicyDTO>();
            
            MapperConfigurationExpression.CreateMap<Bll.Policy, PolicyDTO>();
            MapperConfigurationExpression.CreateMap<Bll.PropertyRules, PropertyRulesDTO>()
                .ForMember(d => d.CheckInFrom, opt => opt.MapFrom(d => TimeSpan.Parse(d.CheckInFrom.ToString())))
                .ForMember(d => d.CheckInTo, opt => opt.MapFrom(d => TimeSpan.Parse(d.CheckInTo.ToString())))
                .ForMember(d => d.CheckOutBefore, opt => opt.MapFrom(d => TimeSpan.Parse(d.CheckOutBefore.ToString())));

            MapperConfigurationExpression.CreateMap<Bll.Extra, ExtraDTO>();

            MapperConfigurationExpression.CreateMap<Bll.Reservation, ReservationDTO>()
                .ForMember(dto => dto.ReservedBy,opt => opt.MapFrom(r  => $"{r.AppUser.FirstName} {r.AppUser.LastName}"))
                .ForMember(dto => dto.RoomDtos, opt => opt.MapFrom(r  =>  r.ReservationRooms.Select(rooms => rooms.Room)));
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
    
}