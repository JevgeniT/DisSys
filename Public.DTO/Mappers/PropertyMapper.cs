using System;
using System.Linq;
using AutoMapper;
using BLL.App.DTO;

namespace Public.DTO.Mappers
{
    public class PropertyMapper: BaseMapper<Property, PropertyDTO>
    {
        public PropertyMapper()
        {
            MapperConfigurationExpression.CreateMap<Property, PropertyDTO>();
            MapperConfigurationExpression.CreateMap<Room, RoomDTO>();
            MapperConfigurationExpression.CreateMap<Facility, FacilityDTO>();
            MapperConfigurationExpression.CreateMap<Room, RoomViewDTO>();

            MapperConfigurationExpression.CreateMap<Property, PropertyViewDTO>()
                .ForMember(dto => dto.Score, opt=> opt.MapFrom(property => property.Reviews.Count==0? 0.0 :
                        Math.Round(property.Reviews.Average(review => review.Score),1)))
                .ForMember(dto => dto.Room, opt=> 
                    opt.MapFrom(property => property.PropertyRooms.OrderByDescending(room => 
                        room.RoomAvailabilities.Min(availability => availability.PricePerNightForAdult)).Reverse().FirstOrDefault()));
                ;
            
            MapperConfigurationExpression.CreateMap<Property, PropertyDTO>()
                .ForMember(dto => dto.Score, opt=> 
                    opt.MapFrom(property => property.Reviews.Count==0? 0.0 :
                        Math.Round(property.Reviews.Average(review => review.Score),1)));
            
            MapperConfigurationExpression.CreateMap<Property, PropertyViewDTO>()
                .ForMember(dto => dto.Room, opt=> 
                    opt.MapFrom(property => property.PropertyRooms.OrderByDescending(room => 
                        room.RoomAvailabilities.Min(availability => availability.PricePerNightForAdult)).Reverse().FirstOrDefault()));

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public PropertyViewDTO MapPropertyView(Property inObject)
        {
            return Mapper.Map<PropertyViewDTO>(inObject);
        }
    }
}
