using AutoMapper;
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
            MapperConfigurationExpression.CreateMap<Bll.PropertyType, string>().ConvertUsing(src=> src.ToString());
            MapperConfigurationExpression.CreateMap<Bll.Facility, FacilityDTO>();
            MapperConfigurationExpression.CreateMap<Bll.Availability, AvailabilityDTO>();
            MapperConfigurationExpression.CreateMap<Bll.Availability, AvailabilityDTO>().ForMember(dto => dto.RoomName,
                opt => opt.MapFrom(r  =>  r.Room.Name));;

            MapperConfigurationExpression.CreateMap<Bll.Reservation, ReservationDTO>();
            MapperConfigurationExpression.CreateMap<Bll.Reservation, ReservationDTO>().ForMember(dto => dto.ReservedBy,
                opt => opt.MapFrom(r  =>  r.AppUser.FirstName + " " + r.AppUser.LastName));

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));

        }
    }
    
}