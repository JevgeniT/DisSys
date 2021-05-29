using System;
using System.Linq;
using AutoMapper;
using BLL.App.DTO;
using Public.DTO.Room;

namespace Public.DTO.Mappers
{
    public class PropertyMapper: BaseMapper<Property, PropertyDTO>
    {
        public PropertyMapper()
        {
            MapperConfigurationExpression.CreateMap<Property, PropertyDTO>();
            MapperConfigurationExpression.CreateMap<PropertyCreateDTO, Property>();

            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Room, RoomDTO>()
                .ForMember(r=>r.Facilities,
                    opt => opt.MapFrom(f=> f.RoomFacilities!.Select( fc => fc.Name)))
                .ForMember(r=> r.FacilityDtos, opt=> opt.Ignore());
            MapperConfigurationExpression.CreateMap<Facility, FacilityDTO>();
            MapperConfigurationExpression.CreateMap<Extra, ExtraDTO>();

            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Room, RoomViewDTO>()
                .ForMember(r=> r.Price, opt 
                    => opt.MapFrom(room => room.RoomAvailabilities!.Min(a=>a.PricePerNightForAdult)));
            
            MapperConfigurationExpression.CreateMap<PropertyRules, PropertyRulesDTO>();
            MapperConfigurationExpression.CreateMap<Property, PropertyViewDTO>()
                .ForMember(dto => dto.Score,
                    opt => opt.MapFrom(property =>
                        property.Reviews!.Count == 0
                            ? 0.0
                            : Math.Round(property.Reviews!.Average(review => review.Score), 1)))
                .ForMember(dto => dto.Room!, opt =>
                    opt.MapFrom(p => p.PropertyRooms!.OrderByDescending(room =>
                            room.RoomAvailabilities!.Min(a => a.PricePerNightForAdult)).Reverse()
                        .FirstOrDefault()));

            MapperConfigurationExpression.CreateMap<Property, PropertyDTO>()
                .ForMember(dto => dto.Score, opt=> 
                    opt.MapFrom(property => property.Reviews!.Count==0? 0.0 :
                        Math.Round(property.Reviews!.Average(review => review.Score),1)));

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        public PropertyViewDTO MapPropertyView(Property inObject)
            => Mapper.Map<PropertyViewDTO>(inObject);
        
        public Property MapPropertyCreateView(PropertyCreateDTO inObject)
            => Mapper.Map<Property>(inObject);
    }
}
